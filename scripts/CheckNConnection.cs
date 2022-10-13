using System;
using Godot;

public class CheckNConnection : HTTPRequest
{
    [Signal]
    delegate void ConnectionFailed();

    [Signal]
    delegate void ConnectionSucceeded();

    Timer checkTimer;

    /*
    private void OnConnectionFailed()
    {
        EmitSignal("ConnectionFailed");
    }

    private void OnSuccess()
    {
        EmitSignal("ConnectionSucceeded");
    }*/

    private void OnSuccess()
    {
        GD.Print("Signal");
        EmitSignal("ConnectionSucceeded");
    }

    public override void _Ready()
    {
        checkTimer = new Timer();
        checkTimer.Autostart = true;
        checkTimer.OneShot = false;
        checkTimer.WaitTime = 3;
        checkTimer.Connect("timeout", this, "CheckConnection");
        // AddChild(checkTimer);
        Connect("request_completed", this, "OnRequestResult");
    }

    void StopCheck()
    {
        if (!checkTimer.IsStopped())
        {
            checkTimer.Stop();
        }
    }

    void StartCheck()
    {
        if (checkTimer.IsStopped())
        {
            checkTimer.Start();
        }
    }

    void CheckConnection()
    {
        Request("http://www.google.com.br");
    }

    void OnRequestResult(int result, int response_code, String[] headers, byte[] body)
    {
        switch (result)
        {
            case (int)Result.Success:
                GD.Print("Conectado");
                StopCheck();
                break;
            case (int)Result.CantConnect:
                EmitSignal("ConnectionFailed");
                break;
        }
    }
}