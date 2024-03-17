using Godot;
using System;

public partial class Multiplayer : Node
{
	
	ENetMultiplayerPeer peer = new ENetMultiplayerPeer();

	[Export]
	public PackedScene PlayerScene { get; set; }
		
	private MultiplayerPeer multiplayer;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		multiplayer = GetNode<MultiplayerPeer>("/root/MultiplayerPeer");
	}
	
	public void OnHostPressed()
	{
		peer.CreateServer(135);
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
