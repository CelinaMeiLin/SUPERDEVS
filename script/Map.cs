using Devs.project.Autoloads;
using Devs.project.Ressources;
using Godot;
using Godot.Collections;

namespace Devs.project.script;

public partial class Map : Control
{
    [Export] Control variable;
    [Export] private AudioStreamPlayer backgroundMusic;
    
    [Export] private Button _tutorial;
    [Export] private Button _level1;
    [Export] private Button _level2;
    [Export] private Button _level3;

    public override void _Ready()
    {
        GD.Print(GameManager.CurrentLevel);
        GD.Print((int)UserPreferences.Data["CurrentLevel"]);
        
        //_tutorial = GetNode<Button>("VBoxContainer/Tuto");
        //_level1 = GetNode<Button>("VBoxContainer/Level1");
        //_level2 = GetNode<Button>("VBoxContainer/Level2");
        //_level3 = GetNode<Button>("VBoxContainer/Level3");
		
        if ((int)UserPreferences.Data["CurrentLevel"] > 0)
        {
            _tutorial.Disabled = false;
        }
        if ((int)UserPreferences.Data["CurrentLevel"] >= 1)
        {
            _level1.Disabled = false;
        }
        if ((int)UserPreferences.Data["CurrentLevel"] >= 2)
        {
            _level2.Disabled = false;
        }
        if ((int)UserPreferences.Data["CurrentLevel"] >= 3)
        {
            _level3.Disabled = false;
        }

    }
    
    public override void _Process(double delta)
    {
        if(Input.IsActionJustPressed("ui_cancel")){
            variable.Visible=true;
            Input.MouseMode = Input.MouseModeEnum.Visible;
        }
    }
    
    public void OnInventoryPressed()
    {
        GetTree().ChangeSceneToFile("res://scene/player_statistics.tscn");
    }
	
    public void OnTutoPressed()
    {
        UserPreferences.Data["IsLevel"] = 1;
        //UserPreferences.Data["CurrentLevel"] = 0;
        UserPreferences.Save();
        GameManager.CurrentLevel = 1;
        GetTree().ChangeSceneToFile("res://scene/Levels/tuto.tscn");
    }

    public void OnLevel1Pressed()
    {
        UserPreferences.Data["IsLevel"] = 1;
        //UserPreferences.Data["CurrentLevel"] = 1;
        UserPreferences.Save();
        GameManager.CurrentLevel = 2;
        GetTree().ChangeSceneToFile("res://scene/Levels/level_1.tscn");
    }
	
    public void OnLevel2Pressed()
    {
        UserPreferences.Data["IsLevel"] = 1;
        //UserPreferences.Data["CurrentLevel"] = 2;
        UserPreferences.Save();
        GameManager.CurrentLevel = 3;
        GetTree().ChangeSceneToFile("res://scene/Levels/level_2.tscn");
    }
	
    public void OnLevel3Pressed()
    {
        UserPreferences.Data["IsLevel"] = 1;
        //UserPreferences.Data["CurrentLevel"] = 3;
        UserPreferences.Save();
        GameManager.CurrentLevel = 4;
        GetTree().ChangeSceneToFile("res://scene/Levels/level_final.tscn");
    }
    
    public void OnPlayerStatisticsPressed()
    {
        GetTree().ChangeSceneToFile("res://scene/player_statistics.tscn");
    }
    
    public void OnExitPressed()
    {
        GetTree().Quit();
    }
    
    public void OnResetPressed()
    {
        UserPreferences.Delete();
        GetTree().Quit();
    }
    
}