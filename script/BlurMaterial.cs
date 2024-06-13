using Godot;

namespace Devs.project.script;

public partial class BlurMaterial : ShaderMaterial
{
    [Export]
    public float Amount { get; set; }

    public void _Ready()
    {
        Shader = GD.Load<Shader>("res://materials/blur.gdshader");
        SetShaderParameter("amount", Amount);
    }
}