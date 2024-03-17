using Godot;
using System;

public partial class Entity : CharacterBody2D
{
	public bool queuefree { get; set; } = true;
	
	public float Vie { get; set; }
	public float Attaque { get; set; }
	public float Defense { get; set; }
	public float Speed { get; set; }
	public float JumpVelocity { get; set; }
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public float maxVie { get; set; }

	public Entity(float vie, float attaque, float defense, float speed, float jumpVelocity)
	{
		Vie = vie;
		Attaque = attaque;
		Defense = defense;
		Speed = speed;
		JumpVelocity = jumpVelocity;
		maxVie = Vie;
	}

	public void set_health(float new_vie)
	{
		if (new_vie > maxVie)
		{
			Vie = maxVie;
			return;
		}
		Vie = new_vie;
		if (Vie <= 0)
		{
			_die();
		}
	}

	public void _die()
	{
		if (queuefree)
		{
			QueueFree();
		}
	}
	
	


}
