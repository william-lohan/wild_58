using Godot;
using System;

public partial class Camera : Camera3D
{

    [Export]
    private Node3D follow;

    [Export]
    private float margin = 2.5f;

    [Export]
    private float speed = 2.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        float distance = GlobalPosition.X - follow.GlobalPosition.X;
        if (Mathf.Abs(distance) > margin)
        {
            Vector3 target = new Vector3(Position.X - distance, Position.Y, Position.Z);
            Position = Position.Lerp(target, (float)delta * speed);
        }
	}
}
