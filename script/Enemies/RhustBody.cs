using Godot;

namespace Devs.project.script.Enemies;

public partial class RhustBody : CharacterBody2D
{
    //------------------------------- V a r i a b l e s ------------------------------------------//
	// Get Node2D Parent
	[Export] public Node2D Character;
	public Vector2 baseposition;
	
	// Rhust Variables
	public Entity Enemy = new Entity(600, 250, 2, 120, -350);
	private AnimatedSprite2D _animatedSprite;
	
	
	
	// Player Variables
	
	
	// Shoot Variables
	
	
	//Sound
	 
	
	//--------------------------------------------------------------------------------------------//
	
	
	//----------------------------------GODOT FUNCTIONS-------------------------------------------//
	public override void _Ready()
	{
		//------ Initialisation -------//
		_animatedSprite = GetNode<AnimatedSprite2D>("Rhust");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		_animatedSprite.Play("run");
	}
	//--------------------------------------------------------------------------------------//
	
	
	

	//--------------------------------- HP SYSTEM -----------------------------------------//
	

	//---------------------------------------------------------------//
    
}