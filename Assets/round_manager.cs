using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class round_manager : MonoBehaviour {

	public static round_manager instance;
	public bodyPL_controller BP;
	public text_change TC;

	public int round_count = 0;
	public int enemy_per_round;
	public float time_per_spawn;
	public int current_enemies_alive;

	int position_index = 0;

	private bool finish_respawn = false;

	public Transform[] spawn_transforms;

	void OnEnable () 
	{
		instance = this;
		Invoke("beguin_round",5);
	}

	public void beguin_round () 
	{
		StartCoroutine("beguin_spawn");
	}

	IEnumerator beguin_spawn ()
	{
		print("round START");
		enemy_per_round +=5;
		round_count++;
		TC.change(round_count);
		int ammo = BP.reset_ammo();
		UI_manager.instance.health_slide.value += 3;
		UI_manager.instance.change_round(round_count);
		UI_manager.instance.change_ammo(ammo);

		for(int i = 0; i < enemy_per_round; i++)
		{
			
			GameObject obj = asset_manager.current.Get_projetil(asset_manager.projectiles.enemy);
			obj.transform.position = spawn_transforms[position_index].transform.position;
			position_index++;
			obj.SetActive(true);
			current_enemies_alive++;

			if(position_index >= spawn_transforms.Length)
				position_index = 0;

			yield return new WaitForSeconds(time_per_spawn);
		}

		finish_respawn = true;
	}

	public void eliminate_enemy () 
	{
		current_enemies_alive--;

		if(current_enemies_alive == 0 && finish_respawn)
		{
			print("ROUND CLEARED");
			UI_manager.instance.round_cleared();
			Invoke("beguin_round",5);
			finish_respawn = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
