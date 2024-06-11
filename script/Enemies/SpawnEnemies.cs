using System;
using Godot;

namespace Devs.project.script.Enemies;

public partial class SpawnEnemies : Node2D
{
    [Export] private Timer _timer;
    
    private int Max;
    
    private int Count;
    
    public override void _Ready()
    {
        _timer = GetNode<Timer>("Timer");
        Random generator = new Random();
        Max = generator.Next(3, 7);
        //GD.Print(Max);
        Count = 0;
    }
    
    public void _on_timer_timeout()
    {
        GD.Print("SPAWN");
        if (Count <= Max)
        {
            GD.Print("ÇA RENTRE DEDANS");
            Random r = new Random();
            int randomspawn = r.Next(1, 4);
            var spawnpoint = GetNode<Node2D>(randomspawn.ToString());
        
            var spawnEnemy = GD.Load<PackedScene>("res://scene/Enemies/ennemy_gus.tscn");
            var spawnEnemyNode = spawnEnemy.Instantiate<Node2D>();
            spawnEnemyNode.Position = spawnpoint.Position;
            AddChild(spawnEnemyNode);
            
            GD.Print(spawnpoint.Position);
            
            Count++;
        }
        else
        {
            _timer.Stop();
        }
    }


    public void OnLevel1Timeout()
    {
        if (Count <= Max)
        {
            Random r = new Random();
            int randomspawn = r.Next(1, 4);
            var spawnpoint = GetNode<Node2D>(randomspawn.ToString());
        
            var spawnEnemy = GD.Load<PackedScene>("res://scene/ennemy_pow.tscn");
            var spawnEnemyNode = spawnEnemy.Instantiate<Node2D>();
            spawnEnemyNode.Position = spawnpoint.Position;
            AddChild(spawnEnemyNode);
            
            GD.Print(spawnpoint.Position);
            
            Count++;
        }
        else
        {
            _timer.Stop();
        }
    }
}