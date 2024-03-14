using Godot;
using System;

public partial class myhealthbar : TextureProgressBar
{
	public float Health { get; set; }
	public float MaxHealth { get; set; }

	public void set_health(float _health)
	{
		Health = Math.Min(MaxHealth, _health);
		Value = Health/MaxHealth *55; // Scale to Health bar cause it's borken :/
	}
	public void health_init(float _health)
	{
		Health = _health;
		MaxHealth = _health;
		// Vie affichée buguée ???
		MaxValue = 100; 
		Value = 55;
	}
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
