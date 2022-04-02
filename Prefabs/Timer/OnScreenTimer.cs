using Godot;

public class OnScreenTimer : Node
{
	private float _time;
	private Label _label;
	private bool _working;

	[Export]
	public float MaxTime = 60f;

	[Signal]
	public delegate void TimeUp();
	
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
		// todo: stop timer when about to sneeze
		if (_working)
		{
			_time += delta;	
		}
		UpdateDisplay();

		if (_time >= MaxTime && _working)
		{
			EmitSignal(nameof(TimeUp));
		}
		
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
