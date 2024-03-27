using Godot;
using System;

public partial class SceneTransistor : CanvasLayer
{
	
	public string new_scene;

	private AnimationPlayer animationPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}
	
	public void StartTransitionTo(string pathToScene)
	{
		new_scene = pathToScene;
		animationPlayer.Play("change_scene_to_file");
	}

	
	public void ChangeSceneToFile()
	{
		GetTree().ChangeSceneToFile(new_scene);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
