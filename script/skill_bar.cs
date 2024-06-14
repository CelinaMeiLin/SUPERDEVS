using Godot;
using System;
using Devs.project.Autoloads;

public partial class skill_bar : Container
{

	private Timer ShildCooldown;
	private TextureProgressBar ShildBar;
	private bool ShildUnlocked = false;
	private ProgressBar XpBar;
	private Label XpTxt;
	private AnimationPlayer SkillsAnimator;

	private int choice1 = 1;
	private int choice2 = 1;
	private int choice3 = 1;
		
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
		XpBar = GetNode<ProgressBar>("SkillBarPanel/XP/Bar");
		XpTxt = GetNode<Label>("SkillBarPanel/XP/Txt");
		SkillsAnimator = GetNode<AnimationPlayer>("SkillsUp");
		XpBar.Value = 0;

	}

	public override void _Process(double delta)
	{
		ShildBar.Value = ShildBar.MaxValue - ShildCooldown.TimeLeft;
	}

	public void _on_shild_skill_pressed()
	{
		if (!ShildCooldown.IsStopped() || ShildUnlocked == false)
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

	public void UpdateXp(int amount)
	{
		XpBar.Value = amount;
	}

	public void UpdateXpTxt(int lvl)
	{
		XpTxt.Text = $"Lv L {lvl}";
		
		SkillsAnimator.Play("Show");
	}

	private async void _on_choice_1_pressed()
	{
		if (choice1 == 1)
		{
			GetTree().CallGroup("Astra", "UnlockShild");
			ShildUnlocked = true;
		}
		else
		{
			GetNode<Button>("SkillBarPanel/Upgrade/Choice1").Text =
				"--------------------------\nOut of Data\n--------------------------";
			return;
		}

		choice1 += 1;
		await ToSignal(GetTree().CreateTimer(0.5), "timeout");
		SkillsAnimator.Play("Hide");
	}

	private async void _on_choice_2_pressed()
	{
		if (choice2 == 1)
		{
			GetNode<Button>("SkillBarPanel/Upgrade/Choice2").Text =
				"--------------------------\nOut of Data\n--------------------------";
			return;
		}
		else
		{
			GetNode<Button>("SkillBarPanel/Upgrade/Choice2").Text =
				"--------------------------\nOut of Data\n--------------------------";
			return;
		}
		
		choice2 += 1;
		await ToSignal(GetTree().CreateTimer(0.5), "timeout");
		SkillsAnimator.Play("Hide");
	}

	private async void _on_choice_3_pressed()
	{
		if (choice3 == 1)
		{
			GetNode<Button>("SkillBarPanel/Upgrade/Choice3").Text =
				"--------------------------\nOut of Data\n--------------------------";
			return;
		}
		else
		{
			GetNode<Button>("SkillBarPanel/Upgrade/Choice3").Text =
				"--------------------------\nOut of Data\n--------------------------";
			return;
		}
		
		choice3 += 1;
		await ToSignal(GetTree().CreateTimer(0.5), "timeout");
		SkillsAnimator.Play("Hide");
	}
}
