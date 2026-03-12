
using Godot;

public partial class SpriteManager : AnimatedSprite2D
{
    [Export] private AnimatedSprite2D sprite;

    public void PlayType(SpriteType type)
    {
        string toPlay = "";
        switch (type)
        {
            case SpriteType.Idle:
                {
                    toPlay = "idle";
                    break;
                }
            case SpriteType.Dead:
                {
                    toPlay = "dead";
                    break;
                }
            case SpriteType.Move:
                {
                    toPlay = "move";
                    break;
                }
            case SpriteType.MoveD:
                {
                    toPlay = "move_d";
                    break;
                }
            case SpriteType.MoveL:
                {
                    toPlay = "move_l";
                    break;
                }
            case SpriteType.MoveR:
                {
                    toPlay = "move_r";
                    break;
                }
            case SpriteType.MoveU:
                {
                    toPlay = "move_u";
                    break;
                }
        }
        sprite.Play(toPlay);
    }
}
public enum SpriteType
{
    Idle,
    Dead,
    Move,
    MoveL,
    MoveR,
    MoveU,
    MoveD,
}