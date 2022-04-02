using Godot;
using System;

public class MainScene : Node
{
	private Random _rnd;

	private OnScreenTimer _onScreenTimer;
	private SneezeScale _scale;
	private Timer _sneezeTimer;
	private Librarian _librarian;
	private Student _student;
	private PressKeyPrompt _prompt;
	private Node2D _gameplayInterfaceRoot;
	private Node2D _menuInterfaceRoot;
	private Timer _showMenuTimer;
	private Label _gameOverLabel;
	private Label _startLabel;
	private Timer _hideStartTimer;
	
	private const float PlayerSneezingDelta = 0.065f;
	
	public override void _Ready()
	{
		_rnd = new Random();
		_gameplayInterfaceRoot = GetNode<Node2D>("GameplayInterface");
		_scale = GetNode<SneezeScale>("GameplayInterface/SneezeScale");
		_onScreenTimer = GetNode<OnScreenTimer>("GameplayInterface/OnScreenTimer");
		_gameOverLabel = GetNode<Label>("GameplayInterface/GameOverLabel");
		_startLabel = GetNode<Label>("GameplayInterface/StartLabel");
		_hideStartTimer = GetNode<Timer>("GameplayInterface/HideStartGameLabelTimer");
		_prompt = GetNode<PressKeyPrompt>("GameplayInterface/PressKeyPrompt");
		
		_menuInterfaceRoot = GetNode<Node2D>("MenuInterface");
		_sneezeTimer = GetNode<Timer>("Timer");
		_librarian = GetNode<Librarian>("Librarian");
		_student = GetNode<Student>("Student");
		_showMenuTimer = GetNode<Timer>("ShowMenuTimer");
		_prompt.Visible = false;
	}

	private float GetRandomWaitTime()
	{
		// wait_times:
		// 10-20 = 4-5
		// 20-40 = 3-4
		// 40-60 = 2-3
		
		if (_onScreenTimer.Time <= 20f)
		{
			return _rnd.Next(4, 6) + Convert.ToSingle(_rnd.NextDouble());
		}
		else if (_onScreenTimer.Time <= 40f)
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
			if (!_gameplayInterfaceRoot.Visible)
			{
				StartGame();
			}
			_scale.Subtract(PlayerSneezingDelta);
		}
		base._Input(@event);
	}

	private void StartGame()
	{
		_gameplayInterfaceRoot.Visible = true;
		_menuInterfaceRoot.Visible = false;
		_onScreenTimer.Start();
		_hideStartTimer.Start();
		StartTimer();
	}

	private void _on_SneezeScale_OnSneeze()
	{
		_librarian.Rage();
		_student.Sneeze();
		_onScreenTimer.Stop();
		_prompt.Visible = false;
		_showMenuTimer.Start();
		_gameOverLabel.Visible = true;
	}

	private void StartTimer()
	{
		_sneezeTimer.WaitTime = GetRandomWaitTime();
		_sneezeTimer.Start();
	}
	
	private void _on_Timer_timeout()
	{
		var delta = 0.006f;
		if (_onScreenTimer.Time <= 20f)
		{
			delta = 0.005f;
		}
		else if (_onScreenTimer.Time <= 40f)
		{
			delta = 0.0055f;
		}
		
		_prompt.Visible = true;
		_scale.InitSneeze(delta);
		_librarian.Stare();
		_student.PrepareSneeze();
	}
	
	private void _on_ShowMenuTimer_timeout()
	{
		_gameplayInterfaceRoot.Visible = false;
		_menuInterfaceRoot.Visible = true;
		_librarian.Walk();
		_student.Read();
		_onScreenTimer.Reset();
		_gameOverLabel.Visible = false;
		_startLabel.Visible = true;
	}
	
	private void _on_HideStartGameLabelTimer_timeout()
	{
		_startLabel.Visible = false;
	}

}
