using Godot;
using System;

public partial class Game : Node2D
{
	#region Private Vars
	private int turnCounter = 0;
	private bool isTurnRunning = false;
	private bool canPlayerMove = true;
	#endregion
	#region Public Vars

	public int Turn { get { return turnCounter; } }
	#endregion
	#region References
	[Export]
	private Player PlayerScript;
	[Export]
	private EnemyManager enemyManager;
	[Export]
	private PackedScene nextScene;
	#endregion

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		if (isTurnRunning || !canPlayerMove || !PlayerScript.IsAlive)
			return;

		Vector2I dir;

		if (Input.IsActionJustPressed("move_left"))
			dir = Vector2I.Left;
		else if (Input.IsActionJustPressed("move_right"))
			dir = Vector2I.Right;
		else if (Input.IsActionJustPressed("move_up"))
			dir = Vector2I.Up;
		else if (Input.IsActionJustPressed("move_down"))
			dir = Vector2I.Down;
		else
			return;

		GameTick(dir);

	}

	private async void GameTick(Vector2I playerDirection)
	{
		isTurnRunning = true;

		await enemyManager.MoveAll();

		PlayerScript.Direction = playerDirection;
		await PlayerScript.SmoothMove();

		turnCounter++;

		await ToSignal(GetTree().CreateTimer(0.2f), SceneTreeTimer.SignalName.Timeout);

		if (enemyManager.AllEnemiesDead)
			NextScene();

		isTurnRunning = false;
	}

	private void NextScene()
	{
		if (nextScene != null)
			GetTree().ChangeSceneToPacked(nextScene);
	}

	public void ReloadScene()
	{
		GetTree().ReloadCurrentScene();
	}
	public void ToggleControl()
	{
		canPlayerMove = !canPlayerMove;
	}
}