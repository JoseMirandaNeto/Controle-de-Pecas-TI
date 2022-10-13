using Godot;
using System;
using System.Diagnostics;

public class UpdateDialog : ConfirmationDialog
{
    private Thread t = new Thread();
    private Timer checkTimer;
    private PopupPanel downloadDialog;
    private ProgressBar downloadProgress;

    private HTTPRequest updReq = new HTTPRequest();
    private HTTPRequest downReq = new HTTPRequest();
    private AcceptDialog acceptDialog;

    public override void _Ready()
    {
        var arg_bytes_loaded = new Godot.Collections.Dictionary();
        arg_bytes_loaded.Add("name", "bytes_loaded");
        arg_bytes_loaded.Add("type", Variant.Type.Int);

        var arg_bytes_total = new Godot.Collections.Dictionary();
        arg_bytes_total.Add("name", "bytes_total");
        arg_bytes_total.Add("type", Variant.Type.Int);

        var arrLoading = new Godot.Collections.Array();
        arrLoading.Add(arg_bytes_loaded);
        arrLoading.Add(arg_bytes_total);

        AddUserSignal("loading", arrLoading);

        var arg_result = new Godot.Collections.Dictionary();
        arg_result.Add("name", "result");
        arg_result.Add("type", Variant.Type.RawArray);

        var arrLoaded = new Godot.Collections.Array();
        arrLoaded.Add(arg_result);

        AddUserSignal("loaded", arrLoaded);

        // MEU CÓDIGO
        GetTree().Paused = true;

        GetOk().Text = "SIM";
        GetCancel().Text = "NÂO";

        updReq          = GetNode<HTTPRequest>("../HTTPRequest");
        acceptDialog    = GetNode<AcceptDialog>("../AcceptDialog");
        downloadDialog  = GetNode<PopupPanel>("../DownloadDialog");
        downloadProgress = GetNode<ProgressBar>("../DownloadDialog/CenterContainer/VBoxContainer/ProgressBar");

        downReq.Connect("request_completed", this, "DownloadRequestCompleted");
        downReq.PauseMode = PauseModeEnum.Process;
        AddChild(downReq);

        checkTimer = new Timer();
        checkTimer.Autostart = true;
        checkTimer.OneShot = true;
        checkTimer.WaitTime = 5;
        checkTimer.Connect("timeout", this, "CheckConnection");
        checkTimer.PauseMode = PauseModeEnum.Process;
        AddChild(checkTimer);
        updReq.Connect("request_completed", this, "CheckUpdateRequestCompleted");
        // updReq.Request(Global.versionURL);
    }

    public static bool CheckForInternetConnection()
    {
        try
        {
            using (var client = new System.Net.WebClient())
            using (client.OpenRead("http://google.com.br/"))
                return true;
        }
        catch
        {
            return false;
        }
    }

    void CheckConnection()
    {
        /*if (updReq.GetHttpClientStatus() != HTTPClient.Status.Connected)
        {
            GetTree().Paused = false;
            checkTimer.Stop();
        }*/

        if (CheckForInternetConnection())
        {
            updReq.Request(Global.versionURL);
        }
        else
        {
            GetTree().Paused = false;
        }

        checkTimer.Stop();
    }

    #region DownloadProgress
    private void getting(String domain, String url, int port, bool ssl)
    {
        if (t.IsActive())
            return;
        
        t.Start( this, "_load", new Godot.Collections.Dictionary<string, object>() { {"domain",domain}, {"url",url}, {"port",port}, {"ssl",ssl} }  );
    }

    private Byte[] _load(String domain, String url, int port, bool ssl)
    {
        var err = 0;
        var http = new HTTPClient();
        err = (int)http.ConnectToHost(domain, port, ssl);

        while(http.GetStatus() == HTTPClient.Status.Connecting || http.GetStatus() == HTTPClient.Status.Resolving)
        {
            http.Poll();
            OS.DelayMsec(100);
        }

        string[] headers = {
            "User-Agent: Pirulo/1.0 (Godot)",
            "Accept: */*"
            };

        err = (int)http.Request(HTTPClient.Method.Get, url, headers);

        while (http.GetStatus() == HTTPClient.Status.Requesting)
        {
            http.Poll();
            OS.DelayMsec(500);
        }

        Byte[] rb = new Byte[] {};

        if (http.HasResponse())
        {
            var headers2 = http.GetResponseHeadersAsDictionary();

            while (http.GetStatus() == HTTPClient.Status.Body)
            {
                http.Poll();

                Byte[] chunk = http.ReadResponseBodyChunk();

                if (chunk.Length == 0)
                {
                    OS.DelayUsec(100);
                }
                else
                {
                    // chunk[0] <<= rb;
                    rb = chunk;
                    CallDeferred("_send_loading_signal", new Godot.Collections.Array(rb.Length, http.GetResponseBodyLength()));
                }
            }
        }

        CallDeferred("_send_loaded_signal");
        http.Close();
        return rb;
    }

    private void _send_loading_signal(int l, Thread t)
    {
        EmitSignal("loading",l,t);
    }

    private void _send_loaded_signal()
    {
        var r = t.WaitToFinish();
        EmitSignal("loaded", r);
    }
    #endregion

    private void UpdateDialogOK()
    {
        downReq.DownloadFile = Global.appPath + "/Controle de Peças TI.exe";
        downReq.Request("http://mddev.x10.mx/Controle%20de%20Estoque.exe");

        downloadDialog.PopupCentered();
        SetProcess(true);
    }

    private void CheckUpdateRequestCompleted(int result, int response_code, String[] headers, byte[] body)
    {
        String check_version = null;

        if (result != (int)HTTPRequest.Result.Success)
        {
            updReq.Request("https://controledeestoqueti.000webhostapp.com/version");
        }

        var parser = new XMLParser();
        parser.OpenBuffer(body);

        while (parser.Read() != Error.FileEof)
        {
            check_version = parser.GetNodeData();

            Global.newVersion = check_version;
        }

        if (Global.version != check_version)
        {
            Global.newVersion = check_version;
            DialogText = String.Format("Foi encontrada uma nova versão: {0}\nBaixar atualização?", Global.newVersion);
            PopupCentered();
            GetTree().Paused = false;
        }
        else
        {
            // acceptDialog.DialogText = "O PROGRAMA JÁ ESTÁ ATUALIZADO";
            // acceptDialog.WindowTitle = "ATUALIZADO";
            acceptDialog.PopupCentered();
            GetTree().Paused = false;
            SetProcess(false);
        }
    }

    private void DownloadRequestCompleted(int result, int response_code, String[] headers, byte[] body)
    {
        /*if (result == (int)HTTPRequest.Result.CantResolve)
        {
            GetTree().Paused = false;
            return;
        }*/

        if (result != (int)HTTPRequest.Result.Success)
        {
            downReq.Request("http://controledeestoqueti.000webhostapp.com/Controle%20de%20Estoque.exe");
            GD.Print("Mirror Download");
        }

        if (result == (int)HTTPRequest.Result.Success)
        {
            GetNode<AcceptDialog>("../AcceptDialog").PopupCentered();

            acceptDialog.DialogText = "ATUALIZAÇÃO CONCLUIDA!";
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        var downloadedBytes = downReq.GetDownloadedBytes();
        var totalSize = downReq.GetBodySize();

        downloadProgress.Value = (double)(downloadedBytes * 100.0) / totalSize;
    }
}
