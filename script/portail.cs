using Godot;
using System;

public partial class portail : Sprite2D
{
    
    private CollisionShape2D collision_shape;

    public override void _Ready()
    {
        collision_shape = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");
        GD.Print("Portail ready!");
       
    }

    public void _on_area_2d_body_entered(astra body)
    {
        
    }
   // public void _on_portail_body_entered(CharacterBody2D body)
    //{
        //collision_shape.SetDeferred("disabled", true);
        //GD.Print("Portail body entered!");
        //SceneTransistor sceneTransistor = GetNode<SceneTransistor>("/root/SceneTransistor");
        //sceneTransistor.StartTransitionTo("res://scene/main_menu.tscn");
        //GetTree().ChangeSceneToFile("res://scene/main_menu.tscn");
    //}
    
    
    public override void _Process(double delta)
    {
    }
}


