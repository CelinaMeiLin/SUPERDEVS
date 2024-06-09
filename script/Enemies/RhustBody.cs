using Godot;

namespace Devs.project.script.Enemies;

public partial class RhustBody : CharacterBody2D
{
    //------------------------------- V a r i a b l e s ------------------------------------------//
	// Get Node2D Parent
	[Export] public Node2D Character;
	public Vector2 baseposition;
	public Node2D projectile;
	
	// Rhust Variables
	public Entity Enemy = new Entity(600, 250, 2, 120, -350);
	private AnimatedSprite2D _animatedSprite;
	private AnimatedSprite2D _sniperanim;
	Vector2 dir; //direction actuelle de Rhust
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
	
	
	//Sound
	 
	
	//--------------------------------------------------------------------------------------------//
	
	
	
	//----------------------------------GODOT FUNCTIONS-------------------------------------------//
	public override void _Ready()
	{
		//------ Initialisation -------//
		_animatedSprite = GetNode<AnimatedSprite2D>("Rhust");
		_sniperanim = GetNode<AnimatedSprite2D>("FlashCrouch");
		healthbar = GetNode<HealthBar>("HealthBar");
		healthbar.health_init(Enemy.Vie);
		baseposition = Character.Position;
		Death_particles = GetNode<GpuParticles2D>("DeathParticles");
		Death_particles.OneShot = true;
		Enemy.queuefree = false;
		Bullet_spawnerG = GetNode<CollisionShape2D>("bulletspawnerG");
		Bullet_spawnerD = GetNode<CollisionShape2D>("bulletspawnerD");
		projectile = GetNode<Node2D>("Projectile");
		
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
			velocity.X = dir.X * Enemy.Speed;
			
			if (gettinghurt == false)
			{
				_animatedSprite.Play("crouch-shoot");
				//_sniperanim.Play("default");
				
			}
			
			// Shoot
			if (shoot_anim)
			{
				//_animatedSprite.Stop();
				//_animatedSprite.Play("crouch-shoot");

				RigidBody2D bullet = Bullet_scn.Instantiate<RigidBody2D>();

				Vector2 Spawn;
				int b_direction = 1;
				//bool isLeft = dir.X <= -1;
				//bool isCrouch = _animatedSprite.Animation == "crouch-shoot";
				if (direction)
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
				shoot_anim = false;

				//audio_gun.Play();
				GetTree().Root.AddChild(bullet);

				time_until_fire = 0f;
			}
			else
			{
				time_until_fire += (float)delta;
			}
			
		}
		else
		{
			
//			temp = (playerbaseposition.X + player.Position.X) - (baseposition.X + Position.X); 
		
			velocity.X = dir.X * Enemy.Speed;
			
			if (gettinghurt == false)
			{
				_animatedSprite.Play("run");	
			}
		}
		
		//pour orienter Rhust
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
		dir = Vector2.Zero;
		GetNode<GpuParticles2D>("Exclamation").Emitting = true;
		
		//attack simulation
		//hurt(body.Astra.Attaque);
		//body.hurt(Enemy.Attaque);
	}
	
	private void _on_detection_area_body_exited(astra body)
	{
		
		player = body;
		playerbaseposition = body.baseposition;
		player_chase = false;
		//dir = Vector2.Zero;
		
		GetNode<GpuParticles2D>("Interrogation").Emitting = true;
		

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

		
		if (gettinghurt == false)
		{
			_animatedSprite.Play("run");	
		}
		
	}

	private void _on_rhust_animation_looped()
	{
		if (_animatedSprite.Animation == "crouch-shoot")
		{
			shoot_anim = true;
		}
		else
		{
			shoot_anim = false;
		}
		
	}
	
	private void _on_rhust_animation_changed()
	{
		shoot_anim = false;
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