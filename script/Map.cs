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
		GetTree().ChangeSceneToFile("res://scene/Levels/tuto.tscn");
	}

	public void OnLevel1Pressed()
	{
		GetTree().ChangeSceneToFile("res://scene/Levels/level_1.tscn");
	}
	
	public void OnLevel2Pressed()
	{
		GetTree().ChangeSceneToFile("res://scene/Levels/level_2.tscn");
	}
	
	public void OnLevel3Pressed()
	{
		GetTree().ChangeSceneToFile("res://scene/Levels/level_3.tscn");
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
