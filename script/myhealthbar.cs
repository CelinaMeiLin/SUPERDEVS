using Godot;
using System;
using Devs.project.Autoloads;

public partial class myhealthbar : TextureProgressBar
{
	public float Health { get; set; }
	public float MaxHealth { get; set; }

	public void set_health(float _health)
	{
		Health = Math.Min((int)UserPreferences.Data["MaxHealth"] * 55, _health);
		Value = Health/MaxHealth * 55; // Scale to Health bar because it's borken :/
	}
	public void health_init(float _health)
	{
		Health = _health;
		MaxHealth = _health;
		// Vie affichée buguée ???
		//MaxValue = (int)UserPreferences.Data["MaxHealth"];
		Value = (int)UserPreferences.Data["MaxHealth"];
	}
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	
	}
}
