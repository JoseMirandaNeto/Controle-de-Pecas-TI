using Godot;
using System;

public class LogWriter : Node
{
    private static File file = new File();
    private static string m_exePath = string.Empty;

    public static void LogWrite(string logMessage)
    {
        m_exePath = OS.GetUserDataDir();
        if (!file.FileExists(m_exePath + "\\" + "log.txt"))
        {
            file.Open(m_exePath + "\\" + "log.txt", File.ModeFlags.WriteRead);
        }

        try
        {
            // AppendLog(logMessage, )
        }
        catch (Exception ex)
        {
            GD.Print(ex.Message);
        }
    }

    private static void AppendLog(string logMessage, File txtWriter)
    {
        try
        {
        //     txtWriter.StoreLine("\r\nLog Entry : ");
        //     txtWriter.StoreLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
        //     txtWriter.StoreLine("  :");
        //     txtWriter.StoreLine("  :{0}", logMessage);
        //     txtWriter.StoreLine("-------------------------------");
        }
        catch (Exception ex)
        {
        }
    }
}
