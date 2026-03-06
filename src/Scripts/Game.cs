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
	private Node2D Player;
	#endregion

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{

	}

	private void GameTick()
	{


	}
}
