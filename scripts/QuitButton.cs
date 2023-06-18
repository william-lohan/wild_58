using Godot;
using System;
using System.ComponentModel.DataAnnotations.Schema;

public partial class QuitButton : Button
{
	public void OnPressed()
    {
        GetTree().Quit();
    }
}
