using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_manager : MonoBehaviour 
{
	public static UI_manager instance;
	public player_input PI;

	public Animator AC;
	public GameObject dead_state;
	public GameObject instructions;
	public Image crossHair;
	public Slider health_slide;
	public Text ammo;
	public Text round;
	public Text out_of_ammo;

	[HideInInspector]
	public bool shield_mode;

	void Awake () 
	{
		AC = GetComponent<Animator> ();
		instance = this;

		crossHair.enabled = false;
		out_of_ammo.enabled = false;
	}

	public void enable_instructions () 
	{
		instructions.SetActive(!instructions.activeInHierarchy);
	}


	public void change_ammo (int new_ammo) 
	{
		ammo.text = new_ammo.ToString();

		if(new_ammo <= 0)
		{
			out_of_ammo.enabled = true;
		}
		else 
		{
			out_of_ammo.enabled = false;
		}
	}

	public void change_round (int new_round)
	{
		round.text = "ROUND "+new_round.ToString();
	}

	public void health_reduction (float min,float max) 
	{
		if(shield_mode == true)
		{
			return;
		}

		float damage = Random.Range (min, max);
		print ("damge = " + damage);

		AC.SetTrigger ("damage");

		health_slide.value -= damage;

		if (health_slide.value <= 0) 
		{
			//death ();
		}
	}
	void death ()
	{
		dead_state.SetActive(true);
		PI.dead_function();
	}

	public void restart_current_scene ()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public void restart_menu () 
	{
		Application.LoadLevel(0);
	}

	public void round_cleared () 
	{
		AC.SetTrigger("round_clear");
	}

	public void activate_crosshair (bool b) 
	{
		crossHair.enabled = b;
	}


	
	// Update is called once per frame
	void Update () 
	{
		
	#if UNITY_EDITOR
		if (Input.GetKeyDown (KeyCode.K)) 
		{
			health_reduction (0.1f,0.3f);
		}
	#endif
	}
}
