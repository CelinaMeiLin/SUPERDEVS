using Godot;
using System;
using Devs.project.Autoloads;
using Devs.project.script.Enemies;

public partial class bullet : RigidBody2D
{

	public float BulletDamage = (float)UserPreferences.Data["AttackDamage"];
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Timer timer = GetNode<Timer>("Timer");
		timer.Timeout += () => QueueFree();
	}

	public void OnBodyEntered(Node2D body)
	{
		if (body is GusBody)
		{
			((GusBody)body).hurt(BulletDamage);
		}
		
		else if (body is PowBody)
		{
			((PowBody)body).hurt(BulletDamage);
		}
		
		else if (body is RhustBody)
		{
			((RhustBody)body).hurt(BulletDamage);
		}
		else
		{
			return;
		}
		
		QueueFree();
	}
}
