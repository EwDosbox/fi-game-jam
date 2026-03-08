using Godot;
using System;
using System.Threading.Tasks;

public partial class EnemyW : Enemy
{
	#region Private Variables
	private bool movingToLoop = true;
	private bool isMoving = false;
	private bool hitWall = false;
	#endregion
	#region Public Variables
	#endregion
	#region References
	[Export]
	private int NoOfSpaces = 1;
	[Export]
	private Marker2D startNode;
	[Export]
	private Marker2D loopNode;
	[Export]
	private Grid gridScript;
	#endregion

	public override async Task SmoothMove()
	{
		if (isMoving || base.isDead)
			return;
		if (hitWall)
		{
			hitWall = false;
			return;
		}
		isMoving = true;

		Vector2 start = startNode.GlobalPosition;
		Vector2 loop = loopNode.GlobalPosition;

		Vector2 target = movingToLoop ? loop : start;
		Vector2 direction = (target - GlobalPosition).Normalized();

		Vector2 moveAmount = direction * NoOfSpaces * gridScript.GridSize;

		KinematicCollision2D collision = MoveAndCollide(moveAmount);
		if (collision != null)
		{
			GD.Print("EnemyW hit something: ", collision.GetCollider());
			isMoving = false;
			return;
		}

		if (GlobalPosition.DistanceTo(target) < 1f)
		{
			hitWall = true;
			movingToLoop = !movingToLoop;
		}
		GlobalPosition = GlobalPosition.Snapped(gridScript.GridVector);

		isMoving = false;

		await Task.CompletedTask;
	}
}