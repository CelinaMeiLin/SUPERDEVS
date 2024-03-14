using Godot;
using System;

public partial class medical_kit : Sprite2D
{
	public float Heal { get; set; }

	private AnimationPlayer animplayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animplayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animplayer.Play("idle");
		Heal = 500;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_detection_area_body_entered(astra body)
	{
		body.heal(Heal);
		QueueFree();
	}
}
