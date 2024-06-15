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
        GameManager.EnemyKilled = 0;
        
        UserPreferences.Data["IsLevel"] = 0;
        //GetTree().CallDeferred("quit");
        Input.MouseMode = Input.MouseModeEnum.Visible;

        GD.Print(GameManager.CurrentLevel);
        GD.Print((int)UserPreferences.Data["CurrentLevel"]);
        UserPreferences.Save();	
        if (GameManager.CurrentLevel >= (int)UserPreferences.Data["CurrentLevel"])
        {
            GD.Print("UPDATE");
				
            UserPreferences.Data["CurrentLevel"] = GameManager.CurrentLevel-1;
            UserPreferences.Save();
        }
        if (GameManager.CurrentLevel == 1) //Pour ne pas save les pièces du tuto
        {
            Devs.project.Ressources.PauseManager parent = (Devs.project.Ressources.PauseManager)GetParent();
            //GD.Print( parent.NbCoinsBeforeTutorial);
            UserPreferences.Data["Coin"] = parent.NbCoinsBeforeTutorial;
            UserPreferences.Save();
        }
            
        GameManager.EnemyKilled = 0;
            
        UserPreferences.Data["IsLevel"] = 0;
        UserPreferences.Save();
        PauseManager.Coincollectedinlevel = 0;

        GetTree().ChangeSceneToFile("res://scene/map.tscn");
    }
}