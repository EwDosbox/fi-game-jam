using Godot;
using System;
using System.Threading.Tasks;

public partial class Player : Node2D
{
	#region Private Variables
	[Export]
	private int bedPower = 1;
	private Vector2 lastDirection = Vector2.Zero;
	private bool isMoving = false;
	#endregion
	#region Public Variables
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

	public async Task SmoothMove(Vector2 direction)
	{
		if (isMoving)
			return;
		isMoving = true;

		sprite.Play("move");

		if (direction == lastDirection)
			bedPower++;
		else
			bedPower = 1;

		lastDirection = direction;
		int move = bedPower * gridScript.GridSize;

		Vector2 target = Position + direction * move;
		target = target.Snapped(gridScript.GridVector);

		Tween tween = CreateTween();
		tween.SetEase(Tween.EaseType.Out);
		tween.TweenProperty(this, "position", target, 0.15f);

		await ToSignal(tween, Tween.SignalName.Finished);

		sprite.Play("idle");
		isMoving = false;
	}
}
