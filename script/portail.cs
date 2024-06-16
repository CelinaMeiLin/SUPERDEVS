using Godot;
using System;
using Devs.project.Autoloads;
using Devs.project.Ressources;

public partial class portail : Sprite2D
{
	
    private CollisionShape2D _collisionShape;
    private AnimatedSprite2D _sprite;

    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite2D>("Sprite2D");
        _sprite.Visible = false;
        _collisionShape = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");
        _collisionShape.Disabled = true;
    }

    public void _on_area_2d_body_entered(Node body)
    {
        if (body is astra)
        {
            UserPreferences.Data["IsLevel"] = 0;
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
                //GD.Print( parent.NbCoinsBeforeTutorial);
               UserPreferences.Data["Coin"] = parent.NbCoinsBeforeTutorial;
               UserPreferences.Save();
            }
            
            GameManager.EnemyKilled = 0;
            
            UserPreferences.Data["IsLevel"] = 0;
            UserPreferences.Save();
            PauseManager.Coincollectedinlevel = 0;
            CallDeferred("CHANGE");
        }
    }

    public void CHANGE()
    {
        GetTree().ChangeSceneToFile("res://scene/map.tscn");
    }
	
	
    public override void _Process(double delta)
    {
       
        if (GameManager.CurrentLevel == 1)
        {
            if (GameManager.EnemyKilled == 4)
            {
                _sprite.Visible = true;
                _collisionShape.Disabled = false;
            }
        }

        if (GameManager.CurrentLevel == 2)
        {
            if (GameManager.EnemyKilled == 5)
            {
                _sprite.Visible = true;
                _collisionShape.Disabled = false;
                
            }
        }

        if (GameManager.CurrentLevel == 3)
        {
            if (GameManager.EnemyKilled == 9)
            {
                _sprite.Visible = true;
                _collisionShape.Disabled = false;
            }
        }

        if (GameManager.CurrentLevel == 4)
        {
            if (GameManager.EnemyKilled == 17)
            {
                _sprite.Visible = true;
                _collisionShape.Disabled = false;
            }
        }
        
    }
    
}