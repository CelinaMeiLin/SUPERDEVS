using Godot;
using System;

public partial class PowShot : RigidBody2D
{
	public float BulletDamge = 100;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Timer timer = GetNode<Timer>("Timer");
		timer.Timeout += () => QueueFree();
	}
	
	public void OnBodyEntered(Node2D body)
	{
		if (body is astra)
		{
			if (((astra)body).Astra.Vie <= 0)
			{
				QueueFree();
			}
			((astra)body).hurt(BulletDamge);
		}
		
		QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}