using Devs.project.Autoloads;
using Devs.project.Ressources;
using Godot;

namespace Devs.project.script.HUD;

public partial class GameOverButton: CanvasLayer
{
    [Export] public Node2D Scene;
    
    public void OnRetryPressed()
    {
        GD.Print("Retry");
        GetTree().Paused = false;
        UserPreferences.Data["Coin"] = (int)UserPreferences.Data["Coin"] - PauseManager.Coincollectedinlevel;
        GameManager.EnemyKilled = 0;
        Scene.GetTree().ReloadCurrentScene();
    }
}