using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Devs.project.script.Enemies;

public partial class SpawnEnemies : Node2D
{
	[Export] private Timer _timer;

	private int Max;
	private int Maxtuto;
	
	private List<Node2D> _liste;
	private List<Node2D> _listelvl1;
	
	private Timer _timerlvl1;
	
	
	private int Count;
	
	public override void _Ready()
	{
		_liste = new List<Node2D> {GetNode<Node2D>("1"), GetNode<Node2D>("2"), GetNode<Node2D>("3"), GetNode<Node2D>("4")};
		_listelvl1 = new List<Node2D> {GetNode<Node2D>("1"), GetNode<Node2D>("2"), GetNode<Node2D>("3")};

		_timer = GetNode<Timer>("Timer");
		_timerlvl1 = GetNode<Timer>("Timer");
		Random generator = new Random();
		Maxtuto = 4;
		Max = generator.Next(3, 7);
		//GD.Print(Max);
		Count = 0;
	}
	
	public void _on_timer_timeout()
	{
		
		if (Count <= Maxtuto && _liste.Count > 0)
		{
			Random rdm = new Random();
			Node2D spawn = _liste[rdm.Next(0, _liste.Count-1)];


			_liste.Remove(spawn);

			//var spawnpoint = GetNode<Node2D>(rdm.ToString());
		
			var spawnEnemy = GD.Load<PackedScene>("res://scene/Enemies/ennemy_gus.tscn");
			var spawnEnemyNode = spawnEnemy.Instantiate<Node2D>();
			spawnEnemyNode.Position = spawn.Position;
			AddChild(spawnEnemyNode);
			
			Count++;
		}
		else
		{
			_timer.Stop();
		}
	}


	public void OnLevel1Timeout()
	{
		if (Count <= Maxtuto && _liste.Count > 0)
		{
			
			//Random rdm = new Random();
			//Node2D spawn = _liste[rdm.Next(0, _liste.Count-1)];

			Node2D spawn = _liste[0];

			_liste.Remove(spawn);

			//var spawnpoint = GetNode<Node2D>(rdm.ToString());
		
			var spawnEnemy = GD.Load<PackedScene>("res://scene/ennemy_pow.tscn");
			var spawnEnemyNode = spawnEnemy.Instantiate<Node2D>();
			spawnEnemyNode.Position = spawn.Position;
			AddChild(spawnEnemyNode);

			GD.Print(spawn.Position);
			
			Count++;
			
			_timerlvl1.WaitTime = 6;
		}
		else
		{
			_timerlvl1.Stop();
		}
	}
	
	
	
}
