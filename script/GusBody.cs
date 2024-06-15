using Godot;
using System;
using System.Collections.Generic;
using System.Numerics;
using Devs.project.Autoloads;
using Devs.project.Ressources;
using Devs.project.script;
using Vector2 = Godot.Vector2;

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
	
	// Shoot Variables
	[Export] PackedScene Bullet_scn;
	private CollisionShape2D Bullet_spawnerG;
	private CollisionShape2D Bullet_spawnerD;
	private float bullet_speed = 800f;
	private float bullet_per_second { get; }= 0.5f;
	private float fire_rate = 1f / 0.5f; //bullet_per_second
	private float time_until_fire = 0f;
	private bool shoot_anim = false;
	
	// Status
	private bool gettinghurt = false;
	private bool direction = false;
	public bool player_chase = false;
	private bool dying = false;
	
	//Sound
	private AudioStreamPlayer2D audio_gun;
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
		Bullet_spawnerG = GetNode<CollisionShape2D>("bulletspawnerG");
		Bullet_spawnerD = GetNode<CollisionShape2D>("bulletspawnerD");
		//-----------------------------//
		audio_gun = GetNode<AudioStreamPlayer2D>("Audio_gun");
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
			
			// Shoot
			if (time_until_fire > fire_rate)
			{
				RigidBody2D bullet = Bullet_scn.Instantiate<RigidBody2D>();

				Vector2 Spawn;
				int b_direction = 1;
				bool isLeft = dir.X <= -1;
				bool isCrouch = _animatedSprite.Animation == "crouch";
				if (isLeft)
				{
					Spawn = Bullet_spawnerG.GlobalPosition;
					b_direction = -1;
				}
				else
				{
					Spawn = Bullet_spawnerD.GlobalPosition;
					b_direction = 1;
				}

				bullet.GlobalPosition = Spawn;
				bullet.LinearVelocity = bullet.Transform.X * bullet_speed * b_direction;

				audio_gun.Play();
				GetTree().Root.AddChild(bullet);

				time_until_fire = 0f;
			}
			else
			{
				time_until_fire += (float)delta;
			}
		}
		/*
		else
		{
			
			bool isLeft = dir.X <= -1;
			direction = isLeft;
			if (isLeft)
			{
				if (dir.X != -20)
				{
					velocity.X = 60 * -1;
				}
				else
				{
					velocity.X = 60;
				}
				_animatedSprite.Play("run");
				
			}
			else
			{
				if (dir.X != 20)
				{
					velocity.X = 60;
				}
				else
				{
					velocity.X = 60* -1;
				}
				_animatedSprite.Play("run");
			}	
		}
		*/
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
		if (gettinghurt == false)
		{
			_animatedSprite.Play("idle");	
		}
		
	}

	//--------------------------------- HP SYSTEM -----------------------------------------//
	private async void _die()
	{
		if (dying)
		{
			return;
		}

		dying = true;
		SetCollisionLayerValue(3, false);
		
		GameManager.EnemyKilled++;
		//UserPreferences.Data["EnemyKilled"] = (int)UserPreferences.Data["EnemyKilled"] + 1;
		
		var pos = Position;
		Death_particles.Emitting = true;
		_animatedSprite.Visible = false;
		
		await ToSignal(GetTree().CreateTimer(0.1), "timeout");
		
		QueueFree(); //this.QueueFree() de l'ennemi
		
		GameManager.SpawnCoin(this, pos);
		GetTree().CallGroup("Astra", "GainXp", 30);
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


