using System.Collections.Generic;

namespace Devs.project.script;

public class Player : Entity
{
	public Dictionary<int, Inventory> Inventory { get; set; }
	public int Argent { get; set; }
	public Player(float vie, float attaque, float defense, float speed, float jumpVelocity, Dictionary<int, Inventory> inventory, int argent) : base (vie, attaque, defense, speed, jumpVelocity)
	{
		Argent = argent;
		Inventory = inventory;
	}
}
