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
	
	public override void _Ready()
	{
		_rnd = new Random();
		_scale = GetNode<SneezeScale>("SneezeScale");
		_timer = GetNode<OnScreenTimer>("OnScreenTimer");
		_sneezeTimer = GetNode<Timer>("Timer");

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
		StartTimer();
	}
	
	private void _on_SneezeScale_OnSneeze()
	{
		_scale.Reset();
		StartTimer();
	}

	private void StartTimer()
	{
		_sneezeTimer.WaitTime = GetRandomWaitTime();
		_sneezeTimer.Start();
	}
	
	private void _on_Timer_timeout()
	{
		var delta = 0.0095f;
		if (_timer.Time <= 20f)
		{
			delta = 0.005f;
		}
		else if (_timer.Time <= 40f)
		{
			delta = 0.0075f;
		}
		
		_scale.InitSneeze(delta);
	}
}
