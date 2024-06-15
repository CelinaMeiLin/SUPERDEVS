using Godot;
using System;
using System.Collections.Generic;
using Devs.project.Autoloads;

namespace Devs.project.Ressources;

public partial class GameManager: Node
{
    public static List<PlayerInfo> Players = new List<PlayerInfo>();
    public static GlobalInfos GlobalInfos = new GlobalInfos();
    
    public static int CurrentLevel = -1;   //accessible que pendant la game
    //public static int CurrentCoin = (int)(UserPreferences.Data["Coin"]);

    public static int EnemyKilled = 0;

    public static void SpawnCoin(Node2D node, Vector2 pos)
    {
        var spawncoin = GD.Load<PackedScene>("res://scene/coin.tscn");
        var spawncoinNode = spawncoin.Instantiate<Sprite2D>();
        spawncoinNode.Position = pos;
        node.AddSibling(spawncoinNode);
        spawncoinNode.GetNode<AnimationPlayer>("AnimationPlayer").Play("Spawn");
        /*
        if (GameManager.CurrentLevel == (int) UserPreferences.Data["CurrentLevel"]+1)
        {
            var spawncoin = GD.Load<PackedScene>("res://scene/coin.tscn");
            var spawncoinNode = spawncoin.Instantiate<Sprite2D>();
            spawncoinNode.Position = pos;
            node.AddSibling(spawncoinNode);
            spawncoinNode.GetNode<AnimationPlayer>("AnimationPlayer").Play("Spawn");
        }
        */
    }
}