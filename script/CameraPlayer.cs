using Godot;
using System;

public partial class CameraPlayer : Camera2D
{
	[Export] private CharacterBody2D ObjectToFollow;
	
	public Node2D RBorderCam;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//RBorderCam = GetParent().GetParent().GetParent().GetParent().GetNode<Node2D>("RBorderLimit");
		//LimitRight = (int)RBorderCam.Position.X;
		
		//Multiplayer Control
		if (GetParent().GetNode<MultiplayerSynchronizer>("MultiplayerSynchronizer").GetMultiplayerAuthority() ==
		    Multiplayer.GetUniqueId())
		{
			Enabled = true;
		}
		else
		{
			Enabled = false;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Position = ObjectToFollow.Position;
	}
}
