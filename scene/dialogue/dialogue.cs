using Godot;
using System;
using System.Collections.Generic;

public partial class dialogue : Control
{

	private Label _dialogueLabel;
	private List<string> _dialogueLines;
	private int _currentLineIndex = 0;

	public override void _Ready()
	{
		
		GD.Print("ici");
		// Initialisation des éléments de l'interface utilisateur
		_dialogueLabel = GetNode<Label>("Panel/Label");

		// Charger les dialogues (ici un exemple simplifié avec des dialogues en dur)
		_dialogueLines = new List<string>
		{
			"Bonjour, comment ça va ?",
			"Je vais bien, merci ! Et toi ?",
			"Moi aussi, merci de demander."
		};

		// Afficher le premier dialogue
		ShowDialogue();
	}

	private void ShowDialogue()
	{
		if (_currentLineIndex < _dialogueLines.Count)
		{
			_dialogueLabel.Text = _dialogueLines[_currentLineIndex];
		}
	}

	private void NextDialogue()
	{
		_currentLineIndex++;
		if (_currentLineIndex < _dialogueLines.Count)
		{
			ShowDialogue();
		}
		else
		{
			// Terminer le dialogue
			QueueFree();
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("ui_accept")) 
		{
			NextDialogue();
		}
	}
}
