using System;
using Godot;

[Tool]
public partial class CustomShaderImporter : EditorScenePostImport
{

    private void AddCustomShader(MeshInstance3D meshInstance)
    {
        StandardMaterial3D originalMat =  meshInstance.Mesh.SurfaceGetMaterial(0) as StandardMaterial3D;
        ShaderMaterial shaderMaterial = new ShaderMaterial();
        shaderMaterial.Shader = GD.Load<Shader>("res://shaders/custom_shader.gdshader");
        shaderMaterial.SetShaderParameter("albedo_color", originalMat.AlbedoColor);
        shaderMaterial.SetShaderParameter("albedo_texture", originalMat.AlbedoTexture);
        meshInstance.Mesh.SurfaceSetMaterial(0, shaderMaterial);
    }

    private void IterateTree(Node scene)
    {
        foreach (Node child in scene.GetChildren())
        {
            if (child is MeshInstance3D meshInstance)
                AddCustomShader(meshInstance);
            IterateTree(child);
        }
    }

    public virtual GodotObject _PostImport(Node scene)
    {
        IterateTree(scene);
        return scene;
    }

}
