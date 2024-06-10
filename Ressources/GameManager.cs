using Godot;
using System;
using System.Collections.Generic;

namespace Devs.project.Ressources;

public partial class GameManager: Node
{
    public static List<PlayerInfo> Players = new List<PlayerInfo>();
    public static GlobalInfos GlobalInfos = new GlobalInfos();
    
    public static int CurrentLevel = -1;   //accessible que pendant la game
}