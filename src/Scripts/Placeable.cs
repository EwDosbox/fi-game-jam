using System.Threading.Tasks;
using Godot;

public abstract partial class Placeable : Node2D
{
    #region Protected
    protected GridTile tile;
    protected bool isMoving = false;
    protected bool isAlive = true;
    #endregion
    #region Properties
    public GridTile Tile
    {
        get { return tile; }
    }
    public bool IsMoving
    {
        get { return isMoving; }
    }
    public bool IsAlive
    {
        get { return isAlive; }
    }
    #endregion
    #region References
    [Export] protected AnimatedSprite2D sprite;
    [Export] protected Grid gridScript;
    [Export] protected CollisionShape2D collision;
    [Export] protected AudioStreamPlayer2D deathSound;
    #endregion
    #region Methods

    public virtual void Kill()
    {
        isAlive = false;
        collision.Disabled = true;
        deathSound.Play();

    }

    public virtual async Task SmoothMoveTo(GridTile to)
    {
        return;
    }
    #endregion
}