using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class player_input : MonoBehaviour 
{

	public Animator ac;
	public AudioSource AS;
	public FirstPersonController FPSC;


	private bool on_aim = false;
	private bool dead = false;

	public void dead_function () 
	{
		dead = true;
		FPSC.paralyse();
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}


	void Update () 
	{
		if(dead)
		{
			return;
		}

		if (Input.GetKeyDown (KeyCode.I)) 
		{
			UI_manager.instance.enable_instructions();
		}

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Application.Quit ();
		}

		if (Input.GetKeyDown (KeyCode.R)) 
		{
			ac.SetBool ("shield", true);
			UI_manager.instance.shield_mode = true;
		}

		if (Input.GetKeyUp (KeyCode.R)) 
		{
			ac.SetBool ("shield", false);
			UI_manager.instance.shield_mode = false;
		}

		if (Input.GetKeyDown (KeyCode.F)) 
		{
			ac.SetTrigger ("melee");
		}

		if (Input.GetMouseButtonDown(1))
		{
			on_aim = !on_aim;
			UI_manager.instance.activate_crosshair (on_aim);
			ac.SetBool ("AIM",on_aim);
		}

		if (Input.GetMouseButtonDown(0) && on_aim)
		{
			ac.SetTrigger ("shoot 0");
		}

	}
}
