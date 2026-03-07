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
	private Grid gridScript;
	[Export]
	private Area2D hitbox;
	[Export]
	private RayCast2D rayCast;
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


		if (direction == lastDirection)
			bedPower++;
		else
			bedPower = 1;

		lastDirection = direction;
		Vector2 movementVector = direction * (bedPower * gridScript.GridSize);

		rayCast.TargetPosition = movementVector;
		rayCast.ForceRaycastUpdate();

		Vector2 finalTarget;

		if (rayCast.IsColliding())
		{
			Vector2 hitPoint = rayCast.GetCollisionPoint();
			finalTarget = hitPoint - (direction * 5);
			bedPower = 1;
		}
		else
			finalTarget = Position + movementVector;

		finalTarget = finalTarget.Snapped(gridScript.GridVector);

		sprite.Play("move");

		Tween tween = CreateTween();
		tween.SetEase(Tween.EaseType.Out);
		tween.SetTrans(Tween.TransitionType.Quad);
		tween.TweenProperty(this, "position", finalTarget, 0.15f);

		await ToSignal(tween, Tween.SignalName.Finished);

		sprite.Play("idle");
		isMoving = false;
	}

	public void Touched(Node body)
	{
		if (body is Enemy enemy)
		{
			if (bedPower > 2)
				enemy.Kill();
			else
				GD.Print("DIED");
		}
	}
}
