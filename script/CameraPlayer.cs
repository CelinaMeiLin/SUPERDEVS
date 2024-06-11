using Godot;
using System;

public partial class CameraPlayer : Camera2D
{
	[Export] private CharacterBody2D ObjectToFollow;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Position = ObjectToFollow.Position;
	}
}
