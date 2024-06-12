using Devs.project.Autoloads;
using Godot;

namespace Devs.project.script;

public partial class PlayerStatistics : Control
{
    public override void _Ready()
    {
        GD.Print("OUI");
        GetNode<Button>("HBoxContainer/VBoxContainer/Health").Text = UserPreferences.Data["MaxHealth"].ToString() + " Health";

        if ((int)UserPreferences.Data["Coin"] >= 3)
        {
            GetNode<Button>("HBoxContainer/VBoxContainer4/Health").Disabled = false;
        }
        else
        {
            GetNode<Button>("HBoxContainer/VBoxContainer4/Health").Disabled = true;
        }
    }

    public void OnHealthSelected()
    {
        UserPreferences.Data["MaxHealth"] = (int)UserPreferences.Data["MaxHealth"] + 100;
        UserPreferences.Data["Coin"] = (int)UserPreferences.Data["Coin"] - 3;
        UserPreferences.Save();
    }
    
    public void OnBackPressed()
    {
        GetTree().ChangeSceneToFile("res://scene/map.tscn");
    }
}