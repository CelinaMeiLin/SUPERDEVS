using Godot;
using System;
using Devs.project.Autoloads;

public partial class tuto : Node2D
{
	
	public int NbCoinsBeforeTutorial;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		NbCoinsBeforeTutorial = (int)UserPreferences.Data["Coin"];
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
