using Godot;
using System;
using System.Collections.Generic;
using Devs.project.script;
public partial class ekko : CharacterBody2D
{
	//------------------------------- V a r i a b l e s ------------------------------------------//
	// Get Node2D Parent
	[Export] public Node2D Character;
	public Vector2 baseposition; //Localisation sur la map
	
	// Astra Variables
	public Player Ekko = new Player(1000, 250, 4, 300, -420, new Dictionary<int, Inventory>(), 1000);
	private AnimatedSprite2D _animatedSprite; //LA VARIABLE D'ASTRA BODY
	[Export] private myhealthbar HealthBar;
	private GpuParticles2D Death_particles;
	
	// Mouvement Variables
	private Vector2 _lastDirection = Vector2.Zero;
	
	// Dash Variables
	private bool CanDash = true;
	private float dash_speed = 1000;
	private double dash_reload = 1.5;
	private static double DOUBLETAP_DELAY = 0.25;
	private double doubletap_time = DOUBLETAP_DELAY;
	private string last_input;
	private GpuParticles2D ghost;
	
	// Jump Variables
	private int _maxJumps = 2; // Maximum number of jumps
	private int _jumpcount = 0; // Number of jumps remaining
	private bool jumped = true;
	private string jumpanimation = "jumpfirst";
	private GpuParticles2D jumpdust;
	
	// Shoot Variables
	[Export] PackedScene Bullet_scn;
	private CollisionShape2D Bullet_spawnerG;
	private CollisionShape2D Bullet_spawnerD;
	private CollisionShape2D Bullet_spawnerGLow;
	private CollisionShape2D Bullet_spawnerDLow;
	private float bullet_speed = 800f;
	private float bullet_per_second { get; }= 5f;
	private float fire_rate = 1f / 5f; //bullet_per_second
	private float time_until_fire = 0f;
	private bool shoot_anim = false;
	
	// Status
	private bool gettinghurt = false;
	private bool isShooting = false;
	private bool Dashing = false;
	//--------------------------------------------------------------------------------------------//
	
	
	//----------------------------------GODOT FUNCTIONS-------------------------------------------//
	public override void _Ready()
	{
		//------ Initialisation -------//
		_animatedSprite = GetNode<AnimatedSprite2D>("Astra");
		Ekko.queuefree = false;
		jumpdust = GetNode<GpuParticles2D>("jumpparticles");
		jumpdust.OneShot = true;
		HealthBar.health_init(Ekko.Vie);
		baseposition = Character.Position;
		ghost = GetNode<GpuParticles2D>("Ghost");
		ghost.OneShot = true;
		Bullet_spawnerG = GetNode<CollisionShape2D>("bulletspawnerG");
		Bullet_spawnerD = GetNode<CollisionShape2D>("bulletspawnerD");
		Bullet_spawnerGLow = GetNode<CollisionShape2D>("bulletspawnerGLow");
		Bullet_spawnerDLow = GetNode<CollisionShape2D>("bulletspawnerDLow");
		Death_particles = GetNode<GpuParticles2D>("DeathParticles");
		Death_particles.OneShot = true;
		//-----------------------------//
		//-----Test pour le multi normalement c fait pour le mult synchronizer------//
		GetNode<MultiplayerSynchronizer>("MultiplayerSynchronizer").SetMultiplayerAuthority(int.Parse(Name));
		
	}
	
    //Multi sync 
	public override void _PhysicsProcess(double delta)
	{

			Vector2 velocity = Velocity;

			//Mouvements fonctionnels
			Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");
			if (Dashing)
			{
				CanDash = false;
				_animatedSprite.Play("dash");
				velocity.X = (direction.X * dash_speed);
				DashTimer();
			}
			else if (direction != Vector2.Zero)
			{
				//pour tourner le perso à gauche
				_lastDirection = direction.Normalized();
				bool isLeft = _lastDirection.X < 0;
				_animatedSprite.FlipH = isLeft;

				velocity.X = direction.X * Ekko.Speed;
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Ekko.Speed);
			}

			//Handle Jump.
			if (_jumpcount < _maxJumps)
			{
				if (Input.IsActionJustPressed("Up"))
				{
					if (_jumpcount == 1 && !jumped)
					{
						jumpdust.Emitting = true;
						jumpanimation = "jump";
					}

					velocity.Y = Ekko.JumpVelocity;
					_jumpcount += 1;
				}
			}

			// Shoot
			if (Input.IsActionJustPressed("Shoot") && time_until_fire > fire_rate)
			{
				isShooting = true;
				RigidBody2D bullet = Bullet_scn.Instantiate<RigidBody2D>();

				Vector2 Spawn;
				int b_direction = 1;
				bool isLeft = _lastDirection.X < 0;
				bool isCrouch = _animatedSprite.Animation == "crouch";
				if (isLeft)
				{
					if (isCrouch)
					{
						Spawn = Bullet_spawnerGLow.GlobalPosition;
					}
					else
					{
						Spawn = Bullet_spawnerG.GlobalPosition;
					}

					b_direction = -1;
				}
				else
				{
					if (isCrouch)
					{
						Spawn = Bullet_spawnerDLow.GlobalPosition;
					}
					else
					{
						Spawn = Bullet_spawnerD.GlobalPosition;
					}

					b_direction = 1;
				}

				bullet.GlobalPosition = Spawn;
				bullet.LinearVelocity = bullet.Transform.X * bullet_speed * b_direction;

				GetTree().Root.AddChild(bullet);

				time_until_fire = 0f;
				ShootStatusTimer();
			}
			else
			{
				time_until_fire += (float)delta;
			}

			//     - Animations - 
			if (IsOnFloor())
			{
				jumpanimation = "jumpfirst";
				if (_jumpcount != 0)
				{
					_jumpcount = 0;
					jumped = true;
				}

				if (gettinghurt == false && Dashing == false && isShooting == false)
				{
					//Pour jouer l'animation de run
					if ((Input.IsActionPressed("Right") || Input.IsActionPressed("Left")))
					{
						_animatedSprite.Play("run");
					}
					//Pour crouch
					else if (Input.IsActionPressed("Down") && IsOnFloor())
					{
						_animatedSprite.Play("crouch");
					}
					//Animation de idle donc lobby
					else
					{
						_animatedSprite.Play("idle");
					}
				}
				else if (isShooting && gettinghurt == false)
				{
					// Run & Shoot
					if ((Input.IsActionPressed("Right") || Input.IsActionPressed("Left")))
					{
						_animatedSprite.Play("run-shoot");
					}
					// Crouch
					else if (Input.IsActionPressed("Down") && IsOnFloor())
					{
						_animatedSprite.Play("crouch");
					}
					// Shoot
					else
					{
						_animatedSprite.Play("shoot");
					}
				}
			}
			//Add the gravity.
			else
			{
				velocity.Y += Ekko.gravity * (float)delta;
				//Animation jump
				if (gettinghurt == false && Dashing == false)
				{
					if (jumped)
					{
						_jumpcount = 1;
						jumped = false;
					}
					else
					{
						if (gettinghurt == false)
						{
							_animatedSprite.Play(jumpanimation);
						}
					}
				}
			}

			doubletap_time -= delta;
			Velocity = velocity;
			MoveAndSlide();
		
	}
	
	//Fonction pour le multi
	public void _enter_tree()
	{
		SetMultiplayerAuthority(Name.ToString().ToInt());
	}
	public override void _Process(double delta)
	{
		//Mourir
		if (Ekko.Vie <= 0)
		{
			QueueFree();
		}
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("Right"))
		{
			if (CanDash && last_input == "Right" && doubletap_time >= 0)
			{
				Dashing = true;
			}
			else
			{
				last_input = "Right";
			}

			doubletap_time = DOUBLETAP_DELAY;
		}
		else if (@event.IsActionPressed("Left"))
		{
			if (CanDash && last_input == "Left" && doubletap_time >= 0)
			{
				Dashing = true;
			}
			else
			{
				last_input = "Left";
			}

			doubletap_time = DOUBLETAP_DELAY;
		}
	}
	//--------------------------------------------------------------------------------------//

	
	public async void DashTimer()
	{
		//Timer de durée Max
		await ToSignal(GetTree().CreateTimer(0.2), "timeout");
		Dashing = false;
		//Timer de reload
		await ToSignal(GetTree().CreateTimer(dash_reload), "timeout");
		CanDash = true;
		ghost.Emitting = true;
	}

	public async void ShootStatusTimer()
	{
		if (shoot_anim)
		{
			return;
		}
		if (isShooting == false)
		{
			return;
		}
		shoot_anim = true;
		await ToSignal(GetTree().CreateTimer(1), "timeout");
		isShooting = false;
		shoot_anim = false;
	}
	
	
	//--------------------------------- HP SYSTEM -----------------------------------------//
	public async void hurt(float value)
	{
		gettinghurt = true;
		//animation1
		_animatedSprite.Play("hurt");
		Color defaultt = _animatedSprite.Modulate;
		_animatedSprite.Modulate = new Color(255, 0, 0);
		
		//set health
		Ekko.set_health(Ekko.Vie - value);
		HealthBar.set_health(Ekko.Vie);
		if (Ekko.Vie <= 0)
		{
			_die();
		}
		//animation2
		await ToSignal(GetTree().CreateTimer(0.2), "timeout");
		_animatedSprite.Modulate = defaultt;
		
		gettinghurt = false;
	}
	public async void heal(float value)
	{
		//set health
		Ekko.set_health(Ekko.Vie + value);
		HealthBar.set_health(Ekko.Vie);
		
		//animation
		Color defaultt = _animatedSprite.Modulate;
		_animatedSprite.Modulate = new Color(0, 255, 0);
		await ToSignal(GetTree().CreateTimer(0.2), "timeout");
		_animatedSprite.Modulate = defaultt;
	}
	private async void _die()
	{
		Death_particles.Emitting = true;
		_animatedSprite.Visible = false;
		await ToSignal(GetTree().CreateTimer(0.6), "timeout");
		QueueFree();
	}
	public void SetUpPlayer(string name)
	{
		GetNode<Label>("PlayerName").Visible = true;
		GetNode<Label>("PlayerName").Text = name;
	}
	//---------------------------------------------------------------//
}
