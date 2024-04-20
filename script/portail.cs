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

    public void _on_area_2d_body_entered(Node body)
    {
        if (body is CharacterBody2D)
        {
            GD.Print("Portail body entered!");
            GetTree().ChangeSceneToFile("res://scene/main_menu.tscn");
        }
    }
    
    
    public override void _Process(double delta)
    {
    }
}


