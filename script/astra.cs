using Godot;
using System;
using System.Collections.Generic;
using Devs.project.script;
public partial class astra : CharacterBody2D
{
	//------------------------------- V a r i a b l e s ------------------------------------------//
	// Get Node2D Parent
	[Export] public Node2D Character;
	public Vector2 baseposition;
	
	// Astra Variables
	public Player Astra = new Player(1000, 250, 4, 300, -420, new Dictionary<int, Inventory>(), 1000);
	private AnimatedSprite2D _animatedSprite; //LA VARIABLE D'ASTRA BODY
	[Export] private myhealthbar HealthBar;
	
	// Mouvement Variables
	private Vector2 _movementInput = Vector2.Zero;
	private Vector2 _lastDirection = Vector2.Zero;
	
	// Jump Variables
	private int _maxJumps = 2; // Maximum number of jumps
	private int _jumpcount = 0; // Number of jumps remaining
	private bool jumped = true;
	private string jumpanimation = "jumpfirst";
	private GpuParticles2D jumpdust;
	
	// Status
	private bool gettinghurt = false;
	//--------------------------------------------------------------------------------------------//
	
	public override void _Ready()
	{
		//------ Initialisation -------//
		_animatedSprite = GetNode<AnimatedSprite2D>("Astra");
		jumpdust = GetNode<GpuParticles2D>("jumpparticles");
		jumpdust.OneShot = true;
		HealthBar.health_init(Astra.Vie);
		baseposition = Character.Position;
		//-----------------------------//
	}
	
	public void MovementPerformed(Vector2 input)
	{
		_movementInput = input.Normalized();

		if (input.X != 0f)
		{
			_lastDirection = _movementInput;
		}
	}

	public void Stop()
	{
		_movementInput = Vector2.Zero;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		
		//Mouvements fonctionnelsgit 
		Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Astra.Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Astra.Speed);
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
				velocity.Y = Astra.JumpVelocity;
				_jumpcount += 1;
			}
		}

		if (IsOnFloor())
		{
			jumpanimation = "jumpfirst";
			if (_jumpcount != 0)
			{
				_jumpcount = 0;
				jumped = true;
			}
			
			if (gettinghurt == false)
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
		}
		//Add the gravity.
		else
		{
			velocity.Y += Astra.gravity * (float)delta;
			//Animation jump
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
		
		
		//pour tourner le perso à gauche
		bool isLeft = _lastDirection.X < 0;
		_animatedSprite.FlipH = isLeft;

		Velocity = velocity;
		MoveAndSlide();
		
	}
	
	public override void _Process(double delta)
	{
		//Mourir
		if (Astra.Vie <= 0)
		{
			QueueFree();
		}
		//MovementInput
		_movementInput = new Vector2(
			Input.GetAxis("Left" ,"Right"),
			Input.GetAxis("Up", "Down")
		);
		MovementPerformed(_movementInput);
	}
	
	public async void hurt(float value)
	{
		gettinghurt = true;
		//animation1
		_animatedSprite.Play("hurt");
		Color defaultt = _animatedSprite.Modulate;
		_animatedSprite.Modulate = new Color(255, 0, 0);
		
		//set health
		Astra.set_health(Astra.Vie - value);
		HealthBar.set_health(Astra.Vie);
		if (Astra.Vie <= 0 && Astra.is_alive)
		{
			Astra._die();
		}
		//animation2
		await ToSignal(GetTree().CreateTimer(0.2), "timeout");
		_animatedSprite.Modulate = defaultt;
		
		gettinghurt = false;
	}

	public async void heal(float value)
	{
		//set health
		Astra.set_health(Astra.Vie + value);
		HealthBar.set_health(Astra.Vie);
		
		//animation
		Color defaultt = _animatedSprite.Modulate;
		_animatedSprite.Modulate = new Color(0, 255, 0);
		await ToSignal(GetTree().CreateTimer(0.2), "timeout");
		_animatedSprite.Modulate = defaultt;
	}
}
