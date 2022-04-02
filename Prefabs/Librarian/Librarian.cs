using Godot;

public class Librarian : Node2D
{
	[Export]
	public NodePath StartPath { get; set; }
	[Export]
	public NodePath EndPath { get; set; }

	private AnimatedSprite _animatedSprite;
	private Position2D _start;
	private Position2D _end;

	private LibrarianState _state;
	private bool _walkingTowardsEnd;
	private float _speed = 2.5f;
	
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		_state = LibrarianState.Walking;
		_walkingTowardsEnd = true;

		if (StartPath != null && EndPath != null)
		{
			_start = GetNode<Position2D>(StartPath);
			_end = GetNode<Position2D>(EndPath);
			Position = _start.GlobalPosition;
		}
	}

	private void UpdateAnimation()
	{
		_animatedSprite.Animation = _state.ToString().ToLower();
	}

	public void Stare()
	{
		_state = LibrarianState.Staring;
		UpdateAnimation();
	}

	public void Walk()
	{
		_state = LibrarianState.Walking;
		UpdateAnimation();
	}

	public void Rage()
	{
		_state = LibrarianState.Raging;
		UpdateAnimation();
	}

	public override void _PhysicsProcess(float delta)
	{
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

			_animatedSprite.FlipH = !_walkingTowardsEnd;
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
