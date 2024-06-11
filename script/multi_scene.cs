using Godot;
using System;
using Devs.project.Ressources;
using Devs.project.script;

public partial class multi_scene : Node2D
{
	[Export] private PackedScene playerScene;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		int index = 0;
		foreach (var item in GameManager.Players)
		{
			Node2D currentPlayer = playerScene.Instantiate<Node2D>();
			astra ply = currentPlayer.GetNode<CharacterBody2D>("AstraBody") as astra;
			currentPlayer.GetNode<CharacterBody2D>("AstraBody").Name = item.Id.ToString();
			ply.SetUpPlayer(item.Name);
			currentPlayer.Name = item.Id.ToString();
			AddChild(currentPlayer);
			foreach (Node2D spawnPoint in GetTree().GetNodesInGroup("PlayerSpawnPoints"))
			{
				if (int.Parse(spawnPoint.Name) == index)
				{
					currentPlayer.GlobalPosition = spawnPoint.GlobalPosition;
				}
			}

			index += 1;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
