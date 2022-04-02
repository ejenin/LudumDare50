using Godot;

public class Librarian : Node2D
{
	private Label _label;
	
	[Export]
	public NodePath StartPath { get; set; }
	[Export]
	public NodePath EndPath { get; set; }

	private Position2D _start;
	private Position2D _end;

	private LibrarianState _state;
	private bool _walkingTowardsEnd;
	private float _speed = 2.5f;
	
	public override void _Ready()
	{
		_label = GetNode<Label>("Label");
		_state = LibrarianState.Walking;
		_walkingTowardsEnd = true;

		if (StartPath != null && EndPath != null)
		{
			_start = GetNode<Position2D>(StartPath);
			_end = GetNode<Position2D>(EndPath);
			Position = _start.GlobalPosition;
		}
	}

	public void Stare() => _state = LibrarianState.Staring;
	public void Walk() => _state = LibrarianState.Walking;
	public void Rage() => _state = LibrarianState.Raging;

	public override void _PhysicsProcess(float delta)
	{
		_label.Text = _state.ToString();
		if (_state == LibrarianState.Walking && _start != null)
		{
			var positionDelta = _walkingTowardsEnd ? _speed : _speed * -1f;
			Position = new Vector2(Position.x + positionDelta, Position.y);

			if (_walkingTowardsEnd && Position.x > _end.GlobalPosition.x)
			{
				_walkingTowardsEnd = false;
			}
			else if (!_walkingTowardsEnd && Position.x <= _start.GlobalPosition.x)
			{
				_walkingTowardsEnd = true;
			}
		}

		base._PhysicsProcess(delta);
	}

	private enum LibrarianState
	{
		Walking = 0,
		Staring = 1,
		Raging = 2
	}
}
