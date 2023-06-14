using Godot;
using System;

public partial class Shine : Area3D
{

    [Signal]
    public delegate void ShineHitEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        GetNode<AnimationPlayer>("shine/AnimationPlayer").Play("RotateShine");
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
            EmitSignal(SignalName.ShineHit);
            QueueFree();
        }
    }
}
