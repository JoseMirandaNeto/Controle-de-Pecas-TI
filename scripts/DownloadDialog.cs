using Godot;
using System;

public class DownloadDialog : PopupPanel
{
    static ProgressBar downloadBar;

    public static int DownloadedBytes { get; set; }
    public static int TotalSize { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        downloadBar = GetNode<ProgressBar>("CenterContainer/VBoxContainer/ProgressBar");

        downloadBar.Value = 0;
    }

    // public static void downloadedBytes(int totalSize, int downloadedBytes)
    // {
    //     var percent = (downloadedBytes / totalSize) / 100;
    //     downloadBar.Value += percent;
    //     // downReq.DownloadChunkSize
    // }

    void OnProgressBarchanged(float value)
    {
        // var percent = (DownloadedBytes * 100) / TotalSize;
        // value += percent;
    }

    // // Called every frame. 'delta' is the elapsed time since the previous frame.
    // public override void _Process(float delta)
    // {
    //     GD.Print(DownloadedBytes);
    //     GD.Print(TotalSize);
    // }
}
