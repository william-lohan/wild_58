using Godot;
using System;

public partial class RotateY : Node3D
{
	float speed = 75.0f;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        RotateY(Mathf.DegToRad((float)delta) * speed);
	}
}
