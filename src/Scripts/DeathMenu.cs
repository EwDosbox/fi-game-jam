using Godot;
using System;

public partial class DeathMenu : Control
{

	[Export]
	private Game game;
	[Export]
	private PauseMenu pauseMenu;
	[Export]
	private Player player;

	public override void _Process(double delta)
	{
		if (player.IsDead)
		{
			this.Visible = true;
		}
	}

	public void RestartLevel()
	{
		game.ReloadScene();
	}

	public void MainMenu()
	{
		pauseMenu.MainMenu();
	}
}
