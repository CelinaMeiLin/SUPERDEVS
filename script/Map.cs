using Godot;
using System;

public partial class Map : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	
	public void OnInventoryPressed()
	{
		GetTree().ChangeSceneToFile("res://scene/inventory.tscn");
	}
	
	public void OnTutoPressed()
	{
		GetTree().ChangeSceneToFile("res://scene/tuto.tscn");
	}

	public void OnLevel1Pressed()
	{
		GetTree().ChangeSceneToFile("res://scene/level_1.tscn");
	}
	
	public void OnOptionsPressed()
	{
		GetTree().ChangeSceneToFile("res://scene/options.tscn");
	}
	
	public void OnExitPressed()
	{
		GetTree().Quit();
	}
	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
