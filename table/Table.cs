using Godot;
using System;
using System.Collections.Generic;

public class Table : Control
{
    private PackedScene _row = (PackedScene)GD.Load("res://table/Row.scn");
    private VBoxContainer table;

    private int ID = 0;
    private int INDEX = 0;
    private List<int> checkedList = new List<int>();
    // private Label _countLabel;

    // CONECTAR NODES
    private PopupDialog _popUpCadItem;
    private Button _btn_CadItem;

    private LineEdit _txtTamanho;
    private LineEdit _txtModelo;
    private LineEdit _txtPacote;
    private SpinBox _spnQuantia;
    private SpinBox _spnLoop;
    private OptionButton _btn_PartsTipo;

    // COMPONENTES

    private TextureButton pc;
    private TextureButton mboard;
    private TextureButton cpu;
    private TextureButton gpu;
    private TextureButton storage;
    private TextureButton ram;
    private TextureButton monitor;
    private TextureButton mouse;
    private TextureButton kboard;
    private TextureButton cooler;
    private TextureButton psu;
    private TextureButton pendrive;
    private TextureButton fita;
    private TextureButton bolha;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Global.Componentes = GetTree().GetNodesInGroup("components");

        table = GetNode("VBoxContainer/PanelContainer2/ScrollContainer/VBoxContainer/") as VBoxContainer;

        _popUpCadItem = GetNode("../PanelContainer/Toolbar/CadastroMenuBtn/PopupCadItem") as PopupDialog;
        _btn_CadItem = _popUpCadItem.GetNode("Body/Grid/Action/Add") as Button;
        _txtTamanho = _popUpCadItem.GetNode("Body/Grid/Tamanho/txt_Tamanho") as LineEdit;
        _txtModelo = _popUpCadItem.GetNode("Body/Grid/Modelo/txt_Modelo") as LineEdit;
        _txtPacote = _popUpCadItem.GetNode("Body/Grid/Pacote/txt_Pacote") as LineEdit;
        _spnQuantia = _popUpCadItem.GetNode("Body/Grid/Quantidade/spn_Quantidade") as SpinBox;
        _spnLoop = _popUpCadItem.GetNode("Body/Grid/Quantidade/spn_Loop") as SpinBox;
        _btn_PartsTipo = _popUpCadItem.GetNode("Body/Grid/Tipo/opt_Tipo") as OptionButton;

        _btn_CadItem.Connect("pressed", this, "_btn_CadItem_pressed");

        LoadDB();
        LoadNodes();
    }

    // public override void _Notification(int notification)
    // {
    //     base._Notification(notification);

    //     // Save on quit. Note that you can call `DataManager.Save()` whenever you want
    //     if (notification == MainLoop.NotificationWmQuitRequest)
    //     {
    //         Database.Save();
    //         GetTree().Quit();
    //     }
    // }

    #region CARREGAR NODES
    private void LoadNodes()
    {
        pc = GetNode<TextureButton>("../Componentes/Panel/VBoxContainer/TextureButton");
        pc.Connect("pressed", this, "ComputadoresPressed");

        mboard = GetNode<TextureButton>("../Componentes/Panel1/VBoxContainer/TextureButton");
        mboard.Connect("pressed", this, "MotherPressed");

        cpu = GetNode<TextureButton>("../Componentes/Panel5/VBoxContainer/TextureButton");
        cpu.Connect("pressed", this, "ProcessorPressed");

        gpu = GetNode<TextureButton>("../Componentes/Panel7/VBoxContainer/TextureButton");
        gpu.Connect("pressed", this, "GraphicPressed");

        storage = GetNode<TextureButton>("../Componentes/Panel9/VBoxContainer/TextureButton");
        storage.Connect("pressed", this, "StoragePressed");

        ram = GetNode<TextureButton>("../Componentes/Panel2/VBoxContainer/TextureButton");
        ram.Connect("pressed", this, "RamPressed");

        monitor = GetNode<TextureButton>("../Componentes/Panel4/VBoxContainer/TextureButton");
        monitor.Connect("pressed", this, "MonitorPressed");

        mouse = GetNode<TextureButton>("../Componentes/Panel3/VBoxContainer/TextureButton");
        mouse.Connect("pressed", this, "MousePressed");

        kboard = GetNode<TextureButton>("../Componentes/Panel8/VBoxContainer/TextureButton");
        kboard.Connect("pressed", this, "KboardPressed");

        cooler = GetNode<TextureButton>("../Componentes/Panel6/VBoxContainer/TextureButton");
        cooler.Connect("pressed", this, "CoolerPressed");

        psu = GetNode<TextureButton>("../Componentes/Panel10/VBoxContainer/TextureButton");
        psu.Connect("pressed", this, "PowerSupplyPressed");

        pendrive = GetNode<TextureButton>("../Componentes/Panel13/VBoxContainer/TextureButton");
        pendrive.Connect("pressed", this, "PendrivePressed");

        fita = GetNode<TextureButton>("../Componentes/Panel11/VBoxContainer/TextureButton");
        fita.Connect("pressed", this, "FitaPressed");

        bolha = GetNode<TextureButton>("../Componentes/Panel12/VBoxContainer/TextureButton");
        bolha.Connect("pressed", this, "BolhaPressed");
    }
    #endregion

    #region FILTRO DE COMPONENTES

    private void ComputadoresPressed()
    {
        LimparTabela();

        // foreach (var item in Database.GetParts())
        for (int item = 0; item < Global.Rows.Count; item++)
        {
            if (Database._dataL[item].Tipo == "COMPUTADORES" && Database._dataL[item] != null)
            {
                // ID = item.ID+1;

                table.AddChild(((Global.Rows[item]) as HBoxContainer));
            }
        }
    }

    private void MotherPressed()
    {
        LimparTabela();

        foreach (var item in Database.GetParts())
        {
            if (item.Tipo == "PLACA MÃE" && item!=null)
            {
                table.AddChild(((Global.Rows[item.ID-1]) as HBoxContainer));
            }
        }
    }

    private void ProcessorPressed()
    {
        LimparTabela();

        foreach (var item in Database.GetParts())
        {
            if (item.Tipo == "PROCESSADOR")
            {
                table.AddChild(((Global.Rows[item.ID-1]) as HBoxContainer));
            }
        }
    }

    private void GraphicPressed()
    {
        LimparTabela();

        foreach (var item in Database.GetParts())
        {
            if (item.Tipo == "PLACA DE VIDEO" && item.Quantidade >= 1)
            {
                table.AddChild(((Global.Rows[item.ID-1]) as HBoxContainer));
            }
        }
    }

    private void StoragePressed()
    {
        LimparTabela();

        foreach (var item in Database.GetParts())
        {
            if (item.Tipo == "ARMAZENAMENTO")
            {
                table.AddChild(((Global.Rows[item.ID-1]) as HBoxContainer));
            }
        }
    }

    private void RamPressed()
    {
        LimparTabela();

        foreach (var item in Database.GetParts())
        {
            if (item.Tipo == "MEMÓRIA RAM")
            {
                table.AddChild(((Global.Rows[item.ID-1]) as HBoxContainer));
            }
        }
    }

    private void MonitorPressed()
    {
        LimparTabela();

        foreach (var item in Database.GetParts())
        {
            if (item.Tipo == "MONITOR")
            {
                table.AddChild(((Global.Rows[item.ID-1]) as HBoxContainer));
            }
        }
    }

    private void MousePressed()
    {
        LimparTabela();

        foreach (var item in Database.GetParts())
        {
            if (item.Tipo == "MOUSE")
            {
                table.AddChild(((Global.Rows[item.ID-1]) as HBoxContainer));
            }
        }
    }

    private void KboardPressed()
    {
        LimparTabela();

        foreach (var item in Database.GetParts())
        {
            if (item.Tipo == "TECLADO")
            {
                table.AddChild(((Global.Rows[item.ID-1]) as HBoxContainer));
            }
        }
    }

    private void CoolerPressed()
    {
        LimparTabela();

        foreach (var item in Database.GetParts())
        {
            if (item.Tipo == "COOLER")
            {
                table.AddChild(((Global.Rows[item.ID-1]) as HBoxContainer));
            }
        }
    }

    private void PowerSupplyPressed()
    {
        LimparTabela();

        foreach (var item in Database.GetParts())
        {
            if (item.Tipo == "FONTE")
            {
                table.AddChild(((Global.Rows[item.ID-1]) as HBoxContainer));
            }
        }
    }

    private void PendrivePressed()
    {
        LimparTabela();

        foreach (var item in Database.GetParts())
        {
            if (item.Tipo == "PEN-DRIVE")
            {
                table.AddChild(((Global.Rows[item.ID-1]) as HBoxContainer));
            }
        }
    }

    private void FitaPressed()
    {
        LimparTabela();

        foreach (var item in Database.GetParts())
        {
            if (item.Tipo == "FITA")
            {
                table.AddChild(((Global.Rows[item.ID-1]) as HBoxContainer));
            }
        }
    }

    private void BolhaPressed()
    {
        LimparTabela();

        foreach (var item in Database.GetParts())
        {
            if (item.Tipo == "PLASTICO BOLHA")
            {
                table.AddChild(((Global.Rows[item.ID-1]) as HBoxContainer));
            }
        }
    }
    #endregion

    private void LoadDB()
    {
        for (var item = 0; item < Database._dataL.Count; item++)
        {
            // Database._dataL[item].ID = item;
            if (Database._dataL[item] != null)
            {
                Setdata(Database._dataL[item]);
            }
        }

        UpdateLabels();
    }

    private void UpdateLabels()
    {
        foreach (Label item in Global.Componentes)
        {
            item.Text = Database.GetQuantia(item.Name).ToString();
        }
    }

    private void LimparTabela()
    {
        // ID = 0;
        INDEX = 0;

        if (Global.Rows != null)
        {
            foreach (HBoxContainer row in table.GetChildren())
            {
                table.RemoveChild((HBoxContainer)row);
                // row.QueueFree();
            }
        }
    }

    private void OrdenarEmUso()
    {
        int ordem = 0;
        for (var item = 0; item < Database._dataL.Count; item++)
        {
            if (Database._dataL[item].EmUso)
            {
                table.MoveChild(((Global.Rows[item]) as HBoxContainer), ordem);
                ordem = ordem+1;
            }
        }
    }

    public void UpdateList()
    {
        LimparTabela();

        ID = 0;

        for (var item = 0; item < Database._dataL.Count; item++)
        {
            Database._dataL[item].ID = item+1;

            if (Database._dataL[item] != null)
            {
                Setdata(Database._dataL[item]);
            }

            if (table.GetNode<CheckBox>(item + 1 + "/HBoxContainer1/CheckBox").Pressed == true)
            {
                table.MoveChild(((Global.Rows[item]) as HBoxContainer), INDEX);
                INDEX = INDEX + 1;
            }
            else
            {
                if (INDEX != 0)
                {
                    INDEX = INDEX - 1;
                }
                table.MoveChild(((Global.Rows[item + 1 ]) as HBoxContainer), INDEX);
            }

            // if (Database._dataL[item].EmUso == true)
            // {
            //     table.MoveChild(((Global.Rows[item]) as HBoxContainer), INDEX);
            //     INDEX = INDEX+1;
            // }
        }

        // Global.Rows.Clear();

        // GD.Print("ID:" + ID);
    }

    private void Setdata(DataModel data)
    {
        ID = ID + 1;

        // var tabela = GetNode("VBoxContainer/PanelContainer2/ScrollContainer/VBoxContainer");
        var instance = _row.Instance() as HBoxContainer;

        instance.Name = Convert.ToString(ID);

        // instance.AddColorOverride("bg_color", new Color(1,0,0));

        table.AddChild(instance);

        instance.Connect("deletarPressed", this, "_on_Deletar_pressed");
        instance.Connect("incrementarPressed", this, "_on_Incrementar_pressed", new Godot.Collections.Array() {+1} );
        instance.Connect("reduzirPressed", this, "_on_Reduzir_pressed", new Godot.Collections.Array() {-1} );
        instance.Connect("checkEmUsoPressed", this, "_on_CheckBox_toggled");
        
        instance.AddToGroup("row");
        Global.Rows = GetTree().GetNodesInGroup("row");

        table.GetNode<Label>(instance.Name + "/1").Text = Convert.ToString(data.ID);
        table.GetNode<Label>(instance.Name + "/2").Text = data.Tipo;
        table.GetNode<Label>(instance.Name + "/3").Text = data.Tamanho;
        table.GetNode<Label>(instance.Name + "/4").Text = data.Modelo;
        table.GetNode<Label>(instance.Name + "/5").Text = data.Pacote;
        table.GetNode<Label>(instance.Name + "/6").Text = Convert.ToString(data.Quantidade);
        table.GetNode<CheckBox>(instance.Name + "/HBoxContainer1/CheckBox").Pressed = data.EmUso;
    }

    private void _btn_CadItem_pressed()
    {
        DataModel item = new DataModel()
        {
            ID = ID + 1,
            Tipo = _btn_PartsTipo.Text,
            Tamanho = _txtTamanho.Text,
            Modelo = _txtModelo.Text,
            Pacote = _txtPacote.Text,
            Quantidade = _spnQuantia.GetLineEdit().Text.ToInt()
        };

        if (item != null)
        {
            Database._dataL.Add(item);
            Setdata(item);
        }

        UpdateLabels();

        _popUpCadItem.Hide();
        _txtTamanho.Clear();
        _txtModelo.Clear();
        _txtPacote.Clear();
        _spnQuantia.GetLineEdit().Text = "1";
        _btn_PartsTipo.Select(0);

        _spnLoop.Visible = false;
        _spnLoop.GetLineEdit().Text = "1";
    }

    private void _on_Deletar_pressed(int index)
    {
        ID = ID-1;
        Database._dataL.RemoveAt(index-1);

        table.RemoveChild((HBoxContainer)Global.Rows[index-1]);

        Global.Rows.RemoveAt(index-1);

        // ((Global.Rows[index-1]) as HBoxContainer).QueueFree();

        // foreach (var item in Database._dataL)
        for (int item = 0; item < Database._dataL.Count; item++)
        {
            Database._dataL[item].ID = item+1;

            table.GetChild(item).Name = Database._dataL[item].ID.ToString();
            table.GetChild(item).GetNode<Label>("1").Text = Database._dataL[item].ID.ToString();
            // ((Global.Rows[item]) as HBoxContainer).Name = Database._dataL[item].ID.ToString();
            // ((Global.Rows[item]) as HBoxContainer).GetNode<Label>("1").Text = Database._dataL[item].ID.ToString();
        }

        // UpdateList();
        UpdateLabels();
    }

    private void _on_Incrementar_pressed(int nome, int index)
    {
        Database.AddQuantia(nome, index);

        int result = Int16.Parse(((Global.Rows[nome-1]) as HBoxContainer).GetNode<Label>("6").Text);
        result+=1;
        ((Global.Rows[nome-1]) as HBoxContainer).GetNode<Label>("6").Text = result.ToString();
        UpdateLabels();
    }

    private void _on_Reduzir_pressed(int nome, int index)
    {
        Database.AddQuantia(nome, index);

        // if (table.GetNode<Label>(nome + "/6").Text == "1")
        if (((Global.Rows[nome - 1]) as HBoxContainer).GetNode<Label>("6").Text == "1")
        {
            // ID = ID - 1;
            // Database._dataL.RemoveAt(nome - 1);
            // ((Global.Rows[nome - 1]) as HBoxContainer).QueueFree();
            // UpdateList();
            // UpdateLabels();

            ID = ID + 1;
            Database._dataL.RemoveAt(nome - 1);

            table.RemoveChild((HBoxContainer)Global.Rows[nome - 1]);

            Global.Rows.RemoveAt(nome - 1);

            for (int item = 0; item < Global.Rows.Count; item++)
            {
                Database._dataL[item].ID = item + 1;

                table.GetChild(item).Name = Database._dataL[item].ID.ToString();
                table.GetChild(item).GetNode<Label>("1").Text = Database._dataL[item].ID.ToString();
            }
        }
        else
        {
            int result = Int16.Parse(((Global.Rows[nome - 1]) as HBoxContainer).GetNode<Label>("6").Text);
            result -= 1;

            ((Global.Rows[nome - 1]) as HBoxContainer).GetNode<Label>("6").Text = result.ToString();
        }

        UpdateLabels();
        
        // LimparTabela();
        // Global.Rows = GetTree().GetNodesInGroup("row");
    }

    private void _on_CheckBox_toggled(int nome)
    {
        Database._dataL[nome-1].EmUso = table.GetNode<CheckBox>(nome + "/HBoxContainer1/CheckBox").Pressed;

        // SUBIR PARA O TOPO MARCADOS
        /*if (table.GetNode<CheckBox>(nome + "/HBoxContainer1/CheckBox").Pressed == true)
        {
            table.MoveChild(((Global.Rows[nome-1]) as HBoxContainer), INDEX);
            INDEX = INDEX+1;
        }
        else
        {
            if (INDEX != 0)
            {
                INDEX = INDEX-1;
            }
            table.MoveChild(((Global.Rows[nome-1]) as HBoxContainer), INDEX);
        }*/
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
