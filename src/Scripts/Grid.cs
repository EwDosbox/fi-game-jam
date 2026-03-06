using Godot;
using Microsoft.VisualBasic;
using System;
using System.Linq;
using System.Runtime.InteropServices;

public partial class Grid : Node2D
{
	private Godot.Collections.Array<Node2D> children;

	public override void _Ready()
	{
		children = new Godot.Collections.Array<Node2D>(
			GetChildren().OfType<Node2D>()
		);
	}

	public override void _Process(double delta)
	{
		foreach (Node2D n in children)
		{
			n.Transform = SnapToGrid(n.Transform);
		}
	}

	private Transform2D SnapToGrid(Transform2D pos)
	{
		return pos;

	}

}
