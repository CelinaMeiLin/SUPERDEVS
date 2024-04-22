using Godot;

namespace Devs.project.Autoloads;

public partial class Global : Node
{
    Signal GainedCoins;
    public static Global Instance { get; private set; }
    public int Coins = 0;
    
    public override void _Ready()
    {
        Instance = this;
    }
    public void GainCoin(int coinGain)
    {
        Coins += coinGain;
        EmitSignal("GainedCoins", coinGain);
    }
}   