using Godot;
using System;

public partial class PauseMenu : Control
{
	#region Private Variables
	#endregion
	#region Public Variables
	#endregion
	#region References
	[Export]
	private PackedScene MainMenuScene;
	#endregion

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("pause"))
		{
			ToggleSetting();
		}
	}

	public void ToggleSetting()
	{
		this.Visible = !this.Visible;
	}

	public void MainMenu()
	{
		if (MainMenuScene != null)
			GetTree().ChangeSceneToPacked(MainMenuScene);

	}
}
