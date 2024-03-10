using Godot;
using System;

public partial class HealthBar : ProgressBar
{
	private ProgressBar damage_bar;
	private Timer timer;
	public bool visible = false;

	public float Health { get; set; }

	public void set_health(float _health)
	{
		float prev_health = Health;
		Health = Math.Min((float)MaxValue, _health);
		Value = (double)Health;
		
		if (Health <= 0)
		{
			QueueFree();
		}

		if (Health < prev_health)
		{
			timer.Start();
		}
		else
		{
			damage_bar.Value = Health;
		}
	}
	public void health_init(float _health)
	{
		Health = _health;
		MaxValue = _health;
		Value = _health;
		damage_bar.MaxValue = _health;
		damage_bar.Value = _health;
	}
	public override void _Ready()
	{
		damage_bar = GetNode<ProgressBar>("DamageBar");
		timer = GetNode<Timer>("Timer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (visible)
		{
			Visible = true;
		}
		else
		{
			Visible = false;
		}

		if (Health < MaxValue)
		{
			visible = true;
		}
	}

	private void _on_timer_timeout()
	{
		damage_bar.Value = Health;
	}
}
