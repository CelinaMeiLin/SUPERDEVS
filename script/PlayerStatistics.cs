using Devs.project.Autoloads;
using Godot;

namespace Devs.project.script;

public partial class PlayerStatistics : Control
{
    public override void _Ready()
    {
        GD.Print("OUI");
        GetNode<Button>("HBoxContainer/VBoxContainer/Health").Text = UserPreferences.Data["MaxHealth"].ToString() + " Health";
        GetNode<Button>("HBoxContainer/VBoxContainer/AttackDamage").Text = UserPreferences.Data["AttackDamage"].ToString() + " Attack Damage";
        GetNode<Button>("HBoxContainer/VBoxContainer/FireRate").Text = UserPreferences.Data["FireRate"].ToString() + " Fire Rate";

        if ((int)UserPreferences.Data["Coin"] >= 3)
        {
            GetNode<Button>("HBoxContainer/VBoxContainer4/Health").Disabled = false;
        }
        else
        {
            GetNode<Button>("HBoxContainer/VBoxContainer4/Health").Disabled = true;
        }

        if ((int)UserPreferences.Data["Coin"] >= 5)
        {
            GetNode<Button>("HBoxContainer/VBoxContainer4/AttackDamage").Disabled = false;
        }
        else
        {
            GetNode<Button>("HBoxContainer/VBoxContainer4/AttackDamage").Disabled = true;
        }

        if ((int)UserPreferences.Data["Coin"] >= 8)
        {
            GetNode<Button>("HBoxContainer/VBoxContainer4/FireRate").Disabled = false;
        }
        else
        {
            GetNode<Button>("HBoxContainer/VBoxContainer4/FireRate").Disabled = true;
        }
    }

    public void OnHealthSelected()
    {
        UserPreferences.Data["MaxHealth"] = (int)UserPreferences.Data["MaxHealth"] + 100;
        UserPreferences.Data["Coin"] = (int)UserPreferences.Data["Coin"] - 3;
        UserPreferences.Save();
    }

    public void OnAttackDamagePressed()
    {
        UserPreferences.Data["AttackDamage"] = (int)UserPreferences.Data["AttackDamage"] + 100;
        UserPreferences.Data["Coin"] = (int)UserPreferences.Data["Coin"] - 5;
        UserPreferences.Save();
    }

    public void OnFireRatePressed()
    {
        UserPreferences.Data["FireRate"] = (float)UserPreferences.Data["FireRate"] + 1f;
        UserPreferences.Data["Coin"] = (int)UserPreferences.Data["Coin"] - 8;
        UserPreferences.Save();
    }
    
    public void OnBackPressed()
    {
        GetTree().ChangeSceneToFile("res://scene/map.tscn");
    }
}