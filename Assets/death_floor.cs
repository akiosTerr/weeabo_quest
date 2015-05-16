using UnityEngine;
using System.Collections;

public class death_floor : MonoBehaviour {

	void OnTriggerEnter () 
	{
		UI_manager.instance.health_reduction(10,10);
	}
}
