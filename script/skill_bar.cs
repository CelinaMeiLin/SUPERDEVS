using Godot;
using System;
using Devs.project.Autoloads;

public partial class skill_bar : Container
{

	private Timer ShildCooldown;
	private TextureProgressBar ShildBar;
		
		
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Button>("SkillBarPanel/VBoxContainer/Skill1").Text = UserPreferences.Data["MaxHealth"].ToString();
		GetNode<Button>("SkillBarPanel/VBoxContainer/Skill2").Text = UserPreferences.Data["AttackDamage"].ToString();
		GetNode<Button>("SkillBarPanel/VBoxContainer/Skill3").Text =
			Math.Round((float)UserPreferences.Data["FireRate"], 3).ToString();
		GetNode<Button>("SkillBarPanel/VBoxContainer2/Skill1").Text = UserPreferences.Data["MovementSpeed"].ToString();
		GetNode<Button>("SkillBarPanel/VBoxContainer2/Skill2").Text = UserPreferences.Data["DashCooldown"].ToString();
		ShildCooldown = GetNode<Timer>("SkillBarPanel/HBoxContainer/ShildSkill/ShildCooldown");
		ShildBar = GetNode<TextureProgressBar>("SkillBarPanel/HBoxContainer/ShildSkill/ShildBar");

	}

	public override void _Process(double delta)
	{
		//ShildBar.Value = ShildBar.MaxValue - ShildCooldown.TimeLeft;
	}

	public void _on_shild_skill_pressed()
	{
		if (!ShildCooldown.IsStopped())
		{
			return;
		}

		ShildBar.Visible = true;
		int cooldown = 90;
		ShildBar.MaxValue = cooldown;
		ShildCooldown.WaitTime = cooldown; //easy to change
		ShildCooldown.Start();
		GetTree().CallGroup("Astra", "SkillShild");
	}

	private void _on_shild_cooldown_timeout()
	{
		ShildBar.Visible = false;
		GetTree().CallGroup("Astra", "UnlockShild");
	}
}
