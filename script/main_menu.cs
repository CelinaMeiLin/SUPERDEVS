using Godot;
using System;

public partial class main_menu : Control
{
	[Export] Control variable;
	[Export] private AudioStreamPlayer backgroundMusic;
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
			variable.Visible=true;
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
	}
	
	public void OnplayPressed()
	{
		variable.Visible=false;
		GetTree().Paused=false;
		GetNode<Button>("VBoxContainer/Play").Text="Resume";
		var mainchildren=GetParent().GetChildren();
		foreach(var child in mainchildren){
			if(child.Name=="Tuto"){
				return;
			}
		}
		GetTree().ChangeSceneToFile("res://scene/modes.tscn");
		//var gameScene=GD.Load<PackedScene>("res://scene/modes.tscn");
		//var gameSceneNode=gameScene.Instantiate();
		//GetParent().AddChild(gameSceneNode);
	}
	
	public void OnOptionsPressed()
	{
		Visible= false;
		GetTree().ChangeSceneToFile("res://scene/Options.tscn");
		//var optionscene=GD.Load<PackedScene>("res://scene/Options.tscn");
		//var optionssceneNode=optionscene.Instantiate();
		//GetParent().AddChild(optionssceneNode);
	}
	
	private void OnSoloPressed()
	{
        
		if (backgroundMusic != null && backgroundMusic.Playing)
		{
			backgroundMusic.Stop();
			GD.Print("Musique de fond arrêtée.");
		}

		Visible=false;
		GetTree().Paused = false;
		var mainchildren=GetParent().GetChildren();
		foreach(var child in mainchildren){
			if(child.Name=="Tuto"){
				return;
			}
		}

		GetTree().ChangeSceneToFile("res://scene/Levels/tuto.tscn");
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
	
	public void OnExitPressed()
	{
		GetTree().Quit();
	}
	private void OnBackPressed()
	{
		Visible=false;
		GetTree().ChangeSceneToFile("res://scene/main_menu.tscn");
		//var gameScene=GD.Load<PackedScene>("res://scene/main_menu.tscn");
		//var gameScenebackNode=gameScene.Instantiate();
		//GetParent().AddChild(gameScenebackNode);
		
	}
}
