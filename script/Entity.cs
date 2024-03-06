using Godot;
using System;

public partial class Entity 
{
	public float Vie { get; set; }
	public float Attaque { get; set; }
	public float Defense { get; set; }
	public float Speed { get; set; }
	public float JumpVelocity { get; set; }
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public Entity(float vie, float attaque, float defense, float speed, float jumpVelocity)
	{
		Vie = vie;
		Attaque = attaque;
		Defense = defense;
		Speed = speed;
		JumpVelocity = jumpVelocity;
	}

	public void Hurt(float damage)
	{
		Vie -= damage;
	}
	
	


}
