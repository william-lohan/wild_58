using Godot;
using System;

public partial class Shrine : Area3D
{

    const float speed = 2.5f;

    private PackedScene shineScene;

    private Node3D shineInstance;

    private int count;

    private Vector3 startPos = Vector3.Zero;

    private Vector3 endPos;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        shineScene = GD.Load<PackedScene>("res://imported_models/shine.glb");
        endPos = GlobalPosition + Vector3.Up * 4;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (count > 0)
        {
            if (shineInstance == null)
            {
                shineInstance = shineScene.Instantiate<Node3D>();
                AddChild(shineInstance);
                shineInstance.GlobalPosition = startPos;
            }
            shineInstance.GlobalPosition = shineInstance.GlobalPosition.Lerp(endPos, (float)delta * speed);
            if (shineInstance.GlobalPosition.DistanceTo(endPos) < 0.2)
            {
                shineInstance.GlobalPosition = startPos;
                count--;
            }
            if (count < 1)
            {
                EndLevel();
            }
        }
	}

    private void OnBodyEntered(Node3D body)
    {
        if (body is Player player)
        {
            player.EndLevel();
            
            count = player.Score;
            startPos = player.GlobalPosition;
        }
    }

    private void EndLevel()
    {
        SceneManager sceneManager = GetTree().GetNodesInGroup("SceneManager")[0] as SceneManager;
        sceneManager.NextScene();
    }
}
