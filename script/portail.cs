using Godot;
using System;
using Devs.project.Autoloads;
using Devs.project.Ressources;

public partial class portail : Sprite2D
{
	
    private CollisionShape2D _collisionShape;

    public override void _Ready()
    {
        _collisionShape = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");
	   
    }

    public void _on_area_2d_body_entered(Node body)
    {
        if (body is astra)
        {
            //GetTree().CallDeferred("quit");
            Input.MouseMode = Input.MouseModeEnum.Visible;

            GD.Print(GameManager.CurrentLevel);
            GD.Print((int)UserPreferences.Data["CurrentLevel"]);
            UserPreferences.Save();	
            if (GameManager.CurrentLevel >= (int)UserPreferences.Data["CurrentLevel"])
            {
                GD.Print("UPDATE");
				
                UserPreferences.Data["CurrentLevel"] = GameManager.CurrentLevel;
                UserPreferences.Save();
            }
            if (GameManager.CurrentLevel == 1) //Pour ne pas save les pièces du tuto
            {
                Devs.project.Ressources.PauseManager parent = (Devs.project.Ressources.PauseManager)GetParent();
                GD.Print( parent.NbCoinsBeforeTutorial);
                UserPreferences.Data["Coin"] = parent.NbCoinsBeforeTutorial;
                UserPreferences.Save();
            }
            UserPreferences.Data["IsLevel"] = 0;
            UserPreferences.Save();
            PauseManager.Coincollectedinlevel = 0;
            GetTree().ChangeSceneToFile("res://scene/map.tscn");
        }
    }
	
	
    public override void _Process(double delta)
    {
    }
}