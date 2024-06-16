using Devs.project.Autoloads;
using Devs.project.script;
using Godot;

namespace Devs.project.Ressources;

public partial class PauseManager : Node2D
{
	[Export] public Node Path;
	[Export] public CanvasLayer GameOverPath;
	
	public int NbCoinsBeforeTutorial;

	public static int Coincollectedinlevel;
	
	public static int NbCoinsBeforeLevel;

	private static CanvasLayer _pauseMenu;
	private Button _resumeButton;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NbCoinsBeforeTutorial = (int)UserPreferences.Data["Coin"];
		Coincollectedinlevel = 0;
		//GameManager.CurrentCoin = (int)UserPreferences.Data["Coin"];
		_pauseMenu = GetNode<CanvasLayer>(Path.GetPath());
		//_resumeButton = GetNode<Button>("/root/Tuto/PauseMenu/ResumeButton");
		_pauseMenu.Hide();
		GameOverPath.Hide();
		ShowDialogueBox();
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_cancel"))
		{
			if ((int)UserPreferences.Data["IsLevel"] == 1)
			{
				_Pause();
			}
		}
	}


	public void _Pause()
	{
		UserPreferences.Data["CurrentLevel"] = 0;
		UserPreferences.Save();
		GetTree().Paused = true;
		//GD.Print("ÇA DOIT FREEZE");
		_pauseMenu.Show();
		//GD.Print("ÇA DOIT AFFICHER LE BOUTON");
		Input.MouseMode = Input.MouseModeEnum.Visible;
			
	}

	public static void _OnResume()
	{
		UserPreferences.Data["CurrentLevel"] = 1;
		UserPreferences.Save();
		_pauseMenu.GetTree().Paused = false;
		_pauseMenu.Hide();
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		
	}

	public static void Die(CanvasLayer gameOverPath)
	{
		//GameManager.CurrentCoin = (int)UserPreferences.Data["Coin"];
		UserPreferences.Data["Coin"] = NbCoinsBeforeLevel;
		GD.Print("MET LE SCREEN");
		
		gameOverPath.GetTree().Paused = true;
		GD.Print("freeze");
		gameOverPath.Show();
		GD.Print("c'est bon");

		Input.MouseMode = Input.MouseModeEnum.Visible;
	}
	
	private void ShowDialogueBox()
	{
		CanvasLayer dialogueBoxInstance = (CanvasLayer)ResourceLoader.Load<PackedScene>("res://scene/dialogue/dialogue1.tscn").Instantiate();
		GD.Print("show");
		AddChild(dialogueBoxInstance);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
