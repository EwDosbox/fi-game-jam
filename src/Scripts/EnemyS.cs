using Godot;
using System;
using System.Threading.Tasks;

public partial class EnemyS : Node2D
{
	#region Private Variables
	private bool isMoving = false;
	#endregion
	#region Public Variables
	[Export]
	public bool isDead = false;
	public Vector2 Direction;
	public int NoOfSquares;
	#endregion
	#region References
	[Export]
	private AnimatedSprite2D sprite;
	[Export]
	public Grid gridScript;
	#endregion
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public void Kill()
	{
		isDead = true;
		sprite.Play("dead");
	}

	public async Task SmoothMove()
	{
		return;
	}
}
