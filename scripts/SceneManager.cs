using Godot;
using System;

public partial class SceneManager : Node
{

    private static int sceneIndex = 0;

    private static string[] sceneArray = new string[]
    {
        "res://scenes/shore.tscn",
        "res://scenes/jungle.tscn",
        "res://scenes/temple.tscn",
        "res://scenes/end.tscn",
    };

    private AnimationPlayer animationPlayer;

    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public override void _Input(InputEvent inputEvent)
    {
        SceneTree tree = GetTree();
        Control pauseMenu = GetNode<Control>("PauseMenu");
        if (inputEvent.IsActionPressed("Pause"))
        {
            if (tree.Paused)
            {
                pauseMenu.Hide();
                tree.Paused = false;
            }
            else
            {
                pauseMenu.Show();
                tree.Paused = true;
            }
        }
    }


    public void NextScene()
    {
        sceneIndex++;

        animationPlayer.AnimationFinished += (animationName) => {
            if (animationName == "transition_fade_in")
            {
                GetTree().ChangeSceneToFile(sceneArray[sceneIndex]);
                animationPlayer.Play("transition_fade_out");
            }
        };
        animationPlayer.Play("transition_fade_in");
    }
}
