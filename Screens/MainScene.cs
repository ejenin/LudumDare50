using Godot;
using System;

public class MainScene : Node
{
	private Random _rnd;
	
	// wait_times:
	// 10-20 = 4-5
	// 20-40 = 3-4
	// 40-60 = 2-3

	private OnScreenTimer _timer;
	private SneezeScale _scale;
	private Timer _sneezeTimer;
	private Librarian _librarian;
	private Student _student;
	private PressKeyPrompt _prompt;
	
	private const float PlayerSneezingDelta = 0.065f;
	
	public override void _Ready()
	{
		_rnd = new Random();
		_scale = GetNode<SneezeScale>("SneezeScale");
		_timer = GetNode<OnScreenTimer>("OnScreenTimer");
		_sneezeTimer = GetNode<Timer>("Timer");
		_librarian = GetNode<Librarian>("Librarian");
		_student = GetNode<Student>("Student");
		_prompt = GetNode<PressKeyPrompt>("PressKeyPrompt");
		_prompt.Visible = false;

		StartTimer();
	}

	private float GetRandomWaitTime()
	{
		if (_timer.Time <= 20f)
		{
			return _rnd.Next(4, 6) + Convert.ToSingle(_rnd.NextDouble());
		}
		else if (_timer.Time <= 40f)
		{
			return _rnd.Next(3, 5) + Convert.ToSingle(_rnd.NextDouble());
		}
		
		return _rnd.Next(2, 4) + Convert.ToSingle(_rnd.NextDouble());
	}
	
	private void _on_SneezeScale_OnNotSneeze()
	{
		_librarian.Walk();
		_student.Read();
		_prompt.Visible = false;
		StartTimer();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_accept"))
		{
			_scale.Subtract(PlayerSneezingDelta);
		}
		base._Input(@event);
	}

	private void _on_SneezeScale_OnSneeze()
	{
		_librarian.Rage();
		_student.Sneeze();
		_timer.Stop();
		_prompt.Visible = false;
	}

	private void StartTimer()
	{
		_sneezeTimer.WaitTime = GetRandomWaitTime();
		_sneezeTimer.Start();
	}
	
	private void _on_Timer_timeout()
	{
		var delta = 0.006f;
		if (_timer.Time <= 20f)
		{
			delta = 0.005f;
		}
		else if (_timer.Time <= 40f)
		{
			delta = 0.0055f;
		}
		
		_prompt.Visible = true;
		_scale.InitSneeze(delta);
		_librarian.Stare();
		_student.PrepareSneeze();
	}
}
