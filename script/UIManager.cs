using Godot;
using System;
using Devs.project.Autoloads;
using Godot.Collections;

public partial class UIManager : CanvasLayer
{

	public Label CoinDisplay;
	
	public int score = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CoinDisplay = GetNode<Label>("CoinDisplay");
		
		Array<Node> coins = GetTree().GetNodesInGroup("coins");
		for (int i = 0; i < coins.Count; i++)
		{
			coin n = (coin)coins[i];
			n.CoinCollected += () => AddScore();
		}
	}

	public void AddScore()
	{
		score += 1;
		CoinDisplay.Text = score.ToString();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
}
