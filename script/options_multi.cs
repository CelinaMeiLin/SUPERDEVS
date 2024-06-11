using Godot;
using System;
using Devs.project.script;

public partial class options_multi : Control
{
	[Export] 
	private int port = 1234;

	[Export]
	private string adress = "10.196.8.31";

	private ENetMultiplayerPeer peer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Multiplayer.PeerConnected += PeerConected;
		Multiplayer.PeerConnected += PeerDisconnected;
		Multiplayer.ConnectedToServer += ConnectedToServer;
		Multiplayer.ConnectionFailed += ConnectionFailed;
	}

	private void ConnectionFailed()
	{
		GD.Print("Connection failed.");
	}

	private void ConnectedToServer()
	{
		GD.Print("Connected to server.");
	}

	private void PeerDisconnected(long id)
	{
		GD.Print("Player disconnected" + id.ToString());
	}

	private void PeerConected(long id)
	{
		GD.Print("Player connected" + id.ToString());
	}

	public void _on_back_button_down()
	{
		Visible=false;
		GetTree().ChangeSceneToFile("res://scene/main_menu.tscn");
		//var gameScene=GD.Load<PackedScene>("res://scene/main_menu.tscn");
		//var gameScenebackNode=gameScene.Instantiate();
		//GetParent().AddChild(gameScenebackNode);
	}
	
	private void _on_host_button_down()
	{
		 peer = new ENetMultiplayerPeer();
		var error = peer.CreateServer(port, 2);
		if (error != Error.Ok)
		{
			GD.Print("error : player cannot host ! : " + error.ToString());
			return;
		}
		peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);
		Multiplayer.MultiplayerPeer = peer;
		GD.Print("Waiting a player.");
	}


	private void _on_join_button_down()
	{
		peer = new ENetMultiplayerPeer();
		peer.CreateClient(adress, port);
		Multiplayer.MultiplayerPeer = peer;
		GD.Print("Joining Game!");
	}
}



