using Godot;

public partial class Grid : Node2D
{
	#region Private Variables
	private int gridSize;
	#endregion

	#region Public Variables
	[Export(PropertyHint.Range, "1,128,1")]
	public int GridSize
	{
		get { return gridSize; }
		set { gridSize = value; }
	}
	public Vector2 GridVector
	{
		get { return new Vector2(gridSize, gridSize); }
	}
	#endregion

	#region References
	#endregion


	private Vector2 SnapToGrid(Vector2 pos)
	{
		return pos.Snapped(new Vector2(gridSize, gridSize));
	}

}
