using Godot;
using System;

public class Row : HBoxContainer
{
    [Signal]
    delegate void deletarPressed();

    [Signal]
    delegate void incrementarPressed(int index);

    [Signal]
    delegate void reduzirPressed(int index);

    [Signal]
    delegate void checkEmUsoPressed();

    private void _on_Deletar_pressed()
    {
        EmitSignal("deletarPressed", this.Name);
        this.QueueFree();
    }

    private void _on_Incrementar_pressed()
    {
        EmitSignal("incrementarPressed", this.Name);
    }

    private void _on_Reduzir_pressed()
    {
        EmitSignal("reduzirPressed", this.Name);
    }

    private void _on_CheckBox_toggled()
    {
        EmitSignal("checkEmUsoPressed", this.Name);
    }

    // // Called when the node enters the scene tree for the first time.
    // public override void _Ready()
    // {
        
    // }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
