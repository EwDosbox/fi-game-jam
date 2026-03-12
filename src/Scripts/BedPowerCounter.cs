using Godot;
using System;

public partial class BedPowerCounter : Camera2D
{
	[Export]
	private Player player;
	[Export]
	private Label number;
	public override void _Process(double delta)
	{
		number.Text = player.BedPower.ToString();
	}
}
