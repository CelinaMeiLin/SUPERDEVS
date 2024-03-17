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
	public Entity Enemy = new Entity(600, 250, 2, 120, -350);
	private AnimatedSprite2D _animatedSprite;
	Vector2 dir; //direction actuelle de Gus
	private HealthBar healthbar;
	float temp;
	private GpuParticles2D Death_particles;
	
	// Player Variables
	public CharacterBody2D player = null;
	public Vector2 playerbaseposition;
	
	// Status
	private bool gettinghurt = false;
	private bool direction = false;
	public bool player_chase = false;
	//--------------------------------------------------------------------------------------------//
	
	
	//----------------------------------GODOT FUNCTIONS-------------------------------------------//
	public override void _Ready()
	{
		//------ Initialisation -------//
		_animatedSprite = GetNode<AnimatedSprite2D>("Gus");
		healthbar = GetNode<HealthBar>("HealthBar");
		healthbar.health_init(Enemy.Vie);
		baseposition = Character.Position;
		Death_particles = GetNode<GpuParticles2D>("DeathParticles");
		Death_particles.OneShot = true;
		Enemy.queuefree = false;
		//-----------------------------//
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
	//--------------------------------------------------------------------------------------//
	
	
	private void _on_detection_area_body_entered(astra body)
	{
		player = body;
		playerbaseposition = body.baseposition;
		player_chase = true;
		GetNode<GpuParticles2D>("Exclamation").Emitting = true;

		//attack simulation
		//hurt(body.Astra.Attaque);
		//body.hurt(Enemy.Attaque);
	}


	private void _on_detection_area_body_exited(astra body)
	{
		player_chase = false;
		dir = Vector2.Zero;
		player = null;
		GetNode<GpuParticles2D>("Interrogation").Emitting = true;
		
	}

	//--------------------------------- HP SYSTEM -----------------------------------------//
	private async void _die()
	{
		Death_particles.Emitting = true;
		_animatedSprite.Visible = false;
		await ToSignal(GetTree().CreateTimer(0.6), "timeout");
		QueueFree();
	}
	public async void hurt(float value)
	{
		gettinghurt = true;
		_animatedSprite.Play("hurt");
		Color defaultt = _animatedSprite.Modulate;
		_animatedSprite.Modulate = new Color(255, 255, 255);
		Enemy.set_health(Enemy.Vie - value);
		if (Enemy.Vie <= 0)
		{
			_die();
		}

		healthbar.set_health(Enemy.Vie);
		await ToSignal(GetTree().CreateTimer(0.5), "timeout");
		gettinghurt = false;
		_animatedSprite.Modulate = defaultt;
	}
	//---------------------------------------------------------------//
}


