using Godot;
using System;
using Devs.project.Autoloads;

public partial class coin : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	
	public void _on_area_2d_area_entered(Node body)
	{
		if (body is CharacterBody2D)
		{
			Global.Instance.GainCoin(1);
			QueueFree();
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
