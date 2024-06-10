using Godot;
using System;
using Devs.project.Autoloads;
using Godot.Collections;

public partial class UIManager : CanvasLayer
{

	public Label CoinDisplay;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CoinDisplay = GetNode<Label>("CoinDisplay");
	}



	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CoinDisplay.Text = UserPreferences.Data["Coin"].ToString();
	}
	
}
