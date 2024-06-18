using Godot;
using System;
using Devs.project.Ressources;
using Devs.project.script;

public partial class multi_scene : Node2D
{
	[Export] private PackedScene playerScene;
	[Export] private PackedScene MedicalKit;
	[Export] private PackedScene GusBody_;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameManager.CurrentLevel = -2;
		
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

	public void _on_health_pack_timer_timeout()
	{
		int rand = new Random().Next(4);
		Marker2D spawn = GetNode<Marker2D>("SpawnPositions/LBottom");
		if (rand == 1)
		{
			spawn = GetNode<Marker2D>("SpawnPositions/LTop");
		}
		else if (rand == 2)
		{
			spawn = GetNode<Marker2D>("SpawnPositions/RTop");
		}
		else if (rand == 3)
		{
			spawn = GetNode<Marker2D>("SpawnPositions/RBottom");
		}

		medical_kit medicalKit = MedicalKit.Instantiate<medical_kit>();
		medicalKit.GlobalPosition = spawn.GlobalPosition;
		AddChild(medicalKit);
	}
	
	public void _on_gus_timer_timeout()
	{
		int rand = new Random().Next(4);
		Marker2D spawn = GetNode<Marker2D>("SpawnPositiongus/LBottom");
		if (rand == 1)
		{
			spawn = GetNode<Marker2D>("SpawnPositiongus/LTop");
		}
		else if (rand == 2)
		{
			spawn = GetNode<Marker2D>("SpawnPositiongus/RTop");
		}
		else if (rand == 3)
		{
			spawn = GetNode<Marker2D>("SpawnPositiongus/RBottom");
		}

		var spawnEnemy = GD.Load<PackedScene>("res://scene/Enemies/ennemy_gus.tscn");
		var spawnEnemyNode = spawnEnemy.Instantiate<Node2D>();
		spawnEnemyNode.Position = spawn.Position;
		AddChild(spawnEnemyNode);
	}
}



