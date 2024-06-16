using Godot;
using System;
using System.Collections.Generic;

public partial class dialoguelvl1 : Control
{

	private Label _dialogueLabel;
	private List<string> _dialogueLines;
	private int _currentLineIndex = 0;

	public override void _Ready()
	{
		// Initialisation des éléments de l'interface utilisateur
		_dialogueLabel = GetNode<Label>("Panel/Label");

		// Charger les dialogues (ici un exemple simplifié avec des dialogues en dur)
		_dialogueLines = new List<string>
		{
			"waf waf...",
			"...",
			"J'ai vu quelques cho \n" +
			"Attention j'ai vu de drôle de personne en jaune au bout..."
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
			GetTree().Paused = false;
			GetParent().GetNode<TextureRect>("TextureRect").QueueFree();
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

