using Godot;

namespace Devs.project.Autoloads;

public partial class Global : Node
{
    public int score = 0;
    
    public override void _Ready()
    {
        
    }
    

  
  public void AddScore(int amount)
  {
      score += amount;
  }
  
  public void Reset ()
  {
      score = 0;
  }
}   