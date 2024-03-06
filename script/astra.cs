using Godot;
using System;
using System.Collections.Generic;
using Devs.project.script;
public partial class astra : CharacterBody2D
{
	private Vector2 _movementInput = Vector2.Zero;
	private Vector2 _lastDirection = Vector2.Zero;
	private int _maxJumps = 2; // Maximum number of jumps
	private int _jumpcount = 0; // Number of jumps remaining
	private bool jumped = true;
	private AnimatedSprite2D _animatedSprite; //LA VARIABLE D'ASTRA
	
	Player Astra = new Player(1000, 250, 4, 300, -420, new Dictionary<int, Inventory>(), 1000);
	
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
	
	public override void _Ready()
	{
		//initialize le sprite Astra
		_animatedSprite = GetNode<AnimatedSprite2D>("Astra");
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
				velocity.Y = Astra.JumpVelocity;
				_jumpcount += 1;
			}
		}

		if (IsOnFloor())
		{
			if (_jumpcount != 0)
			{
				_jumpcount = 0;
				jumped = true;
			}
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
				_animatedSprite.Play("jump");
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
		//MovementInput
		_movementInput = new Vector2(
			Input.GetAxis("Left" ,"Right"),
			Input.GetAxis("Up", "Down")
		);
		MovementPerformed(_movementInput);
	}
}
