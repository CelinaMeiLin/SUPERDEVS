using Godot;
using System;
using Devs.project.Autoloads;

public partial class coin : Sprite2D
{
	private Global _global; 
	
	[Signal]
	public delegate void CoinCollectedEventHandler();
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Coin = 1;
		_global = GetTree().Root.GetNode<Global>("Global");
	}
	
	public void _on_area_2d_body_entered(Node body)
	{
		if (body is CharacterBody2D)
		{
			EmitSignal("CoinCollected");
			//_global.AddScore(1);
			QueueFree();
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
