using Godot;
using System;

public partial class portail : Sprite2D
{
    
    private CollisionShape2D collision_shape;

    public override void _Ready()
    {
        collision_shape = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");
       
    }

    public void _on_area_2d_body_entered(Node body)
    {
        if (body is astra)
        {
            GetTree().ChangeSceneToFile("res://scene/map.tscn");
        }
    }
    
    
    public override void _Process(double delta)
    {
    }
}


