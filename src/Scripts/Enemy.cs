using Godot;
using System.Threading.Tasks;

public partial class Enemy : CharacterBody2D
{
	#region Private Variables
	#endregion

	#region Public Variables
	public bool isDead = false;
	public virtual Vector2 Direction { get; set; }
	public virtual int NoOfSquares { get; set; } = 1;
	public virtual bool IsMoving { get; set; }
	[Export] public virtual AnimatedSprite2D Sprite { get; set; }
	[Export] public virtual Grid GridScript { get; set; }
	[Export] public virtual CollisionShape2D Collision { get; set; }
	#endregion

	#region References
	#endregion

	public virtual void Kill()
	{
		isDead = true;
		Collision.Disabled = true;
		Sprite.Play("dead");
	}

	public virtual async Task SmoothMove()
	{
		if (IsMoving)
			return;
		IsMoving = true;

		Sprite.Play("move");

		Vector2 target = Position + Direction * NoOfSquares;
		target = target.Snapped(GridScript.GridVector);

		GlobalPosition = target;

		Sprite.Play("idle");
		IsMoving = false;
	}

}
