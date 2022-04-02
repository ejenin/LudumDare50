using Godot;

public class SneezeScale : Node2D
{
	private ColorRect _scaleRect;
	private float _fillagePercent;
	private float _maxY;
	
	public override void _Ready()
	{
		_fillagePercent = 0f;
		_scaleRect = GetNode<ColorRect>("Scale");
		_maxY = GetNode<ColorRect>("Background").RectSize.y;
	}

	public override void _PhysicsProcess(float delta)
	{
		_fillagePercent += 0.005f;
		UpdateDisplay();

		if (_fillagePercent > 1.0f)
		{
			// todo: send end game signal
			_fillagePercent = 0f;
		}
		
		base._PhysicsProcess(delta);
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
		// _scaleRect
	}
}
