using Godot;
using System;
using System.Collections.Generic;
using System.Numerics;
using Devs.project.script;
using Vector2 = Godot.Vector2;

public partial class PowBody : CharacterBody2D
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

	//--------------------------------------------------------------------------------------------//
	
	
	//----------------------------------GODOT FUNCTIONS-------------------------------------------//

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		
	}
	
	public void _on_body_entered(astra body)
	{
		player = body;
		playerbaseposition = body.baseposition;
		player_chase = true;
		//GetNode<GpuParticles2D>("Exclamation").Emitting = true;

	}
	
	public void _on_body_exited(astra body)
	{
		player_chase = false;
		dir = Vector2.Zero;
		player = null;
		//GetNode<GpuParticles2D>("Interrogation").Emitting = true;
		if (gettinghurt == false)
		{
			_animatedSprite.Play("idle");	
		}
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
