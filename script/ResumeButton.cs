using Devs.project.Autoloads;
using Devs.project.Ressources;
using Godot;

namespace Devs.project.script;

public partial class ResumeButton : Button
{
    public void OnResumePressed()
    {
        PauseManager._OnResume();
    }

    public void OnExitButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scene/map.tscn");
    }
    
}