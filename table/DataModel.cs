using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class DataModel
{
    public int ID { get; set; }
    public string Tipo { get; set; }
    public string Tamanho { get; set; }
    public string Modelo { get; set; }
    public string Pacote { get; set; }
    public int Quantidade { get; set; }
    public bool EmUso { get; set; }
    // public string data { get {return DateTime.Now.Month.ToString();} }

    public DataModel() {}

    // public DataModel(int id, string nome, string tipo, string modelo, int quantia )
    // {
    //     this.ID = id;
    //     this.Tipo = tipo;
    //     this.Tamanho = tamanho;
    //     this.Modelo = modelo;
    //     this.Quantidade = quantia;
    // }
}
