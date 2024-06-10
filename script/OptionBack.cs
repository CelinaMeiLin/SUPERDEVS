using Godot;
using System;

public partial class OptionBack : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		if (GetParent().GetParent().Name == "map")
		{
			Position = new Vector2(-139.5f, 485f);
		}
		else 
		{
			Position = new Vector2(821, 1015);
		}
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void OnBackOptionsPressed()
	{
		GetParent().QueueFree();
	}
}
