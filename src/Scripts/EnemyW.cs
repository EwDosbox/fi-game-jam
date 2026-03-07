using Godot;
using System;
using System.Threading.Tasks;

public partial class EnemyW : Enemy
{
	#region Private Variables
	private bool isMoving = false;
	#endregion
	#region Public Variables
	#endregion
	#region References
	[Export]
	private AnimatedSprite2D sprite;
	#endregion
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public override async Task SmoothMove()
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
