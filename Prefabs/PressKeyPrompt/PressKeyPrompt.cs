using System;
using Godot;

public class PressKeyPrompt : Node2D
{
	private Label _label;

	public override void _Ready()
	{
		_label = GetNode<Label>("Label");
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventJoypadButton)
		{
			_label.Text = "Mash 'A' to delay the inevitable!";
		}
		else if (@event is InputEventKey)
		{
			_label.Text = "Mash 'Space' to delay the inevitable!";
		}
		
		
		base._Input(@event);
	}
}
