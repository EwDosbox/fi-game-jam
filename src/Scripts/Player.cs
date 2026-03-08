using Godot;
using System;
using System.Threading.Tasks;

public partial class Player : CharacterBody2D
{
	#region Private Variables
	[Export]
	private int bedPower = 1;
	private Vector2 lastDirection = Vector2.Zero;
	private bool isMoving = false;
	private bool isDead = false;
	#endregion
	#region Public Variables
	public bool IsDead { get { return isDead; } set { isDead = value; } }
	#endregion
	#region References
	[Export]
	private AnimatedSprite2D sprite;
	[Export]
	private Grid gridScript;
	#endregion

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
		Vector2 targetPosition = GlobalPosition + (direction * (bedPower * gridScript.GridSize));
		targetPosition = targetPosition.Snapped(gridScript.GridVector);

		sprite.Play("move");

		await TweenPhysicsMove(targetPosition);

		sprite.Play("idle");
		isMoving = false;
	}

	private async Task TweenPhysicsMove(Vector2 target)
	{
		float duration = 0.2f;
		float elapsed = 0.0f;
		Vector2 startPos = GlobalPosition;

		while (elapsed < duration)
		{
			float delta = (float)GetProcessDeltaTime();
			elapsed += delta;

			float t = Mathf.Clamp(elapsed / duration, 0, 1);
			float curve = t * t * (3 - 2 * t);

			Vector2 nextPoint = startPos.Lerp(target, curve);
			Vector2 moveVelocity = nextPoint - GlobalPosition;

			KinematicCollision2D collision = MoveAndCollide(moveVelocity);

			if (collision != null)
			{
				Node collider = collision.GetCollider() as Node;

				if (collider is Enemy enemy)
				{
					if (bedPower > 3)
					{
						enemy.Kill();
						continue;
					}
					else
					{
						this.isDead = true;
						GD.Print("He dead");
					}
				}

				bedPower = 1;
				break;
			}

			await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

		}

		GlobalPosition = GlobalPosition.Snapped(gridScript.GridVector);
	}
}
