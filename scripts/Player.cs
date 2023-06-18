using System;
using Godot;

public partial class Player : CharacterBody3D
{
    [Export]
    private float Speed = 5.0f;
    [Export]
    private float JumpVelocity = 4.5f;

    private int shineCount = 0;

    public int Score {
        get { return shineCount; }
    }

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    private AnimationPlayer animationPlayer;

    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("demon/AnimationPlayer");
        animationPlayer.Play("IdleAction");
    }

    public override void _PhysicsProcess(double delta)
    {
        float adjustedDelta = (float)delta * 60;
        Vector3 velocity = Velocity;
        bool isJumping = false;

        // Add the gravity.
        if (!IsOnFloor())
            velocity.Y -= gravity * (float)delta;

        // Handle Jump.
        if (Input.IsActionJustPressed("Jump") && IsOnFloor())
        {
            velocity.Y = JumpVelocity;
            isJumping = true;
        }

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        float inputDir = Input.GetAxis("MoveLeft", "MoveRight");
        if (inputDir != 0.0f)
        {
            velocity.X = inputDir * Speed * adjustedDelta;
            if (IsOnFloor() && animationPlayer.CurrentAnimation != "WalkAction")
                animationPlayer.Play("WalkAction");
            if (inputDir < 0)
            {
                LookAt(Position + Vector3.Left);
            }
            else
            {
                LookAt(Position + Vector3.Right);
            }
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0.0f, Speed * adjustedDelta);
            if (IsOnFloor() && animationPlayer.CurrentAnimation != "IdleAction")
                animationPlayer.Play("IdleAction");
        }

        if(isJumping && animationPlayer.CurrentAnimation != "JumpAction")
            animationPlayer.Play("JumpAction");

        Velocity = velocity;
        MoveAndSlide();
    }

    public void AddShine()
    {
        shineCount++;
    }

    public void DeathBoundsHit(Node3D node)
    {
        if (node == this)
        {
            GlobalPosition = Vector3.Zero;
        }
    }

    public void EndLevel()
    {
        ProcessMode = ProcessModeEnum.Disabled;
    }
}
