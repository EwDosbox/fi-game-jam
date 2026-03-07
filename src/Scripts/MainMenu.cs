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
	#endregion
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public void NextScene()
	{
		GetTree().ChangeSceneToFile("res://Menus/Level_1.tscn");
	}
}
