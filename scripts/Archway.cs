using Godot;
using System;

public partial class Archway : Node3D
{

    private StaticBody3D door;

    private AnimationPlayer waterAnimation;

    private AnimationPlayer archwayAnimation;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        door = GetNode<StaticBody3D>("Door");
        waterAnimation = GetNode<AnimationPlayer>("Water/AnimationPlayer");
        archwayAnimation = GetNode<AnimationPlayer>("archway/AnimationPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void OnWeatherChanged(bool isRaining)
    {
        if (isRaining)
        {
            door.SetCollisionMaskValue(2, false);
            door.Position = new Vector3(2.0f, 4.0f, 0.0f);
            waterAnimation.Play("WaterFall");
            archwayAnimation.Play("DoorOpen");
        }
        else
        {
            door.SetCollisionMaskValue(2, true);
            door.Position = new Vector3(2.0f, 1.0f, 0.0f);
            waterAnimation.PlayBackwards("WaterFall");
            archwayAnimation.PlayBackwards("DoorOpen");
        }
    }
}
