using Godot;

public class Student : Node2D
{
	private Label _label;
	
	private StudentState _state;

	public override void _Ready()
	{
		_label = GetNode<Label>("Label");
		_state = StudentState.Reading;
	}

	public override void _PhysicsProcess(float delta)
	{
		_label.Text = _state.ToString();
		base._PhysicsProcess(delta);
	}

	public void PrepareSneeze() => _state = StudentState.PreparingToSneeze;
	public void Sneeze() => _state = StudentState.Sneezing;
	public void Read() => _state = StudentState.Reading;

	private enum StudentState
	{
		Reading = 0,
		PreparingToSneeze = 1,
		Sneezing = 2,
		Fearing = 3,
	}
}
