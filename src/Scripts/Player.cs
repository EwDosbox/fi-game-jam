using Godot;
using System;

public partial class Player : Node2D
{
	#region Private Variables
	[Export]
	private int bedPower = 1;
	#endregion
	#region Public Variables
	#endregion
	#region References
	[Export]
	public Grid gridScript;
	#endregion
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public void Move(Vector2 direction)
	{
		int move = bedPower * gridScript.GridSize;
		Position += direction * move;
	}
}
