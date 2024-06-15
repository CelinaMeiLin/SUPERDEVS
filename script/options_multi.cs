using Godot;
using System;
using Devs.project.Ressources;
using System.Linq;
using Devs.project.script;
using PlayerInfo = Devs.project.Ressources.PlayerInfo;

public partial class options_multi : Control
{
	[Export] 
	private int port = 8910;

	[Export]
	private string ip_address = "test";

	private ENetMultiplayerPeer peer;
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

		GetNode<Label>("VBoxContainer/Label").Text = ip_address;
		
		Multiplayer.PeerConnected += PeerConected;
		Multiplayer.PeerDisconnected += PeerDisconnected;
		Multiplayer.ConnectedToServer += ConnectedToServer;
		Multiplayer.ConnectionFailed += ConnectionFailed;
		if (OS.GetCmdlineArgs().Contains("--server"))
		{
			hostGame();
		}
	}

	private void ConnectionFailed()
	{
		GD.Print("Connection failed.");
	}

	private void ConnectedToServer()
	{
		GD.Print("Connected to server.");
		RpcId(1,"sendPlayerInformation", GetNode<LineEdit>("HBoxContainer/LineEdit").Text, Multiplayer.GetUniqueId());
	}

	private void PeerDisconnected(long id)
	{
		GD.Print("Player disconnected " + id.ToString());
		GameManager.Players.Remove(GameManager.Players.Where(i => i.Id == id).First<PlayerInfo>());
		var players = GetTree().GetNodesInGroup("Player");
		foreach (var item in players)
		{
			if (item.Name == id.ToString())
			{
				item.QueueFree();
			}
		}
	}

	private void PeerConected(long id)
	{
		GD.Print("Player connected " + id.ToString());
	}

	public void _on_back_button_down()
	{
		Visible=false;
		GetTree().ChangeSceneToFile("res://scene/main_menu.tscn");
		//var gameScene=GD.Load<PackedScene>("res://scene/main_menu.tscn");
		//var gameScenebackNode=gameScene.Instantiate();
		//GetParent().AddChild(gameScenebackNode);
	}


	private void hostGame()
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
	private void _on_host_button_down()
	{
		hostGame();
		sendPlayerInformation(GetNode<LineEdit>("HBoxContainer/LineEdit").Text, 1);
	}


	private void _on_join_button_down()
	{
		ip_address = GetNode<LineEdit>("Ip").Text;
		peer = new ENetMultiplayerPeer();
		peer.CreateClient(ip_address, port);
		
		peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);
		Multiplayer.MultiplayerPeer = peer;
		GD.Print("Joining Game!");
	}

	private void _on_start_button_down()
	{
		foreach (var item in GameManager.Players)
		{
			GD.Print(item.Name + " is playing");
		}
		
		//var scene = ResourceLoader.Load<PackedScene>("res://scene/Levels/multi_scene.tscn").Instantiate<Node2D>();
		//GetTree().Root.AddChild(scene);
		//Hide();

		//GetTree().ChangeSceneToFile("res://scene/Levels/multi_scene.tscn");
		Rpc("startGame");
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	private void startGame()
	{
		GetTree().ChangeSceneToFile("res://scene/Levels/multi_scene.tscn");
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer)]
	private void sendPlayerInformation(string name, int id)
	{
		PlayerInfo playerInfo = new PlayerInfo()
		{
			Name = name,
			Id = id
		};
		if (!GameManager.Players.Contains(playerInfo))
		{
			GameManager.Players.Add(playerInfo);
		}

		if (Multiplayer.IsServer())
		{
			foreach (var item in GameManager.Players)
			{
				Rpc("sendPlayerInformation", item.Name, item.Id);
			}
		}
	}
}



