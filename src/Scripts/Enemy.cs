using Godot;
using System.Threading.Tasks;

public partial class Enemy : CharacterBody2D
{
	#region Private Variables
	private bool isMoving = false;
	#endregion

	#region Public Variables
	public bool isDead = false;
	public virtual Vector2 Direction { get; set; }
	public virtual int NoOfSquares { get; set; }
	#endregion

	#region References
	[Export]
	private AnimatedSprite2D sprite;
	[Export]
	private Grid gridScript;
	[Export]
	private CollisionShape2D collision;
	#endregion

	public virtual void Kill()
	{
		isDead = true;
		collision.Disabled = true;
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