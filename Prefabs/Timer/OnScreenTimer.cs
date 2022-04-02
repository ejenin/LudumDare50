using Godot;

public class OnScreenTimer : Node
{
	private float _time;
	private Label _label;
	
	public override void _Ready()
	{
		_label = GetNode<Label>("Label");
		_time = 0f;
	}

	public float Time => _time;

	public override void _PhysicsProcess(float delta)
	{
		_time += delta;
		UpdateDisplay();
		base._PhysicsProcess(delta);
	}

	private void UpdateDisplay()
	{
		_label.Text = $"{_time:0.00} sec";
	}
}