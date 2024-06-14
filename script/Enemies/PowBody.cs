using Godot;
using System;
using System.Collections.Generic;
using System.Numerics;
using Devs.project.Ressources;
using Devs.project.script;
using Vector2 = Godot.Vector2;

public partial class PowBody : CharacterBody2D
{

	//------------------------------- V a r i a b l e s ------------------------------------------//

	// Get Node2D Parent
	[Export] public Node2D Character;
	public Vector2 baseposition;
	[Export] public AnimationPlayer AnimationPlayer;

	// Pow Variables
	public Entity Enemy = new Entity(600, 20, 2, 120, -350);
	private AnimatedSprite2D _animatedSprite;
	private AnimatedSprite2D _attackanimation;
	Vector2 dir; //direction actuelle de Pow
	private HealthBar healthbar;
	float temp;
	private GpuParticles2D Death_particles;
	private Color basecolor;

	// Player Variables
	public CharacterBody2D player = null;
	public Vector2 playerbaseposition;

	public bool ISFINISHED = false;

	// Shoot Variables
	[Export] PackedScene Bullet_scn;
	public RigidBody2D Bullet;
	private CollisionShape2D Bullet_spawnerG;
	private CollisionShape2D Bullet_spawnerD;
	private float bullet_speed = 300f;
	private float bullet_per_second { get; } = 0.5f;
	private float fire_rate = 1f / 0.5f; //bullet_per_second
	private float time_until_fire = 0f;
	private bool shoot_anim = false;

	// Status
	private bool gettinghurt = false;
	private bool direction = false;
	public bool player_chase = false;
	private bool dying = false;

	//--------------------------------------------------------------------------------------------//


	//----------------------------------GODOT FUNCTIONS-------------------------------------------//


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//------ Initialisation -------//
		_animatedSprite = GetNode<AnimatedSprite2D>("Pow");
		//_attackanimation = GetNode<AnimatedSprite2D>("attackanimation");
		healthbar = GetNode<HealthBar>("HealthBar");
		healthbar.health_init(Enemy.Vie);
		baseposition = Character.Position;
		Death_particles = GetNode<GpuParticles2D>("DeathParticles");
		Death_particles.OneShot = true;
		Enemy.queuefree = false;
		Bullet_spawnerG = GetNode<CollisionShape2D>("bulletspawnerG");
		Bullet_spawnerD = GetNode<CollisionShape2D>("bulletspawnerD");
		basecolor = _animatedSprite.Modulate;
		//-----------------------------//
		
		direction = true;

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
				//dir = Vector2.Right;
				direction = false;
			}
			else
			{
				//dir = Vector2.Left;
				direction = true;
			}

			velocity.X = 0; //////dir.X * Enemy.Speed;
			
			//_animatedSprite.Play("attack3");
			if (direction)
			{
					//GD.Print("attack3");
				//_animatedSprite.Play("attack3");
					//Wave();
					//AnimationPlayer.Play("ATTACKANIMATIONLEFT");
			}
			else
			{
					//AnimationPlayer.Play("attack");
			}
				
			_animatedSprite.Play("attack3");


			if (_animatedSprite.Animation == "attack3" && ISFINISHED)
			{
				GD.Print("WAVE");
				Wave();
				ISFINISHED = false;
			}
			

		}
		else
		{
			velocity.X = 0;
			
			
			if (gettinghurt == false && Enemy.Vie > 0)
			{
				_animatedSprite.Play("idle");
			}
		}


		Velocity = velocity;
		
		//pour orienter Pow
		_animatedSprite.FlipH = direction;

		//MoveAndSlide();
	}

	public void Wave()
	{
		RigidBody2D bullet = Bullet_scn.Instantiate<RigidBody2D>();

		Vector2 Spawn;
		int b_direction = 1;
		
		if (direction)
		{ 
			Spawn = Bullet_spawnerG.GlobalPosition;
			b_direction = -1;
		}
		else 
		{ 
			Spawn = Bullet_spawnerD.GlobalPosition;
			GD.Print("Bullet");
			b_direction = 1; 
		}

		bullet.GlobalPosition = Spawn;
		bullet.LinearVelocity = bullet.Transform.X * bullet_speed * b_direction;
		shoot_anim = false;

		//audio_gun.Play();
		GetTree().Root.AddChild(bullet);

		time_until_fire = 0f;
		
	}
	

	public void _on_pow_frame_changed()
	{
		if (_animatedSprite.Animation == "attack3" && _animatedSprite.Frame == 3)
		{
			ISFINISHED = true;
		}
	}

	public void _on_attack_animation_looped()
	{
		//ISFINISHED = true;
		if (_animatedSprite.Animation == "attack3")
		{
			//Astra.hurt(100);
		}	
	}

	public void _on_pow_animation_finished()
	{
		//ISFINISHED = true;
		
		
		if (_animatedSprite.Animation == "death")
		{
			//QueueFree();
		}
	}
	
	public void _on_animation_player_animation_finished(string animName)
	{
		if (animName == "attack" || animName == "ATTACKANIMATIONLEFT")
		{
			//
		}
	}
	
	public void _on_body_entered(Node2D body)
	{
		if (body is astra)
		{
			player = (astra)body;
			playerbaseposition = ((astra)body).baseposition;
			player_chase = true;
			GetNode<GpuParticles2D>("Exclamation").Emitting = true;
		}
		

	}
	
	public void _on_body_exited(astra body)
	{
		player_chase = false;
		dir = Vector2.Zero;
		player = null;
		GetNode<GpuParticles2D>("Interrogation").Emitting = true;
		if (gettinghurt == false)
		{
			_animatedSprite.Play("walk");	
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
		//SetCollisionLayerValue(3, false);
		
		//_animatedSprite.Play("death");
		
		
		Death_particles.Emitting = true;
		_animatedSprite.Visible = false;
		await ToSignal(GetTree().CreateTimer(0.2), "timeout");
		QueueFree();
		
		GameManager.SpawnCoin(this, Position);
		GetTree().CallGroup("Astra", "GainXp", 90);
	}
	
	public async void hurt(float value)
	{
		if (_animatedSprite.Animation == "death")
		{
			return;
		}
		gettinghurt = true;
		_animatedSprite.Play("hurt");
		//Color defaultt = _animatedSprite.Modulate;
		//_animatedSprite.Modulate = new Color(255, 255, 255);
		Enemy.set_health(Enemy.Vie - value);
		if (Enemy.Vie <= 0)
		{
			_die();
		}

		healthbar.set_health(Enemy.Vie);
		await ToSignal(GetTree().CreateTimer(0.5), "timeout");
		gettinghurt = false;
		//_animatedSprite.Modulate = defaultt;
	}
	//---------------------------------------------------------------//

}
