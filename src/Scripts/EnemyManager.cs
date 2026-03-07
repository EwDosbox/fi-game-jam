using Godot;
using System.Linq;
using System.Threading.Tasks;

public partial class EnemyManager : Node2D
{
	#region Private Variables
	#endregion

	#region Public Variables
	public bool AllEnemiesDead => enemies.Where(e => IsInstanceValid(e)).All(e => e.isDead);
	#endregion

	#region References
	[Export]
	private Enemy[] enemies;
	#endregion

	public async Task MoveAll()
	{
		foreach (Enemy en in enemies)
		{
			await en.SmoothMove();
		}
	}
}
