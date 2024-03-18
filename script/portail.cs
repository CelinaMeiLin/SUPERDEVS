using Godot;
using System;

public partial class portail : Sprite2D
{

	private void _on_area_2d_body_entered(Node2D body)
	{
		_Ready();
	}

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Visible=false;
		var gameScene=GD.Load<PackedScene>("res://scene/main_menu.tscn");
		var gameScenebackNode=gameScene.Instantiate();
		GetParent().AddChild(gameScenebackNode);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}


