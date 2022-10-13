using Godot;
using IronXL;
using Newtonsoft.Json;
using System.Collections.Generic;

public class  Database : Godot.Node
{
    private static string SAVE_PATH { get {return "user://data.json"; } }
    // private static string SAVE_PATH { get {return "user://data " + Global.DataHoje + ".json"; }}
    //private static readonly string SAVE_PATH = "res://data " + Global.DataHoje + ".json";

    public static DataModel _data = new DataModel();
    public static List<DataModel> _dataL = new List<DataModel>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ReadSaveFile();
    }

    public static void Save()
    {
        WriteSaveFile();
    }

    public void ReadSaveFile()
    {
        string jsonString = null;
        var saveFile = OpenSaveFile(File.ModeFlags.Read);
        if (saveFile != null)
        {
            try
            {
                while (!saveFile.EofReached())
                {
                    jsonString = saveFile.GetLine();
                    if (saveFile != null)
                    {
                        _dataL.Add(Deserialize(jsonString));
                        _dataL.RemoveAll(s => s == null);
                    }
                }
                saveFile.Close();
            }
            catch
            {
                // Careful here! This will create new save data if something went wrong with reading the file. You may want to handle this case differently.
                if (saveFile != null)
                {
                    _dataL.Add(_data);
                }
            }
        }
    }
    
    public static void AddQuantia(int nome, int index)
    {
        foreach (var part in GetParts())
        {
            if (part.ID == nome)
            {
                part.Quantidade += index;
            }
        }
    }

    public static int GetQuantia(string tipo)
    {
        int quantia = 0;
        foreach (var part in GetParts())
        {
            if (part.Tipo == tipo)
            {
                quantia += part.Quantidade;
            }
        }
        return quantia;
    }

    public static int GetLastIndex()
    {
        int lastIndex = 0;
        foreach (var item in GetParts())
        {
            lastIndex = item.ID -1;
        }
        return lastIndex;
    }

    public static List<DataModel> GetParts()
    {
        return _dataL; // I like to create a new list so we aren't passing the "source of truth" list as a reference
    }

    private static void WriteSaveFile()
    {
        var saveFile = OpenSaveFile(File.ModeFlags.Write);
        if (saveFile != null)
        {
            foreach (DataModel item in _dataL)
            // for (var item = 0; item < _dataL.Count; item++)
            {
                if (item != null)
                {
                    var json = JsonConvert.SerializeObject(item);
                    saveFile.StoreLine(json);
                }
            }
            saveFile.Close();
        }
    }

    private static File OpenSaveFile(File.ModeFlags flag = File.ModeFlags.Read)
    {
        var saveFile = new File();

        var err = saveFile.Open(SAVE_PATH, flag);

        if (err == 0)
        {
            return saveFile;
        }
        return null;
    }

    private static DataModel Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<DataModel>(json);
    }
}