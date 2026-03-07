using Godot;
using System;

public partial class Game : Node2D
{
	#region Private Vars
	private int turnCounter = 0;
	private bool isTurnRunning = false;
	#endregion
	#region Public Vars

	public int Turn { get { return turnCounter; } }
	#endregion
	#region References
	[Export]
	private Player PlayerScript;
	#endregion

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		if (isTurnRunning)
			return;

		Vector2 dir = Vector2.Zero;

		if (Input.IsActionJustPressed("move_left"))
			dir = Vector2.Left;
		else if (Input.IsActionJustPressed("move_right"))
			dir = Vector2.Right;
		else if (Input.IsActionJustPressed("move_up"))
			dir = Vector2.Up;
		else if (Input.IsActionJustPressed("move_down"))
			dir = Vector2.Down;

		if (dir == Vector2.Zero)
			return;

		GameTick(dir);

	}

	private async void GameTick(Vector2 playerDirection)
	{
		isTurnRunning = true;

		// Enemies

		await PlayerScript.SmoothMove(playerDirection);

		turnCounter++;

		await ToSignal(GetTree().CreateTimer(0.2f), SceneTreeTimer.SignalName.Timeout);

		isTurnRunning = false;
	}
}
