using Godot;

public class SneezeScale : Node2D
{
	private ColorRect _scaleRect;
	private float _fillagePercent;
	private float _maxY;
	private float _currentDelta;
	private bool _sneezing;

	[Signal]
	public delegate void OnSneeze();
	
	[Signal]
	public delegate void OnNotSneeze();
	
	public override void _Ready()
	{
		_sneezing = false;
		_fillagePercent = 0f;
		_currentDelta = 0.005f;
		_scaleRect = GetNode<ColorRect>("Scale");
		_maxY = GetNode<ColorRect>("Background").RectSize.y;
	}

	public override void _PhysicsProcess(float delta)
	{
		UpdateDisplay();
		if (_sneezing)
		{
			_fillagePercent += _currentDelta;

			if (_fillagePercent > 1.0f)
			{
				_sneezing = false;
				_fillagePercent = 0f;
				EmitSignal(nameof(OnSneeze));
			}	
		}

		base._PhysicsProcess(delta);
	}

	// todo: remove
	public void Reset()
	{
		_fillagePercent = 0f;
	}

	public void Subtract(float value)
	{
		if (_sneezing)
		{
			_fillagePercent -= value;
			if (_fillagePercent <= 0f)
			{
				_sneezing = false;
				EmitSignal(nameof(OnNotSneeze));
			}	
		}
	}

	public void InitSneeze(float newDelta)
	{
		_currentDelta = newDelta;
		_sneezing = true;
	}

	private void UpdateDisplay()
	{
		// maxY = 730
		// percent0 = position_y = 730
		//			  size_y     = 0
		//

		var y = _fillagePercent * _maxY;
		_scaleRect.RectSize = new Vector2(_scaleRect.RectSize.x, y);
		_scaleRect.RectPosition = new Vector2(_scaleRect.RectPosition.x, _maxY - y);
	}
}
