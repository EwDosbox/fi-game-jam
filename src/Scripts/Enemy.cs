using Godot;
using System.Threading.Tasks;

public partial class Enemy : Node2D
{
	#region Private Variables
	private bool isMoving = false;
	#endregion
	#region Public Variables
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

	public virtual void Kill()
	{
		isDead = true;
		sprite.Play("dead");
	}

	public virtual async Task SmoothMove()
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
