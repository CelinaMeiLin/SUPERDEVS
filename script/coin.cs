using Godot;
using System;
using Devs.project.Autoloads;
using Devs.project.Ressources;

public partial class coin : Sprite2D
{
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	public void _on_area_2d_body_entered(Node body)
	{
		if (body is CharacterBody2D)
		{
			UserPreferences.Data["Coin"] = (int)UserPreferences.Data["Coin"] + 1;
			PauseManager.Coincollectedinlevel++;
			//_global.AddScore(1);
			QueueFree();
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
