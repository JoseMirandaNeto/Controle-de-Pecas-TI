using Godot;
using Godot.Collections;
using System;

public class Global : Godot.Node
{
    // VARIAVEIS GLOBAIS
    public static Godot.Collections.Array Componentes;
    public static Godot.Collections.Array Toolbar;
    public static Godot.Collections.Array Rows;

    public static Godot.Collections.Array Children;

    public static string appPath { get { return OS.GetExecutablePath().GetBaseDir(); } }
    public static string versionURL = "http://mddev.x10.mx/version";
    public static string newVersion;
    public static string version { get { return (string)GD.Load("res://version.gd").Get("VERSION"); } }
    
    // public static string versionURL = "http://localhost/version";
    // public const string version = "version";

    // private static ConfigFile CF = new ConfigFile();
    // private static String Key = OS.GetUniqueId();
    // private static File file = new File();

    // public static string[] users = new string[] [ "admin", "user"];
    // public static string[] pass = {"123", "12345"};
    // public static int[] permission = new int[] { 0, 1, 2 };

    public static Vector2 WindowSize = new Vector2(1024, 600);

    public static string DataHoje { get { return System.DateTime.Now.ToString("dd-MM-yyyy"); } }

    private static string dataCalendario;
    public static string DataCalendario
    {
        get { return dataCalendario; }
        set { dataCalendario = value; }
    }

    // public override void _Ready()
    // {
    //     if (CF.Load("user://Config.cfg") != Error.Ok)
    //     {
    //         Save();
    //     }
    //     else
    //     {
    //         Load();
    //     }
    // }

    // private void Save()
    // {
    //     file.OpenEncryptedWithPass("user://Config.cfg", File.ModeFlags.Write, Key);

    //     CF.SetValue("App", "UserLogin", users);
    //     CF.SetValue("App", "UserPassword", pass);
    //     CF.SaveEncryptedPass("user://Config.cfg", Key);

    //     file.Close();
    // }

    // private void Load()
    // {
    //     file.OpenEncryptedWithPass("user://Config.cfg", File.ModeFlags.Write, Key);
    //     users = (String[])CF.GetValue("App", "UserLogin", users);
    //     pass = (String[])CF.GetValue("App", "UserPassword", pass);
    // }

    // private Godot.Collections.Dictionary<string, object> SaveD()
    // {
    //     return new Godot.Collections.Dictionary<string, object>()
    //     {
    //         { "User", users },
    //         { "Password", pass },
    //         { "Permission", permission }
    //     };
    // }

    /*public static void MouseCursor(Control.CursorShape shape)
    {
        foreach (Control child in Children)
        {
            ((Control)child).MouseDefaultCursorShape = shape;
        }
    }*/

    // // Called every frame. 'delta' is the elapsed time since the previous frame.
    // public override void _Process(float delta)
    // {

    // }
}
