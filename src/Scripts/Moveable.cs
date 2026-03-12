using System.Threading.Tasks;
using Godot;

public abstract partial class Moveable : Placeable
{
    #region Protected
    protected bool isMoving = false;
    protected Vector2I direction = Vector2I.Zero;
    protected int step = 1;
    #endregion
    #region Properties
    public bool IsMoving
    {
        get { return isMoving; }
    }
    public Vector2I Direction
    {
        get { return direction; }
        set { direction = value; }
    }
    public int Step
    {
        get { return step; }
    }
    #endregion
    #region References
    [Export] protected AudioStreamPlayer2D MoveSound;
    #endregion
    #region Methods
    public override void Kill()
    {
        base.Kill();
        isAlive = false;
    }

    public virtual async Task SmoothMove()
    {
        MoveSound.Play();
        return;
    }
    #endregion

}