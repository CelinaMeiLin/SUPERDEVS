using Godot;
using System;

public partial class modes : Control
{ 
	[Export] private AudioStreamPlayer backgroundMusic;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{


	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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
		var gameScene=GD.Load<PackedScene>("res://scene/tuto.tscn");
		var gameSceneNode=gameScene.Instantiate();
		GetParent().AddChild(gameSceneNode);
	}
	
	private void OnBackPressed()
	{
		Visible=false;
		var gameScene=GD.Load<PackedScene>("res://scene/main_menu.tscn");
		var gameScenebackNode=gameScene.Instantiate();
		GetParent().AddChild(gameScenebackNode);
		
	}
	
	private void OnMultiPressed()
	{

		Visible=false;
		var gameScene=GD.Load<PackedScene>("res://scene/Server.tscn");
		var gameScenebackNode=gameScene.Instantiate();
		GetParent().AddChild(gameScenebackNode);
	}
}
