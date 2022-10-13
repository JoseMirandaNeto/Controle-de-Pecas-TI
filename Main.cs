using Godot;
using System;

public class Main : Control
{
    private Control table;
    private MenuButton _cadastroMenuClick;
    private MenuButton _consultaMenuClick;
    private MenuButton _BackUpMenuClick;

    private PopupDialog _cadastroMenuBtn0;

    private bool dragging;
    private Vector2 click_pos;

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (@event is InputEventKey keyEvent && keyEvent.Pressed)
        {
            switch ((KeyList)keyEvent.Scancode)
            {
                case KeyList.I:
                    if (keyEvent.Control)
                        GetNode<PopupDialog>("VBoxContainer/PanelContainer/Toolbar/CadastroMenuBtn/PopupCadItem").PopupCentered();
                    break;
            }

            if ((KeyList)keyEvent.Scancode == KeyList.F11)
            {
                MaximizePressed();
            }
        }
    }

    public override void _Notification(int notification)
    {
        base._Notification(notification);

        // Save on quit. Note that you can call `DataManager.Save()` whenever you want
        if (notification == MainLoop.NotificationWmQuitRequest)
        {
            Database.Save();
            GetTree().Quit();
        }
    }

    private void TitleBarInput(InputEvent inputEvent)
    {
        base._Input(inputEvent);

        if (inputEvent is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
        {
            if (!OS.WindowFullscreen && mouseEvent.ButtonIndex == 1)
            {
                dragging = !dragging;
                click_pos = GetLocalMousePosition();
            }
        }
    }

    private void ClosePressed()
    {
        Database.Save();
        GetTree().Quit();
    }

    private void MaximizePressed()
    {
        OS.WindowMaximized = !OS.WindowMaximized;
    }

    public override void _Ready()
    {
        // var calendar_button_node = GetNode("VBoxContainer/PanelContainer/Toolbar/HBoxContainer/CalendarButton");
        // calendar_button_node.Call("")
        
        table = GetNode("VBoxContainer/Table") as Control;

        _cadastroMenuClick = GetNode("VBoxContainer/PanelContainer/Toolbar/CadastroMenuBtn") as MenuButton;
        _cadastroMenuClick.GetPopup().Connect("id_pressed", this, "CadastroMenuBtnPressed");

        _consultaMenuClick = GetNode("VBoxContainer/PanelContainer/Toolbar/ConsultaMenuBtn") as MenuButton;
        _consultaMenuClick.GetPopup().Connect("id_pressed", this, "ConsultaMenuBtnPressed");

        _BackUpMenuClick = GetNode("VBoxContainer/PanelContainer/Toolbar/BackUpMenuBtn") as MenuButton;
        _BackUpMenuClick.GetPopup().Connect("id_pressed", this, "BackUpMenuBtnPressed");

        // Global.Toolbar = GetTree().GetNodesInGroup("toolbar");
        // _cadastroMenuBtn0 = _cadastroMenuClick.GetNode("PopupCadItem") as PopupDialog;

        // foreach (MenuButton item in Global.Toolbar)
        // {
        //     if (item.Name == "CadastroMenuBtn")
        //     {
        //         _cadastroMenuBtn0 = GetNode(item.GetPath()) as PopupDialog;
        //         item.GetPopup().Connect("id_pressed", this, "OnCadMenuBtnPressed");
        //     }
        // }

	    // calendar_button_node.Connect("date_selected", this, "_on_CalendarButton_date_selected");

        OS.WindowSize = Global.WindowSize;
        OS.CenterWindow();
    }
    
    private void _on_CalendarButton_date_selected(Godot.Object date_obj)
    {
        var data = date_obj.Call("date", "DD-MM-YYYY");
        Global.DataCalendario = (string)data;
    }

    private void CadastroMenuBtnPressed(int index)
    {
        if (index == 0)
        {
            GetNode<PopupDialog>("VBoxContainer/PanelContainer/Toolbar/CadastroMenuBtn/PopupCadItem").PopupCentered();
        }

        if (index == 1)
        {
            GetNode<PopupDialog>("VBoxContainer/PanelContainer/Toolbar/CadastroMenuBtn/PopupUsers").PopupCentered();
        }
    }

    private void ConsultaMenuBtnPressed(int index)
    {
        if (index == 0)
        {
            table.Call("UpdateList");
        }
    }

    private void BackUpMenuBtnPressed(int index)
    {
        if (index == 0)
        {
            Database.Save();
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (!OS.WindowFullscreen && dragging)
        {
            OS.WindowPosition = OS.WindowPosition + GetGlobalMousePosition() - click_pos;
        }
    }
}
