using Godot;
using System;

public partial class CameraLevelFinal : Camera2D
{
	[Export]
	private Player player;
	public override void _Process(double delta)
	{
		this.GlobalPosition = player.GlobalPosition;
	}
}
