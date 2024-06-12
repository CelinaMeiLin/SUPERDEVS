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
			UserPreferences.Data.Add("IsLevel", 0);
			UserPreferences.Data.Add("Coin", 0);
			
			
			UserPreferences.Data.Add("Health", 100);
			//UserPreferences.Data.Add("Health", 100);
			//UserPreferences.Data.Add("Health", 100);
			//UserPreferences.Data.Add("Health", 100);
			//UserPreferences.Data.Add("Health", 100);

			

			UserPreferences.Data.Add("Master", 50);
			UserPreferences.Data.Add("Music", 50);
			UserPreferences.Data.Add("SFX", 50);
			UserPreferences.Save();
		}
		UserPreferences.Load();

		var cursor = ResourceLoader.Load("res://art/Cursor/Cursor.png");
		Input.SetCustomMouseCursor(cursor, 0, new Vector2(128, 128));

		var optionscene=GD.Load<PackedScene>("res://scene/main_menu.tscn");
		var optionssceneNode = optionscene.Instantiate(); 
		AddChild(optionssceneNode);
	}
}
