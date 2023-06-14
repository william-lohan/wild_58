using Godot;
using System;

public partial class Weather : Node3D
{

    private static Color CLEAR_SKY =  new Color(0.0f, 0.75f, 1.0f, 1.0f);

    private static Color CLOUDY_SKY =  new Color(0.49f, 0.53f, 0.59f, 1.0f);

    [Export]
    private WorldEnvironment worldEnvironment;

    [Export]
    private DirectionalLight3D sunLight;

    [Export]
    private GpuParticles3D rainParticles;

    private bool isRaining = false;

    private ProceduralSkyMaterial skyMaterial;

    [Signal]
    public delegate void WeatherChangedEventHandler(bool isRaining);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        skyMaterial = worldEnvironment.Environment.Sky.SkyMaterial as ProceduralSkyMaterial;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (isRaining)
        {
            sunLight.LightEnergy = Mathf.Lerp(sunLight.LightEnergy, 0.5f, (float)delta);
            rainParticles.Emitting = true;
            skyMaterial.SkyTopColor = skyMaterial.SkyTopColor.Lerp(CLOUDY_SKY, (float)delta);
        }
        else
        {
            sunLight.LightEnergy = Mathf.Lerp(sunLight.LightEnergy, 1.0f, (float)delta);
            rainParticles.Emitting = false;
            skyMaterial.SkyTopColor = skyMaterial.SkyTopColor.Lerp(CLEAR_SKY, (float)delta);
        }
	}

    public void MakeRain()
    {
        isRaining = true;
        EmitSignal(SignalName.WeatherChanged, isRaining);
    }

    public void MakeShine()
    {
        isRaining = false;
        EmitSignal(SignalName.WeatherChanged, isRaining);
    }
}
