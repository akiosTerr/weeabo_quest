using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class asset_manager : MonoBehaviour {

	
	//static instance to accsess the script
	public static asset_manager current;

	//prefabs 
	[System.Serializable]
	public class gm_list
	{
	public GameObject ball_of_energy;
	public GameObject enemy;
	}

	public gm_list GO_class;
	//class instances

	public poole_obj ball_of_energy;
	public poole_obj enemy;

	
	public enum projectiles 
	{
		ball_of_energy,
		enemy
	}
	
	//can the list grow the amount of prefabs if needed? (you can also do code to limit how much can grow)
	public bool Can_Expand;
	
	public class poole_obj
	{
		public List<GameObject> obj_list;
		public GameObject obj_sample;
		
		public poole_obj (GameObject g_obj,int start_amount) 
		{
			obj_sample = g_obj;
			obj_list = new List<GameObject> ();
			
			for (int i = 0; i < start_amount; i++) 
			{
				var temp_obj = (GameObject)Instantiate(g_obj);
				temp_obj.SetActive(false);
				obj_list.Add(temp_obj);
			}
		}
	}
	
	
	
	//inicialize your objects in start (or awake).
	//as first argument put the prefab you want to instantiate.
	//as second argument put how many objects you will instantiate at first to be always ready to use(it can grow if needed if you enable "Can_Expand")
	void Awake () 
	{
		current = this;
		ball_of_energy = new poole_obj (GO_class.ball_of_energy,10);
		enemy = new poole_obj (GO_class.enemy,20);
	}
	
	public GameObject Get_projetil (projectiles projetil_name)
	{
		poole_obj proj_obj = null;
		
		if (projetil_name == projectiles.ball_of_energy)
		{
			proj_obj = ball_of_energy;
		}
		else if (projetil_name == projectiles.enemy)
		{
			proj_obj = enemy;
		}
			
		if (proj_obj == null) 
		{
			Debug.LogError("null object");		
			return null;
		}
		
	
		for (int i = 0; i < proj_obj.obj_list.Count; i++)
		{
			if(proj_obj.obj_list[i] != null  && !proj_obj.obj_list[i].activeInHierarchy)
			{
				return proj_obj.obj_list[i];
			}
		}
		if (Can_Expand) 
		{
			GameObject extra_obj = (GameObject)Instantiate(proj_obj.obj_sample);
			proj_obj.obj_list.Add(extra_obj);
			print(projetil_name+" expanded");
			return extra_obj;
		}

		Debug.LogError(projetil_name+" send null");
		return null;
	}
	
//	public delegate void null_alert();
//	public static event null_alert AMdestroyed;

	void OnDestroy () 
	{
		//AMdestroyed ();
	}
}
