using Godot;
using System;

public partial class SoundEffects : Node2D
{
	[Export]
	private AudioStreamPlayer2D playerMove;
	[Export]
	private AudioStreamPlayer2D playerDead;
	[Export]
	private AudioStreamPlayer2D EnemyDead;

	public void PlayPlayerMove()
	{
		playerMove.Play();
	}
	public void PlayPlayerDead()
	{
		playerDead.Play();
	}
	public void PlayEnemyDead()
	{
		EnemyDead.Play();
	}
}
