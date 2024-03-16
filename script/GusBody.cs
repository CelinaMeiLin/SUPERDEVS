using Godot;
using System;
using System.Collections.Generic;
using Devs.project.script;

public partial class GusBody : CharacterBody2D
{
	//------------------------------- V a r i a b l e s ------------------------------------------//
	// Get Node2D Parent
	[Export] public Node2D Character;
	public Vector2 baseposition;
	
	// Gus Variables
	Entity Enemy = new Entity(600, 250, 2, 120, -350);
	private AnimatedSprite2D _animatedSprite;
	Vector2 dir; //direction actuelle de Gus
	private HealthBar healthbar;
	float temp;
	
	// Player Variables
	public CharacterBody2D player = null;
	public Vector2 playerbaseposition;
	
	// Status
	private bool gettinghurt = false;
	private bool direction = false;
	public bool player_chase = false;
	//--------------------------------------------------------------------------------------------//
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//------ Initialisation -------//
		_animatedSprite = GetNode<AnimatedSprite2D>("Gus");
		healthbar = GetNode<HealthBar>("HealthBar");
		healthbar.health_init(Enemy.Vie);
		baseposition = Character.Position;
		//-----------------------------//
	}

	public override void _Process(double delta)
	{
		if (Enemy.Vie <= 0)
		{
			QueueFree();
		}
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
			temp = (playerbaseposition.X + player.Position.X) - (baseposition.X + Position.X); 
			if (temp > 0)
			{
				dir = Vector2.Right;
				direction = false;
			}
			else
			{
				dir = Vector2.Left;
				direction = true;
			}
			velocity.X = dir.X * Enemy.Speed;
			if (gettinghurt == false)
			{
				_animatedSprite.Play("run-shoot");
			}
		}
		else
		{
			velocity.X = 0;
			if (gettinghurt == false)
			{
				_animatedSprite.Play("idle");	
			}
		}
		
		//pour orienter Gus
		_animatedSprite.FlipH = direction;
		//_animatedSprite.FlipH = dir.X < 0;
		
		Velocity = velocity;
		MoveAndSlide();
	}
	
	
	private void _on_detection_area_body_entered(astra body)
	{
		player = body;
		playerbaseposition = body.baseposition;
		player_chase = true;
		
		//attack simulation
		hurt(body.Astra.Attaque);
		body.hurt(Enemy.Attaque);
	}


	private void _on_detection_area_body_exited(astra body)
	{
		player_chase = false;
		dir = Vector2.Zero;
		player = null;
		
	}

	public async void hurt(float value)
	{
		gettinghurt = true;
		_animatedSprite.Play("hurt");
		Color defaultt = _animatedSprite.Modulate;
		_animatedSprite.Modulate = new Color(255, 255, 255);
		Enemy.set_health(Enemy.Vie - value);
		if (Enemy.Vie <= 0 && Enemy.is_alive)
		{
			Enemy._die();
		}

		healthbar.set_health(Enemy.Vie);
		await ToSignal(GetTree().CreateTimer(0.5), "timeout");
		gettinghurt = false;
		_animatedSprite.Modulate = defaultt;
	}
}


