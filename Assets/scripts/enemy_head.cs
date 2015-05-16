using UnityEngine;
using System.Collections;

public class enemy_head : MonoBehaviour {

	public enemy_behaviour eb;

	public void hit (int dmg) 
	{
		eb.hit(dmg*2);

		print ("head shot");
	}
}
