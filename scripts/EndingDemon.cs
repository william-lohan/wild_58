using Godot;
using System;

public partial class EndingDemon : Node3D
{

    private AnimationPlayer animationPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        animationPlayer.AnimationFinished += (StringName animationName) => animationPlayer.Play("JumpAction");
        animationPlayer.Play("JumpAction");
	}
}
