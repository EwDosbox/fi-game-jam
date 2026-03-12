using Godot;
using System.Threading.Tasks;

public partial class Enemy : Moveable
{
	public async override Task SmoothMove()
	{
		if (isMoving)
			return;
		isMoving = true;

		sprite.PlayType(SpriteType.Move);
		await base.SmoothMove();

		Vector2 target = Position + direction * step;
		target = target.Snapped(Grid.GridVector);

		Tween tween = CreateTween();
		tween.SetEase(Tween.EaseType.Out);
		tween.TweenProperty(this, "position", target, 0.15f);

		await ToSignal(tween, Tween.SignalName.Finished);

		sprite.PlayType(SpriteType.Idle);
		isMoving = false;
	}

}