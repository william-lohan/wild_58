using Godot;
using System;

public static class AudioStreamPlayer3DPlayFromTo
{
    public static void PlayFromTo(this AudioStreamPlayer3D player, float from, float to)
    {
        var timer = new Timer{
            OneShot = true,
            WaitTime = to - from,
        };
        player.AddChild(timer);
        timer.Timeout += () => player.Stop();
        timer.Start();
        player.Play(from);
    }
}