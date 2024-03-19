using Godot;
using System;

public partial class portail : tuto
{

    private void _on_area_2d_body_entered(Node2D body)
    {
        Visible=false;
        var gameScene = GD.Load<PackedScene>(path:"res://scene/main_menu.tscn");
        var gameScenebackNode = gameScene.Instantiate();
        GetTree().Root.AddChild(gameScenebackNode);
        //CurrentScene.QueueFree();
    }


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Visible=false;
        //var gameScene=GD.Load<PackedScene>("res://scene/main_menu.tscn");
        //var gameScenebackNode=gameScene.Instantiate();
        //GetTree().ChangeSceneToFile("res://scene/main_menu.tscn");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}


