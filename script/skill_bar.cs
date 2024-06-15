using Godot;
using System;
using Devs.project.Autoloads;

public partial class skill_bar : Container
{

	private Timer ShildCooldown;
	private TextureProgressBar ShildBar;
	private bool ShildUnlocked = false;

	private Timer ShockCooldown;
	private TextureProgressBar ShockBar;
	private bool ShockUnlocked = false;

	private Timer ZapCooldown;
	private TextureProgressBar ZapBar;
	private bool ZapUnlocked = false;
	
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
		
		GetNode<Button>("SkillBarPanel/Upgrade/Choice1").Text = "- Shild -\n\nUnlock new Ability";
		GetNode<Button>("SkillBarPanel/Upgrade/Choice2").Text = "- Shock -\n\nUnlock New Ability";
		GetNode<Button>("SkillBarPanel/Upgrade/Choice3").Text = "- Zap -\n\nUnlock New Ability";
		
		ShildCooldown = GetNode<Timer>("SkillBarPanel/HBoxContainer/ShildSkill/ShildCooldown");
		ShildBar = GetNode<TextureProgressBar>("SkillBarPanel/HBoxContainer/ShildSkill/ShildBar");
		ShockCooldown = GetNode<Timer>("SkillBarPanel/HBoxContainer/ShockSkill/ShockCooldown");
		ShockBar = GetNode<TextureProgressBar>("SkillBarPanel/HBoxContainer/ShockSkill/ShockBar");
		ZapCooldown = GetNode<Timer>("SkillBarPanel/HBoxContainer/ZapSkill/ZapCooldown");
		ZapBar = GetNode<TextureProgressBar>("SkillBarPanel/HBoxContainer/ZapSkill/ZapBar");
		
		XpBar = GetNode<ProgressBar>("SkillBarPanel/XP/Bar");
		XpTxt = GetNode<Label>("SkillBarPanel/XP/Txt");
		SkillsAnimator = GetNode<AnimationPlayer>("SkillsUp");
		XpBar.Value = 0;

	}

	public override void _Process(double delta)
	{
		if (ShildUnlocked && !ShildCooldown.IsStopped())
			ShildBar.Value = ShildBar.MaxValue - ShildCooldown.TimeLeft;
		if (ShockUnlocked && !ShockCooldown.IsStopped())
			ShockBar.Value = ShockBar.MaxValue - ShockCooldown.TimeLeft;
		if (ZapUnlocked && !ZapCooldown.IsStopped())
			ZapBar.Value = ZapBar.MaxValue - ZapCooldown.TimeLeft;
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


	private void _on_shock_skill_pressed()
	{
		if (!ShockCooldown.IsStopped() || ShockUnlocked == false)
		{
			return;
		}

		ShockBar.Visible = true;
		int cooldown = 90;
		ShockBar.MaxValue = cooldown;
		ShockCooldown.WaitTime = cooldown; //easy to change
		ShockCooldown.Start();
		GetTree().CallGroup("Astra", "SkillShock");
	}

	private void _on_shock_cooldown_timeout()
	{
		ShockBar.Visible = false;
		GetTree().CallGroup("Astra", "UnlockShock");
	}


	private void _on_zap_skill_pressed()
	{
		if (!ZapCooldown.IsStopped() || ZapUnlocked == false)
		{
			return;
		}

		ZapBar.Visible = true;
		int cooldown = 60;
		ZapBar.MaxValue = cooldown;
		ZapCooldown.WaitTime = cooldown;
		ZapCooldown.Start();
		GetTree().CallGroup("Astra", "SkillZap");
	}

	private void _on_zap_cooldown_timeout()
	{
		ZapBar.Visible = false;
		GetTree().CallGroup("Astra", "UnlockZap");
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
			//for next choice
			GetNode<Button>("SkillBarPanel/Upgrade/Choice1").Text =
				"--------------------------\nOut of Data\n--------------------------";
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
			GetTree().CallGroup("Astra", "UnlockShock");
			ShockUnlocked = true;
			//for next choice
			GetNode<Button>("SkillBarPanel/Upgrade/Choice2").Text =
				"--------------------------\nOut of Data\n--------------------------";
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
			GetTree().CallGroup("Astra", "UnlockZap");
			ZapUnlocked = true;
			
			GetNode<Button>("SkillBarPanel/Upgrade/Choice3").Text =
				"--------------------------\nOut of Data\n--------------------------";
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
