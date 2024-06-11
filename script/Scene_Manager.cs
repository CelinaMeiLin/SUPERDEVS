using Devs.project.Autoloads;
using Devs.project.Ressources;
using Godot;

namespace Devs.project.script;

public partial class Scene_Manager : Control
{
    [Export] Control variable;
	[Export] private AudioStreamPlayer backgroundMusic;
	
	public string PreviousScene = "res://scene/main_menu.tscn";	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		if (backgroundMusic != null)
		{
			backgroundMusic.Play();
		}
		

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("ui_cancel")){
			//variable.Visible=true;
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
	}
	
	public void OnplayPressed()
	{
//		variable.Visible=false;
		GetTree().Paused=false;
		GetNode<Button>("VBoxContainer/Play").Text="Resume";
		var mainchildren=GetParent().GetChildren();
		foreach(var child in mainchildren){
			if(child.Name=="Tuto"){
				return;
			}
		}
		GetTree().ChangeSceneToFile("res://scene/modes.tscn");
		PreviousScene = "res://scene/modes.tscn";
		//var gameScene=GD.Load<PackedScene>("res://scene/modes.tscn");
		//var gameSceneNode=gameScene.Instantiate();
		//GetParent().AddChild(gameSceneNode);
	}
	
	
	private void OnSoloPressed()
	{
        
		if (backgroundMusic != null && backgroundMusic.Playing)
		{
			backgroundMusic.Stop();
			//GD.Print("Musique de fond arrêtée.");
		}

		Visible=false;
		GetTree().Paused = false;
		var mainchildren=GetParent().GetChildren();
		foreach(var child in mainchildren){
			if(child.Name=="Tuto"){
				return;
			}
		}

		GetTree().ChangeSceneToFile("res://scene/map.tscn");
		//var gameScene=GD.Load<PackedScene>("res://scene/tuto.tscn");
		//var gameSceneNode=gameScene.Instantiate();
		//GetParent().AddChild(gameSceneNode);
	}
	
	private void OnMultiPressed()
	{

		Visible=false;
		GetTree().ChangeSceneToFile("res://scene/Server.tscn");
		//var gameScene=GD.Load<PackedScene>("res://scene/Server.tscn");
		//var gameScenebackNode=gameScene.Instantiate();
		//GetParent().AddChild(gameScenebackNode);
	}
	
	
	public void OnInventoryPressed()
	{
		GetTree().ChangeSceneToFile("res://scene/inventory.tscn");
	}
	
	public void OnTutoPressed()
	{
		UserPreferences.Data["CurrentLevel"] = 1;
		UserPreferences.Save();
		GameManager.CurrentLevel = 0;
		GetTree().ChangeSceneToFile("res://scene/Levels/tuto.tscn");
	}

	public void OnLevel1Pressed()
	{
		UserPreferences.Data["CurrentLevel"] = 1;
		UserPreferences.Save();
		GameManager.CurrentLevel = 1;
		GetTree().ChangeSceneToFile("res://scene/Levels/level_1.tscn");
	}
	
	public void OnLevel2Pressed()
	{
		UserPreferences.Data["CurrentLevel"] = 1;
		UserPreferences.Save();
		GameManager.CurrentLevel = 2;
		GetTree().ChangeSceneToFile("res://scene/Levels/level_2.tscn");
	}
	
	public void OnLevel3Pressed()
	{
		UserPreferences.Data["CurrentLevel"] = 1;
		UserPreferences.Save();
		GameManager.CurrentLevel = 3;
		GetTree().ChangeSceneToFile("res://scene/Levels/level_3.tscn");
	}

	private void OnBackPressed()
	{
		Visible=false;
		GetTree().ChangeSceneToFile("res://scene/main_menu.tscn");
		//var gameScene=GD.Load<PackedScene>("res://scene/main_menu.tscn");
		//var gameScenebackNode=gameScene.Instantiate();
		//GetParent().AddChild(gameScenebackNode);

	}
	
	
	public void OnExitPressed()
	{
		GetTree().Quit();
	}
}