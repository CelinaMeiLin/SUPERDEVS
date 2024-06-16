using Godot;
using System;
using System.Collections.Generic;

public partial class dialoguelvl2 : Control
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
			"OH ! Tu es là !",
			"J'ai pas retrouvé papa...",
			"Mais j’ai vu maman aller vers la bas, \net toujours avec des personnes bizarres…",
			"euh...",
			"C'est quoi ce point rouge sur ta tête ?"
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
		if (Input.IsActionJustPressed("SuiteD")) 
		{
			NextDialogue();
		}
	}
}
