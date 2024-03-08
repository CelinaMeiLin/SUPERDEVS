using Godot;
using System;

public partial class UI_Pause_Menu : Control
{
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		if (Input.IsActionPressed("Exit"))
		{
			Visible = true;
			GetTree().Paused = true;
		}
	}
	
	private void _on_resume_pressed()
	{
		
	}
	
	private void _on_exit_pressed()
	{
		SceneManager.instance.ChangeScene(eSceneNames.MainMenu);
		// Replace with function body.
	}
}






