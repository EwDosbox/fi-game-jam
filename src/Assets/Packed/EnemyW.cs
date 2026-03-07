using Godot;
using System;
using System.Threading.Tasks;

public partial class EnemyW : Node2D
{
	#region Private Variables
	private bool isMoving = false;
	#endregion
	#region Public Variables
	public bool isDead = false;
	[Export]
	public Vector2 Direction;
	public int NoOfSquares = 1;
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
		if (isMoving)
			return;
		isMoving = true;

		sprite.Play("move");

		Vector2 target = Position + Direction * NoOfSquares;
		target = target.Snapped(gridScript.GridVector);

		Tween tween = CreateTween();
		tween.SetEase(Tween.EaseType.Out);
		tween.TweenProperty(this, "position", target, 0.15f);

		await ToSignal(tween, Tween.SignalName.Finished);

		sprite.Play("idle");
		isMoving = false;
	}
}
