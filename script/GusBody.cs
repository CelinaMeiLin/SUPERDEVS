using Godot;
using System;
using System.Collections.Generic;
using Devs.project.script;

public partial class GusBody : CharacterBody2D
{
	Entity Enemy = new Entity(200, 50, 2, 120, -350);
	public bool player_chase = false;
	public CharacterBody2D player = null;
	float temp;
	Vector2 dir;
	private bool direction = false;
	private AnimatedSprite2D _animatedSprite;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("Gus");
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
			velocity.X = dir.X * Enemy.Speed;
			_animatedSprite.Play("run-shoot");
		}
		else
		{
			velocity.X = 0;
			_animatedSprite.Play("idle");
		}
		
		//pour orienter Gus
		_animatedSprite.FlipH = direction;
		//_animatedSprite.FlipH = dir.X < 0;
		
		Velocity = velocity;
		MoveAndSlide();
	}
	
	
	private void _on_detection_area_body_entered(CharacterBody2D body)
	{
		GD.Print("entered");
		player = body;
		temp = player.Position.X - Position.X + 300; //je sais pas d'où sort le décalage de 300 mais ça marche
		GD.Print("player : " + player.Position);
		GD.Print("mob : " + Position);
		GD.Print(temp);
		if (temp > 0)
		{
			dir = Vector2.Right;
			direction = false;
			GD.Print(dir.X);
		}
		else
		{
			dir = Vector2.Left;
			direction = true;
			GD.Print(dir.X);
		}
		player_chase = true;
	}


	private void _on_detection_area_body_exited(CharacterBody2D body)
	{
		GD.Print("exited");
		player_chase = false;
		dir = Vector2.Zero;
		player = null;
	}
}


