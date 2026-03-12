using Godot;
using System;
using System.Threading.Tasks;

public partial class Player : Moveable
{
	#region Private Variables
	[Export] private int bedPower = 1;
	private Vector2 lastDirection = Vector2.Zero;
	#endregion
	#region Public Variables
	public int BedPower { get { return bedPower; } }
	#endregion
	#region References
	[Export] private AudioStreamPlayer2D moveSound;
	#endregion

	public async override Task SmoothMove()
	{
		if (isMoving)
			return;
		isMoving = true;

		await base.SmoothMove();
		sprite.PlayType(SpriteType.Move);
		moveSound.Play();

		if (direction == lastDirection)
			bedPower++;
		else
			bedPower = 1;

		sprite.PlayType(SpriteType.Idle);
		isMoving = false;
	}
}
