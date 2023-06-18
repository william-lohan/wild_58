using Godot;
using System;
using System.Collections.Generic;

public partial class RainStone : Area3D
{

    private Node3D originalStone;

    private PackedScene brokenScene;

    private Node3D brokenStone;

    private AudioStreamPlayer3D BreakSFX;

    [Signal]
    public delegate void BlockBrokenEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        originalStone = GetNode<Node3D>("rain_stone");
        brokenScene = GD.Load<PackedScene>("res://imported_models/broke_rain_stone.glb");
        BreakSFX = GetNode<AudioStreamPlayer3D>("BreakSFX");

        // BodyEntered += OnBodyEntered; // wired in GUI
	}

    private void AnimateBreaking(Node3D brokenStone)
    {
        AnimationPlayer animationPlayer = brokenStone.GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("KeyAction");
    }

    private float[] GetRandomSoundClip()
    {
        List<float[]> clips = new List<float[]>
        {
            new float[] { 0.0f, 0.81f },
            new float[] { 1.58f, 2.29f },
            new float[] { 2.98f, 3.92f },
            new float[] { 4.53f, 5.42f },
            new float[] { 6.26f, 7.42f },
            new float[] { 8.14f, 8.84f },
        };

        return clips[new Random().Next(0, clips.Count)];
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

            // play sfx
            float[] clip = GetRandomSoundClip();
            BreakSFX.PlayFromTo(clip[0], clip[1]);

            Weather weather = GetTree().GetNodesInGroup("Weather")[0] as Weather;
            weather.MakeRain();
            EmitSignal(SignalName.BlockBroken);
        }
    }

    private void ResetStone(Node3D node)
    {
        originalStone.Visible = true;
        if (brokenStone != null)
            brokenStone.Visible = false;
        SetCollisionMaskValue(2, true);
    }

}
