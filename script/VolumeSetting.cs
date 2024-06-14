using System;
using Devs.project.Autoloads;
using Godot;
using Godot.Collections;

namespace Devs.project.script;

[Tool]
public partial class VolumeSetting : VBoxContainer
{
    
    private static Dictionary<int, double> _volumePercentageBusIndex = new Dictionary<int, double>();


    public string _busName = "<Bus Name>";
    
    private Label _busNameLabel;
    private HSlider _volumeSlider;
    private Label _volumePercentageLabel;
    
    [Export]
    public string BusName
    {
        get => _busName;
        set
        {
            _busName = value;
            UpdateBusNameLabel();
        }
    }
    
    [Export]
    public int BusIndex { get; set; }

    public override void _Ready()
    {
        _busNameLabel = GetNode<Label>("BusNameLabel");
        UpdateBusNameLabel();
        
        _volumeSlider = GetNode<HSlider>("VolumeSlider");
        _volumeSlider.ValueChanged += _OnValueChanged;
        
        _volumePercentageLabel = GetNode<Label>("VolumePercentageLabel");
        UpdateVolumePercentageLabel();
        
        if (_volumePercentageBusIndex.ContainsKey(BusIndex))
        {
            _volumeSlider.Value = _volumePercentageBusIndex[BusIndex];
            UserPreferences.Data[BusIndex] = _volumeSlider.Value;
            UserPreferences.Save();
        }
        else
        {
            _volumeSlider.Value = 50.0;
        }
    }

    private void _OnValueChanged(double value)
    {
        UpdateVolumePercentageLabel();
        _volumePercentageBusIndex[BusIndex] = value;
        UserPreferences.Data[BusIndex] = value;
        UserPreferences.Save();

        if (value == 0)
        {
         AudioServer.SetBusMute(BusIndex, true);
         return;
        }

        if (AudioServer.IsBusMute(BusIndex))
        {
            AudioServer.SetBusMute(BusIndex, false);
        }
        
        float decibels = ConvertPercentageToDecibels(value);
        AudioServer.SetBusVolumeDb(BusIndex, decibels);
    }

    private void UpdateBusNameLabel()
    {
        if (_busNameLabel == null)
        {
            return;
        }
        _busNameLabel.Text = _busName;
    }
    
    private void UpdateVolumePercentageLabel()
    {
        double volume = _volumeSlider.Value;
        string volumePercentage = $"{Math.Floor(volume)}";
        _volumePercentageLabel.Text = volumePercentage;
    }
    
    private float ConvertPercentageToDecibels(double percentage)
    {
        float scale = 20.0f;
        float divisor = 50.0f;
        return scale * (float)Math.Log10(percentage / divisor);
    }
}