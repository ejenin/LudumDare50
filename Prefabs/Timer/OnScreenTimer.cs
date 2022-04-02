using Godot;

public class OnScreenTimer : Node
{
	private float _time;
	private Label _label;
	private bool _working;
	
	public override void _Ready()
	{
		_label = GetNode<Label>("Label");
		_time = 0f;
		_working = false;
	}

	public float Time => _time;

	public void Start() => _working = true;

	public override void _PhysicsProcess(float delta)
	{
		if (_working)
		{
			_time += delta;	
		}
		UpdateDisplay();
		base._PhysicsProcess(delta);
	}

	public void Stop() => _working = false;

	public void Reset()
	{
		_working = false;
		_time = 0f;
	}

	private void UpdateDisplay()
	{
		_label.Text = $"{_time:0.00} sec";
	}
}
