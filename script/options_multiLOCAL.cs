using Godot;
using System;

public partial class options_multiLOCAL : Control
{
	private int DEFAULT_PORT = 28960;
	private int MAX_CLIENT = 2;
	
	private ENetMultiplayerPeer server = null;
	private	ENetMultiplayerPeer client = null;

	private string ip_address = "";
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (OS.GetName() == "Windows")
		{
			ip_address = IP.GetLocalAddresses()[3];
		}

		foreach (var ip in IP.GetLocalAddresses())
		{
			if (ip.StartsWith("192.168."))
			{
				ip_address = ip;
			}
		}

		Multiplayer.ConnectedToServer += _connected_to_server;
		Multiplayer.ServerDisconnected += _server_disconnected;
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	private void _connected_to_server()
	{
		GD.Print("Successfully connected to the server");
	}

	private void _server_disconnected()
	{
		GD.Print("Disconnected from the server");
	}

	private void create_server()
	{
		server = new ENetMultiplayerPeer();
		server.CreateServer(DEFAULT_PORT, MAX_CLIENT);
		Multiplayer.MultiplayerPeer = server;
	}

	private void join_server()
	{
		client = new ENetMultiplayerPeer();
		client.CreateClient(ip_address, DEFAULT_PORT);
		Multiplayer.MultiplayerPeer = client;
	}
}
