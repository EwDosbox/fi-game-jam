using Godot;
using System;
using System.Threading.Tasks;

public partial class EnemyW : Enemy
{
	#region Private Variables
	private bool movingToLoop = true;
	private bool hitWall = false;
	private bool hasBonked = false;
	#endregion

	#region Public Variables
	public bool HitWall { get { return hitWall; } set { hitWall = value; } }
	public bool MovingToLoop { get { return movingToLoop; } set { movingToLoop = value; } }
	public bool HasBonked { get { return hasBonked; } set { hasBonked = value; } }
	#endregion

	#region References
	[Export] private Marker2D startMarker;
	[Export] private Marker2D loopMarker;
	#endregion

	public override void _Ready()
	{
		this.NoOfSquares = 1;
	}

	public override async Task SmoothMove()
	{
		if (IsMoving)
			return;

		// --- BONKED STATE ---
		// We are in the "waiting" turn after a collision.
		// Spend this turn recovering: re-enable collider, swap to idle, and SKIP movement.
		// The direction was already flipped when the bonk was registered, so next turn
		// we'll move normally in the correct direction.
		if (hasBonked)
		{
			GD.Print($"{Name} recovering from bonk — skipping this turn.");
			hasBonked = false;
			Collision.SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
			Sprite.Play("idle");
			return; // <-- skip movement entirely this turn
		}

		// --- WALL HIT STATE ---
		// Direction was already flipped when hitWall was set last turn.
		// Just clear the flag and fall through to the normal move below.
		if (hitWall)
		{
			hitWall = false;
		}

		// --- NORMAL MOVE ---
		IsMoving = true;

		Vector2 ultimateGoal = movingToLoop ? loopMarker.GlobalPosition : startMarker.GlobalPosition;
		Vector2 direction = GlobalPosition.DirectionTo(ultimateGoal).Round();
		Vector2 targetPosition = GlobalPosition + (direction * NoOfSquares * GridScript.GridSize);
		targetPosition = targetPosition.Snapped(GridScript.GridVector);

		Sprite.Play("move");

		Tween tween = CreateTween();
		tween.SetEase(Tween.EaseType.Out);
		tween.SetTrans(Tween.TransitionType.Quad);
		tween.TweenProperty(this, "global_position", targetPosition, 0.15f);
		await ToSignal(tween, Tween.SignalName.Finished);

		Sprite.Play("idle");

		// If we've arrived at our current goal, flip direction for next turn.
		if (GlobalPosition.DistanceTo(ultimateGoal) < (GridScript.GridSize / 2f))
		{
			hitWall = true;
			movingToLoop = !movingToLoop;
		}

		IsMoving = false;

		// Check for enemy-enemy collisions AFTER the move settles.
		CheckCollisions();
	}

	public void CheckCollisions()
	{
		var touching = GetTouchingBodies();
		foreach (Node2D body in touching)
		{
			if (body is EnemyW otherEnemy && otherEnemy != this)
			{
				// Skip if either is already in bonk-recovery to avoid double-triggering.
				if (this.hasBonked || otherEnemy.HasBonked)
					continue;

				GD.Print($"BONK: {Name} and {otherEnemy.Name} collided!");

				// 1. Flip both directions so they retrace their steps next turn.
				this.movingToLoop = !this.movingToLoop;
				otherEnemy.MovingToLoop = !otherEnemy.MovingToLoop;

				// 2. Mark both as bonked — next SmoothMove call will be their skip/recovery turn.
				this.hasBonked = true;
				otherEnemy.HasBonked = true;

				// 3. Disable colliders so they don't re-trigger while overlapping.
				this.Collision.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
				otherEnemy.Collision.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);

				// 4. Play bonk animations.
				this.Sprite.Play("bonk");
				if (otherEnemy.Sprite != null)
					otherEnemy.Sprite.Play("bonk");

				break; // One bonk per turn is enough.
			}

			if (body is Player)
			{
				GD.Print($"{Name} caught the player!");
			}
		}
	}

	public Godot.Collections.Array<Node2D> GetTouchingBodies()
	{
		var spaceState = GetWorld2D().DirectSpaceState;

		var query = new PhysicsShapeQueryParameters2D();
		query.Shape = Collision.Shape;
		query.Transform = GlobalTransform;
		query.CollisionMask = CollisionLayer | CollisionMask;
		query.Exclude = new Godot.Collections.Array<Rid> { GetRid() };

		var results = spaceState.IntersectShape(query);
		var touching = new Godot.Collections.Array<Node2D>();

		foreach (var result in results)
		{
			if (result["collider"].Obj is Node2D body)
				touching.Add(body);
		}

		return touching;
	}
}
