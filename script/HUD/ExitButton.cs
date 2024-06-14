using Devs.project.Autoloads;
using Devs.project.Ressources;
using Godot;

namespace Devs.project.script.HUD;

public partial class ExitButton : Button
{
    public void OnExitButtonPressed()
    {
        GetTree().Paused = false;
        UserPreferences.Data["Coin"] = (int)UserPreferences.Data["Coin"] - PauseManager.Coincollectedinlevel;

        GetTree().ChangeSceneToFile("res://scene/map.tscn");
    }
}