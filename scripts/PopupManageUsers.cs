using Godot;
using System;

public class PopupManageUsers : PopupDialog
{
    private Tree _userList;
    private LineEdit _ledUserName;
    private OptionButton _optUserAccess;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _ledUserName = GetNode("VBoxContainer/GridContainer/HBoxContainer/led_userName") as LineEdit;
        _optUserAccess = GetNode("VBoxContainer/GridContainer/HBoxContainer2/OptionButton") as OptionButton;
        _userList = GetNode("VBoxContainer/GridContainer/ScrollContainer/UserList") as Tree;
    }

    private void _on_Close_pressed()
    {
        this.Hide();
    }

    private void _on_Add_pressed()
    {
        var root = _userList.CreateItem();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
