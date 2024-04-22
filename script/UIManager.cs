using Godot;
using System;
using Devs.project.Autoloads;

public partial class UIManager : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Godot.Callable updateCoinDisplayCallable = new Godot.Callable(this, "Update_Coin_Display");
		Global.Instance.Connect("Gained_Coins", updateCoinDisplayCallable);	}
	
	public void Update_Coin_Display(Signal GainedCoins)
	{
		
		var coinLabel = GetNode<Label>("CoinLabel");
		coinLabel.Text = Global.Instance.Coins.ToString();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
