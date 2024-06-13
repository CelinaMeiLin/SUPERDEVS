using Devs.project.Autoloads;
using Godot;

namespace Devs.project.script;

public partial class PlayerStatistics : Control
{
    public override void _Ready()
    {
        GD.Print("OUI");

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
        UserPreferences.Data["FireRate"] = (float)UserPreferences.Data["FireRate"] - 0.2f;
        UserPreferences.Data["Coin"] = (int)UserPreferences.Data["Coin"] - 8;
        UserPreferences.Save();
    }

    public void OnMSPressed()
    {
        UserPreferences.Data["MovementSpeed"] = (int)UserPreferences.Data["MovementSpeed"] + 50;
        UserPreferences.Data["Coin"] = (int)UserPreferences.Data["Coin"] - 5;
        UserPreferences.Save();
    }

    public void OnDTPressed()
    {
        UserPreferences.Data["DashCooldown"] = (int)UserPreferences.Data["DashCooldown"] - 0.2;
        UserPreferences.Data["Coin"] = (int)UserPreferences.Data["Coin"] - 6;
        UserPreferences.Save();
    }
    
    public void OnBackPressed()
    {
        GetTree().ChangeSceneToFile("res://scene/map.tscn");
    }

    public override void _Process(double delta)
    { 
        GetNode<Button>("GridContainer/Health").Text = UserPreferences.Data["MaxHealth"].ToString() + " Health";
        GetNode<Button>("GridContainer/AttackDamage").Text = UserPreferences.Data["AttackDamage"].ToString() + " Attack Damage";
        GetNode<Button>("GridContainer/FireRate").Text = UserPreferences.Data["FireRate"].ToString() + " Fire Rate";
        GetNode<Button>("GridContainer/MovementSpeed").Text = UserPreferences.Data["MovementSpeed"].ToString() + " Movement Speed";
        GetNode<Button>("GridContainer/DashTimer").Text = UserPreferences.Data["DashCooldown"].ToString() + " Dash Cooldown";
        
        if ((int)UserPreferences.Data["Coin"] >= 3)
        {
            GetNode<Button>("GridContainer/Health2").Disabled = false;
        }
        else
        {
            GetNode<Button>("GridContainer/Health2").Disabled = true;
        }

        if ((int)UserPreferences.Data["Coin"] >= 5)
        {
            GetNode<Button>("GridContainer/AttackDamage2").Disabled = false;
        }
        else
        {
            GetNode<Button>("GridContainer/AttackDamage2").Disabled = true;
        }

        if ((int)UserPreferences.Data["Coin"] >= 8)
        {
            if ((float)UserPreferences.Data["FireRate"] >= 0.1f)
            {
                GetNode<Button>("GridContainer/FireRate2").Disabled = false;

            }
        }
        else
        {
            GetNode<Button>("GridContainer/FireRate2").Disabled = true;
        }

        if ((int)UserPreferences.Data["Coin"] >= 5)
        {
         GetNode<Button>("GridContainer/MovementSpeed2").Disabled = false;
        }
        else
        {
            GetNode<Button>("GridContainer/MovementSpeed2").Disabled = true;
        }

        if ((int)UserPreferences.Data["Coin"] >= 6)
        {
            if ((double)UserPreferences.Data["DashCooldown"] >= 0.2)
            {
                GetNode<Button>("GridContainer/DashTimer2").Disabled = false;

            }
        }
        else
        {
            
            GetNode<Button>("GridContainer/DashTimer2").Disabled = true;
        }
    }
}