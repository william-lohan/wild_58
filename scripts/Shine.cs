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
            var _shine = this;
            SFX.Finished += () => _shine.QueueFree(); // remove after sound
            SFX.PlayFromTo(0.06f, 1.18f);
            Visible = false; // hide while sound plays

            // limitation of PlayFromTo won't trigger Finished, so we'll make our own timer
            var timer = new Timer{
                OneShot = true,
                WaitTime = 1.12f,
            };
            player.AddChild(timer);
            timer.Timeout += () => _shine.QueueFree(); // for real this time
            timer.Start();

            Weather weather = GetTree().GetNodesInGroup("Weather")[0] as Weather;
            weather.MakeShine();
            EmitSignal(SignalName.ShineHit);
        }
    }
}
