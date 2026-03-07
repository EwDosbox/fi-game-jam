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


		Vector2 dir = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (dir == Vector2.Zero)
			return;

		if (Mathf.Abs(dir.X) > Mathf.Abs(dir.Y))
			dir = new Vector2(Mathf.Sign(dir.X), 0);
		else
			dir = new Vector2(0, Mathf.Sign(dir.Y));

		GameTick(dir);

	}

	private async void GameTick(Vector2 playerDirection)
	{
		isTurnRunning = true;

		// Enemies

		await PlayerScript.SmoothMove(playerDirection);

		turnCounter++;

		isTurnRunning = false;
	}
}
