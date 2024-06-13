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
			
			
			UserPreferences.Data.Add("MaxHealth", 100);
			UserPreferences.Data.Add("AttackDamage", 200);
			UserPreferences.Data.Add("FireRate", 0.5f);
			UserPreferences.Data.Add("MovementSpeed", 300);
			UserPreferences.Data.Add("DashCooldown", 1); /// à faire

			

			UserPreferences.Data.Add("Master", 50);
			UserPreferences.Data.Add("Music", 50);
			UserPreferences.Data.Add("SFX", 50);
			UserPreferences.Save();
		}
		UserPreferences.Load();

		var cursor =(Texture) ResourceLoader.Load("res://art/Cursor/Cursor.png");
		Input.SetCustomMouseCursor(cursor, Input.CursorShape.Arrow, new Vector2(128, 128));

		var optionscene=GD.Load<PackedScene>("res://scene/main_menu.tscn");
		var optionssceneNode = optionscene.Instantiate(); 
		AddChild(optionssceneNode);
	}
}
