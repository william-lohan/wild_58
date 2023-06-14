using Godot;
using System;

public partial class RainStone : Area3D
{

    private Node3D originalStone;

    private PackedScene brokenScene;

    private Node3D brokenStone;

    [Signal]
    public delegate void BlockBrokenEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        originalStone = GetNode<Node3D>("rain_stone");
        brokenScene = GD.Load<PackedScene>("res://imported_models/broke_rain_stone.glb");

        // BodyEntered += OnBodyEntered; // wired in GUI
	}

    private void AnimateBreaking(Node3D brokenStone)
    {
        AnimationPlayer animationPlayer = brokenStone.GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("KeyAction");
    }


    private void OnBodyEntered(Node3D body)
    {
        if (body is CharacterBody3D player)
        {
            if (brokenStone == null)
            {
                // add broken visuals
                brokenStone = brokenScene.Instantiate<Node3D>();
                AddChild(brokenStone);
            }
            AnimateBreaking(brokenStone);

            // remove original
            originalStone.Visible = false;

            // stop colliding
            SetCollisionMaskValue(2, false);

            EmitSignal(SignalName.BlockBroken);
        }
    }

    private void resetStone()
    {
        originalStone.Visible = true;
        brokenStone.Visible = false;
        SetCollisionMaskValue(2, true);
    }

}
