using UnityEngine;
using System.Collections;

public class bodyPL_controller : MonoBehaviour 
{


	public float max_shooting_distance = 200;
	public float max_melee_distance = 10;

	public int melee_dmg = 10;
	public int shooting_dmg = 5;

	public int ranged_ammo = 30;

	public ParticleSystem feather_particle;
	public ParticleSystem muzzleflash_particle;
	public ParticleSystem blood_particles;

	public AudioSource AS;
	public AudioSource AS2;


	void feather() 
	{
		feather_particle.Play ();
	}

	public int reset_ammo () 
	{
		ranged_ammo += 30;
		return ranged_ammo;
	}
	
	void shoot () 
	{
		if(ranged_ammo == 0)
		{
			return;
		}
		ranged_ammo--;
		UI_manager.instance.change_ammo(ranged_ammo);


		muzzleflash_particle.Play();

		if(!AS.isPlaying)
			AS.PlayOneShot(audio_manager.instance.shoot);

		
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (new Vector3(Screen.width/2,Screen.height/2,0));

		if(Physics.Raycast(ray,out hit,max_shooting_distance))
		{
			GameObject hit_obj = hit.collider.gameObject;
			//print(hit_obj.name);
			blood_particles.transform.position = hit.point;


			if (hit_obj.tag == "enemy") 
			{
				blood_particles.Play();
				//print ("HIT ENEMY");

				hit_obj.SendMessage ("hit", shooting_dmg);
			}
		}
	}

	void melee () 
	{

		if(!AS2.isPlaying)
			AS2.PlayOneShot(audio_manager.instance.pillow_melee);

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (new Vector3(Screen.width/2,Screen.height/2,0));


		if (Physics.Raycast (ray, out hit, max_shooting_distance)) 
		{
			GameObject hit_obj = hit.collider.gameObject;
			print (hit_obj.name);

			if (Vector3.Distance (hit_obj.transform.position, transform.position) < max_melee_distance && hit_obj.tag == "enemy") 
			{
				hit_obj.SendMessage ("hit", melee_dmg);
				print ("melee hit");
			}
		}
	}
}
