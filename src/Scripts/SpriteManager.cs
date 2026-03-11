
using Godot;

public partial class SpriteManager : AnimatedSprite2D
{
    [Export] private AnimatedSprite2D sprite;

    public void PlayType(SpriteType type)
    {
        sprite.Play(type.ToString());
    }
}
public enum SpriteType
{
    Dead,
    Move,
    MoveL,
    MoveR,
    MoveU,
    MoveD,
}