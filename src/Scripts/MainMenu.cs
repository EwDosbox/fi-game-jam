using Godot;
using System;
using System.Diagnostics;

public partial class MainMenu : Control
{

	#region Private Variables
	#endregion
	#region Public Variables
	#endregion
	#region References
	[Export]
	private PackedScene nextScene;
	#endregion

	public void NextScene()
	{
		if (nextScene != null)
			GetTree().ChangeSceneToPacked(nextScene);
	}

	public void ExitGame()
	{
		GetTree().Quit();
	}
}
