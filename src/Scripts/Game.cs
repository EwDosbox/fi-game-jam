using Godot;
using System;

public partial class Game : Node2D
{
	#region Private Vars
	private int turnCounter = 0;
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
		Vector2 dir = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (dir != Vector2.Zero)
		{
			if (Mathf.Abs(dir.X) > Mathf.Abs(dir.Y))
				dir = new Vector2(Mathf.Sign(dir.X), 0);
			else
				dir = new Vector2(0, Mathf.Sign(dir.Y));

			GameTick(dir);
		}
	}

	private void GameTick(Vector2 playerDirection)
	{
		PlayerScript.Move(playerDirection);
	}
}
