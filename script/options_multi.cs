using Godot;
using System;
using Devs.project.script;

public partial class options_multi : Control
{
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_back_button_down()
	{
		Visible=false;
		var gameScene=GD.Load<PackedScene>("res://scene/main_menu.tscn");
		var gameScenebackNode=gameScene.Instantiate();
		GetParent().AddChild(gameScenebackNode);
	}
}
