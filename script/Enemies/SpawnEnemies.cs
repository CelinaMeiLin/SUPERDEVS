using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Devs.project.Ressources;
using Godot;

namespace Devs.project.script.Enemies;

public partial class SpawnEnemies : Node2D
{
	[Export] private Timer _timer;

	private int Max;
	private int Maxtuto;
	private int MaxLevel2;
	
	private List<Node2D> _liste;
	private List<Node2D> _listelvl1;
	private List<Node2D> _listelvl2;
	private List<Node2D> _listelvl3;
	
	private Timer _timerlvl1;
	
	
	private int Count;
	
	public override void _Ready()
	{
		_liste = new List<Node2D> {GetNode<Node2D>("1"), GetNode<Node2D>("2"), GetNode<Node2D>("3"), GetNode<Node2D>("4")};
		_listelvl1 = new List<Node2D> {GetNode<Node2D>("1"), GetNode<Node2D>("2"), GetNode<Node2D>("3")};
		if (GameManager.CurrentLevel == 3)
		{
			_listelvl2 = new List<Node2D> {GetNode<Node2D>("1"), GetNode<Node2D>("2"), GetNode<Node2D>("3"), GetNode<Node2D>("4"), GetNode<Node2D>("5"), GetNode<Node2D>("6"), GetNode<Node2D>("7"), GetNode<Node2D>("8"), GetNode<Node2D>("9")};
		}
		else if (GameManager.CurrentLevel == 4)
		{
			_listelvl3 = new List<Node2D> {GetNode<Node2D>("1"), GetNode<Node2D>("2"), GetNode<Node2D>("3"), GetNode<Node2D>("4"), GetNode<Node2D>("5"), GetNode<Node2D>("6"), GetNode<Node2D>("7"), GetNode<Node2D>("8"), GetNode<Node2D>("9"), GetNode<Node2D>("10"), GetNode<Node2D>("11"), GetNode<Node2D>("12"), GetNode<Node2D>("13"), GetNode<Node2D>("14"), GetNode<Node2D>("15"), GetNode<Node2D>("16"), GetNode<Node2D>("17")};
		}
		_timer = GetNode<Timer>("Timer");
		_timerlvl1 = GetNode<Timer>("Timer");
		Random generator = new Random();
		Maxtuto = 4;
		MaxLevel2 = 9;
		Max = generator.Next(3, 7);
		//GD.Print(Max);
		Count = 0;
	}
	
	public void _on_timer_timeout()
	{
		
		if (Count <= MaxLevel2 && _liste.Count > 0)
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

	public void OnLevel2TimerTimeout()
	{
		if (Count <= MaxLevel2 && _listelvl2.Count > 0)
		{
			
			//Random rdm = new Random();
			//Node2D spawn = _liste[rdm.Next(0, _liste.Count-1)];
			
			Node2D spawn = _listelvl2[0];

			_listelvl2.Remove(spawn);

			//var spawnpoint = GetNode<Node2D>(rdm.ToString());

			if (spawn ==  GetNode<Node2D>("1")|| spawn == GetNode<Node2D>("6") || spawn == GetNode<Node2D>("8"))
			{
				var spawnEnemy = GD.Load<PackedScene>("res://scene/Enemies/enemy_rhust.tscn");
				var spawnEnemyNode = spawnEnemy.Instantiate<Node2D>();
				spawnEnemyNode.Position = spawn.Position;
				AddChild(spawnEnemyNode);
			}

			if (spawn == GetNode<Node2D>("2") || spawn == GetNode<Node2D>("3") || spawn == GetNode<Node2D>("5"))
			{
				var spawnEnemy = GD.Load<PackedScene>("res://scene/Enemies/ennemy_gus.tscn");
				var spawnEnemyNode = spawnEnemy.Instantiate<Node2D>();
				spawnEnemyNode.Position = spawn.Position;
				AddChild(spawnEnemyNode);
			}

			if (spawn == GetNode<Node2D>("4") || spawn == GetNode<Node2D>("7") || spawn == GetNode<Node2D>("9"))
			{
				var spawnEnemy = GD.Load<PackedScene>("res://scene/ennemy_pow.tscn");
				var spawnEnemyNode = spawnEnemy.Instantiate<Node2D>();
				spawnEnemyNode.Position = spawn.Position;
				AddChild(spawnEnemyNode);
			}

			GD.Print(spawn.Position);
			
			Count++;
			
			_timerlvl1.WaitTime = 3;
		}
		else
		{
			_timerlvl1.Stop();
		}
	}

	private void _on_level_3_timer_timeout()
	{
		if (Count <= 17 && _liste.Count > 0)
		{
			if (_listelvl3.Count <= 0)
			{
				_timer.Stop();
				_timerlvl1.Stop();
				return;
			}
			Node2D spawn = _listelvl3.First();
			_listelvl3.Remove(spawn);
			
			if (spawn ==  GetNode<Node2D>("1")|| spawn == GetNode<Node2D>("3") || spawn == GetNode<Node2D>("4") || spawn == GetNode<Node2D>("7") || spawn == GetNode<Node2D>("10") || spawn == GetNode<Node2D>("13") || spawn == GetNode<Node2D>("17"))
			{
				var spawnEnemy = GD.Load<PackedScene>("res://scene/Enemies/ennemy_gus.tscn");
				var spawnEnemyNode = spawnEnemy.Instantiate<Node2D>();
				spawnEnemyNode.Position = spawn.Position;
				AddChild(spawnEnemyNode);
			}

			if (spawn == GetNode<Node2D>("2") || spawn == GetNode<Node2D>("5") || spawn == GetNode<Node2D>("8") || spawn == GetNode<Node2D>("11") || spawn == GetNode<Node2D>("16"))
			{
				var spawnEnemy = GD.Load<PackedScene>("res://scene/ennemy_pow.tscn");
				var spawnEnemyNode = spawnEnemy.Instantiate<Node2D>();
				spawnEnemyNode.Position = spawn.Position;
				AddChild(spawnEnemyNode);
			}

			if (spawn == GetNode<Node2D>("6") || spawn == GetNode<Node2D>("12") || spawn == GetNode<Node2D>("9") || spawn == GetNode<Node2D>("14") || spawn == GetNode<Node2D>("15"))
			{
				var spawnEnemy = GD.Load<PackedScene>("res://scene/Enemies/enemy_rhust.tscn");
				var spawnEnemyNode = spawnEnemy.Instantiate<Node2D>();
				spawnEnemyNode.Position = spawn.Position;
				AddChild(spawnEnemyNode);
			}
			
			Count++;
		}
		else
		{
			_timer.Stop();
			_timerlvl1.Stop();
		}
		
	}
	
	
}
