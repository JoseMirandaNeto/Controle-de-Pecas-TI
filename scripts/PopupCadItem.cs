using Godot;
using System;
using System.Collections.Generic;

public class PopupCadItem : PopupDialog
{
    private Button _btn_PopupClose;

    private LineEdit _txtTamanho;
    private LineEdit _txtModelo;
    private LineEdit _txtPacote;
    private SpinBox _spnQuantia;
    private SpinBox _spnLoop;
    private OptionButton _btn_PartsTipo;


    // public override void _Input(InputEvent @event)
    // {
    //     base._Input(@event);

    //     if (this.Visible && @event is InputEventKey keyEvent && keyEvent.Pressed)
    //     {
    //         if ((KeyList)keyEvent.Scancode == KeyList.F8)
    //         {
    //             _spnLoop.Visible = !_spnLoop.Visible;
    //         }
    //     }
    // }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // _btn_CadItem = GetNode("VBoxContainer/PanelContainer/MarginContainer/GridContainer/VBoxContainer/Button") as Button;
        _btn_PopupClose = GetNode("Body/Grid/Action/Close") as Button;

        _txtTamanho = GetNode("Body/Grid/Tamanho/txt_Tamanho") as LineEdit;
        _txtModelo = GetNode("Body/Grid/Modelo/txt_Modelo") as LineEdit;
        _txtPacote = GetNode("Body/Grid/Pacote/txt_Pacote") as LineEdit;
        _spnQuantia = GetNode("Body/Grid/Quantidade/spn_Quantidade") as SpinBox;
        _spnLoop = GetNode("Body/Grid/Quantidade/spn_Loop") as SpinBox;
        _btn_PartsTipo = GetNode("Body/Grid/Tipo/opt_Tipo") as OptionButton;

        // _btn_CadItem.Connect("pressed", this, "_btn_CadItem_pressed");
        // _btn_PopupClose.Connect("pressed", this, "PopUpClosePressed");

        // _spnQuantia.GetLineEdit().GetStylebox("custem_styles/normal").Set("bg_color", Colors.Red);
// 
        _btn_PartsTipo.AddItem("COMPUTADORES");
        _btn_PartsTipo.AddItem("PLACA MÃE");
        _btn_PartsTipo.AddItem("PROCESSADOR");
        _btn_PartsTipo.AddItem("PLACA DE VIDEO");
        _btn_PartsTipo.AddItem("ARMAZENAMENTO");
        _btn_PartsTipo.AddItem("MEMÓRIA RAM");
        _btn_PartsTipo.AddItem("MONITOR");
        _btn_PartsTipo.AddItem("MOUSE");
        _btn_PartsTipo.AddItem("TECLADO");
        _btn_PartsTipo.AddItem("COOLER");
        _btn_PartsTipo.AddItem("FONTE");
        _btn_PartsTipo.AddItem("FITA");
        _btn_PartsTipo.AddItem("PLASTICO BOLHA");
        _btn_PartsTipo.AddItem("PEN-DRIVE");
    }

    private void PopUpClosePressed()
    {
        _txtTamanho.GrabFocus();
        _txtTamanho.Clear();
        _txtModelo.Clear();
        _txtPacote.Clear();
        _spnQuantia.GetLineEdit().Text = "1";
        _btn_PartsTipo.Select(0);

        _spnLoop.Visible = false;
        _spnLoop.GetLineEdit().Text = "1";

        this.Hide();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
