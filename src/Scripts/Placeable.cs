using Godot;

public abstract partial class Placeable : Node2D
{
    #region Protected
    protected GridTile tile;
    protected bool isAlive = true;
    #endregion
    #region Properties
    public GridTile Tile
    {
        get { return tile; }
    }

    public bool IsAlive
    {
        get { return isAlive; }
    }
    #endregion
    #region References
    [Export] protected SpriteManager sprite;
    [Export] protected CollisionShape2D collision;
    [Export] protected AudioStreamPlayer2D deathSound;
    #endregion
    #region Methods
    public virtual void Kill()
    {
        deathSound.Play();
        collision.Disabled = true;
        sprite.PlayType(SpriteType.Dead);
    }
    #endregion
}