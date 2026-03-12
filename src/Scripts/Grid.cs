using Godot;

public static class Grid
{
	#region Private Variables
	private static int gridSize = 64;
	#endregion

	#region Public Variables
	[Export]
	public static int GridSize
	{
		get { return gridSize; }
		set { gridSize = value; }
	}
	public static Vector2I GridVector
	{
		get { return new Vector2I(gridSize / 2, gridSize / 2); }
	}
	#endregion

	#region References
	#endregion


	private static Vector2I SnapToGrid(Vector2I pos)
	{
		return pos.Snapped(new Vector2I(gridSize, gridSize));
	}

}

public partial class GridTile
{
	private Vector2I vector;

	public Vector2I GridLocation
	{
		get { return vector; }
		set { vector = value; }
	}
	public Vector2 WorldLocation
	{
		get { return vector * Grid.GridSize; }
		set { vector = new Vector2I(Mathf.RoundToInt(value.X), Mathf.RoundToInt(value.Y)); }
	}
	public int X => vector.X;
	public int Y => vector.Y;

	public void SnapOnGrid()
	{
		vector.Snapped(Grid.GridVector);
	}

}