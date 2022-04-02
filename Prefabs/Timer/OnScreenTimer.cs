using Godot;

public class OnScreenTimer : Node
{
	private float _time;
	private Label _label;
	private bool _working;
	private SneezeScale _scale;
	
	[Export]
	public NodePath SneezeScale { get; set; }

	[Export]
	public float MaxTime = 60f;

	[Signal]
	public delegate void TimeUp();
	
	public override void _Ready()
	{
		_label = GetNode<Label>("Label");
		_scale = GetNode<SneezeScale>(SneezeScale);
		_time = 0f;
		_working = false;
	}

	public float Time => _time;

	public void Start() => _working = true;

	public override void _PhysicsProcess(float delta)
	{
		if (_working)
		{
			var toAdd = delta;
			if (_scale.Sneezing)
			{
				toAdd *= 0.15f;
			}
			_time += toAdd;	
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
