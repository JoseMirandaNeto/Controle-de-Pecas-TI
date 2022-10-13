using Godot;
using System;

public class Menu : Control
{
    private LineEdit ledUser;
    private LineEdit ledPass;
    private RichTextLabel lbl_log;

    private Vector2 click_pos;
    private int clickRadius = 32;
    private bool dragging;

    private bool connected;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        OS.WindowSize = new Vector2(500,500);
        SetGlobalPosition(OS.MaxWindowSize/2);
        OS.CenterWindow();
        // OS.WindowBorderless = true;

        ledUser = GetNode<LineEdit>("Body/Grid/Login/txt_Login");
        ledPass = GetNode<LineEdit>("Body/Grid/Senha/txt_Senha");
        
        ledUser.GrabFocus();

        lbl_log = GetNode<RichTextLabel>("CenterContainer/rtxt_log");

        // GetNode<Label>("Label").Text = OS.GetExecutablePath().GetBaseDir();
        // GetNode<Label>("Label").Text = GetNode<Label>("Label").Text + (String)GD.Load("res://version.gd").Get("VERSION");
        // GetNode<Label>("Label").Text = GetNode<Label>("Label").Text + Global.version;

        // Global.WindowSize = _windowSize;
    }

    public void TitleBarInput(InputEvent inputEvent)
    {
        base._Input(inputEvent);

        if (inputEvent is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
        {
            if (mouseEvent.ButtonIndex == 1)
            {
                dragging = !dragging;
                click_pos = GetLocalMousePosition();
            }
        }
    }

    private void ClosePressed()
    {
        GetTree().Quit();
    }

    private void MaximizePressed()
    {
        OS.WindowMaximized = !OS.WindowMaximized;
    }

    public async void ConnectPressed()
    {
        var t = GetNode<Timer>("Timer");

        // Vector2 size = lbl_log.GetFont("bold_font").GetStringSize(lbl_log.Text + ledUser.Text);
        // Vector2 sizeE = lbl_log.GetFont("bold_font").GetStringSize(lbl_log.Text);

        lbl_log.Visible = true;

        if (ledUser.Text == "admin" && ledPass.Text == "admin")
        {
            lbl_log.BbcodeText = String.Format("CONECTADO COMO: [b]{0}[/b]", ledUser.Text.ToUpper());
            lbl_log.RectMinSize = lbl_log.GetFont("bold_font").GetStringSize(lbl_log.Text);

            connected = true;

            // t.Start();
            // await ToSignal(t, "timeout");
        }
        else
        {
            // lbl_log.RectMinSize = sizeE; // SET SIZE TO CENTER
            lbl_log.BbcodeText = String.Format("[b][color=red]ACESSO NEGADO![/color][/b]");
            lbl_log.RectMinSize = lbl_log.GetFont("bold_font").GetStringSize(lbl_log.Text);
        }

        GetTree().Paused = true;

        t.Start();
        await ToSignal(t, "timeout");
    }

    public void TimerTimeout()
    {
        GetTree().Paused = false;
        // Global.MouseCursor((int)CursorShape.Arrow);

        if (connected)
            GetTree().ChangeScene("res://Main.scn");
        else return;
    }

    private void UpdateFinished()
    {
        // GetTree().Paused = false;

        var dlgResult = GetNode<AcceptDialog>("AcceptDialog");

        if (dlgResult.DialogText == "ATUALIZAÇÃO CONCLUIDA!")
        {
            GetTree().Quit();
        }

        dlgResult.Hide();
    }

    private void txtSenha_guiInput(InputEvent @event)
    {
        if (@event is InputEventKey eventKey)
            if (eventKey.Pressed && eventKey.Scancode == (int)KeyList.Enter || eventKey.Scancode == (int)KeyList.KpEnter)
                ConnectPressed();
    }

    // // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (dragging)
        {
            OS.WindowPosition = OS.WindowPosition + GetGlobalMousePosition() - click_pos;
        }
    }
}