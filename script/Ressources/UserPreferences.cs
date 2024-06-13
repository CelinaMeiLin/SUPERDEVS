using Godot;
using System;
using System.IO;
using Godot.Collections;

namespace Devs.project.Autoloads;

public partial class UserPreferences : Node
{
    public static string FileName { get; } = "UserPreferences.json";
    public static string FilePath { get; } = ProjectSettings.GlobalizePath("user://");
    public static Dictionary Data { get; set; } = new Dictionary();
    
    public static void Load()
    {
        string data = String.Empty;
        string localPath = Path.Join(FilePath, FileName);

        try 
        {
            data = File.ReadAllText(localPath);
        }
        catch (Exception e)
        {
            GD.Print("Load data error : " + e);
        }

        Json json = new Json();
        Error error = json.Parse(data);

        if(error != Error.Ok)
        {
            GD.Print("Parsing data error : " + error);
        }

        Data = (Dictionary)json.Data;
    }

    public static void Save()
    {
        string localPath = Path.Join(FilePath, FileName);

        try 
        {
            File.WriteAllText(localPath, Json.Stringify(Data));
        }
        catch (Exception e)
        {
            GD.Print("Save data error : " + e);
        }
    }

    public static void Delete()
    {
        string localPath = Path.Join(FilePath, FileName);

        try 
        {
            File.Delete(localPath);
        }
        catch (Exception e)
        {
            GD.Print("Delete data error : " + e);
        }
    }
    
}