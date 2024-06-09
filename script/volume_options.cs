using Godot;

namespace Devs.project.script;

public partial class volume_options : HSlider
{

    [Export]
    public string busName;

    private int busIndex;

    public override void _Ready()
    {
        busIndex = AudioServer.GetBusIndex(busName);
        //ValueChanged += _OnValueChanged;
        Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(busIndex));
    }

    private void _OnValueChanged(float value)
    {
        if (value == 0)
        {
            AudioServer.SetBusMute(busIndex, true);
            return;
        }

        if (AudioServer.IsBusMute(busIndex))
        {
            AudioServer.SetBusMute(busIndex, false);
        }
        
        float dbValue = Mathf.LinearToDb(value);
        AudioServer.SetBusVolumeDb(busIndex, dbValue);
    }
}