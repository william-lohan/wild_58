using Godot;
using System;

public partial class PauseMenu : Control
{

    [Export]
    private HSlider musicSlider;

    [Export]
    private HSlider SFXSlider;

    private int musicBusIndex;

    private int SFXBusIndex;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        // get indexes
        musicBusIndex = AudioServer.GetBusIndex("Music");
        SFXBusIndex = AudioServer.GetBusIndex("SFX");
        
        // set initial
        float currentMusVol = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(musicBusIndex));
        musicSlider.Value = currentMusVol;

        float currentSFXVol = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(SFXBusIndex));
        SFXSlider.Value = currentSFXVol;
        
	}

	public void OnMusicSliderChanged(float value)
	{
        AudioServer.SetBusVolumeDb(musicBusIndex, Mathf.LinearToDb(value));
	}

	public void OnSFXSliderChanged(float value)
	{
        AudioServer.SetBusVolumeDb(SFXBusIndex, Mathf.LinearToDb(value));
	}

    public void OnExitButtonPress()
    {
        GetTree().Quit();
    }
}
