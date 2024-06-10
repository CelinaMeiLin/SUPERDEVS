using System.IO;
using Devs.project.Autoloads;
using Godot;

namespace Devs.project.Ressources;

public partial class Launcher : Node
{
    public override void _Ready()
    {
        if (!Directory.Exists(UserPreferences.FilePath)) Directory.CreateDirectory(UserPreferences.FilePath);

        string localPath = Path.Join(UserPreferences.FilePath, UserPreferences.FileName);
        if (!File.Exists(localPath))
        {
            //UserPreferences.Data.Add("Name", "Beginner");
            UserPreferences.Data.Add("CurrentLevel", 0);

            UserPreferences.Data.Add("Master", 50);
            UserPreferences.Data.Add("Music", 50);
            UserPreferences.Data.Add("SFX", 50);
            UserPreferences.Save();
        }
        UserPreferences.Load();


        GetTree().ChangeSceneToFile("res://scene/main_menu.tscn");
    }
}