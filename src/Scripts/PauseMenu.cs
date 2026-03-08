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
	private Game game;
	[Export]
	private Player player;
	#endregion

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("pause") && !player.IsDead)
		{
			ToggleSetting();
			game.ToggleControl();
		}
	}

	public void ToggleSetting()
	{
		this.Visible = !this.Visible;
	}

	public void MainMenu()
	{
		GetTree().ChangeSceneToFile("res://Menus/MainMenu.tscn");
	}
}
