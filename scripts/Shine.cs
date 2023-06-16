using Godot;
using System;

public partial class Shine : Area3D
{

    [Signal]
    public delegate void ShineHitEventHandler();

    private AudioStreamPlayer3D SFX;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        GetNode<AnimationPlayer>("shine/AnimationPlayer").Play("RotateShine");
        SFX = GetNode<AudioStreamPlayer3D>("SFX");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    private void OnBodyEntered(Node3D body)
    {
        if (body is Player player)
        {
            player.AddShine();
            SFX.Finished += () => QueueFree(); // remove after sound
            SFX.Play();
            Visible = false; // hide while sound plays
            EmitSignal(SignalName.ShineHit);
        }
    }
}
