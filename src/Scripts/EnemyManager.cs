using Godot;
using System.Linq;
using System.Threading.Tasks;

public partial class EnemyManager : Node2D
{
	#region Private Variables
	#endregion

	#region Public Variables
	public bool AllEnemiesDead
	{
		get { return enemies.All(f => f.isDead); }
	}
	#endregion

	#region References
	[Export]
	private Enemy[] enemies;
	#endregion

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public async Task MoveAll()
	{
		foreach (Enemy en in enemies)
		{
			await en.SmoothMove();
		}

	}
}
