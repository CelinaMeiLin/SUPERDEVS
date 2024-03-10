using Godot;
using System;

public partial class myhealthbar : TextureProgressBar
{

	public float Health { get; set; }

	public void set_health(float _health)
	{
		Health = Math.Min((float)MaxValue, _health);
		Value = (double)Health;
		
		if (Health <= 0)
		{
			QueueFree();
		}
	}
	public void health_init(float _health)
	{
		Health = _health;
		MaxValue = _health;
		Value = _health;
	}
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
