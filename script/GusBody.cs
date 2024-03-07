using Godot;
using System;
using System.Collections.Generic;
using Devs.project.script;

public partial class GusBody : CharacterBody2D
{
	Entity Enemy = new Entity(200, 50, 2, 120, -350);
	public bool player_chase = false;
	public CharacterBody2D player = null;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		if (!IsOnFloor())
		{
			velocity.Y += Enemy.gravity * (float)delta;
		}

		if (player_chase)
		{
			velocity += (player.Position - Position) / Enemy.Speed; //mouvement pas bon :(
		}
		
		Velocity = velocity;
		MoveAndSlide();
	}
	
	
	private void _on_detection_area_body_entered(CharacterBody2D body)
	{
		player = body;
		player_chase = true;
	}


	private void _on_detection_area_body_exited(CharacterBody2D body)
	{
		player_chase = false;
		player = null;
	}
}


