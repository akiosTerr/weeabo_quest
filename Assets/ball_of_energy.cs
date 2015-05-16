using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class ball_of_energy : MonoBehaviour 
{

	public Rigidbody rb;

	private bool launch_physics;
	public Transform player;
	public ForceMode force_mode;

	public float min_dmg, max_dmg;

	public float velocity = 100;

	void Awake () 
	{
		rb = GetComponent<Rigidbody>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void OnEnable () 
	{
		Invoke("launch",0.5f);
		Invoke("destroy",7);

		min_dmg += 0.01f;
		max_dmg += 0.01f;
	}


	void destroy () 
	{
		launch_physics = false;
		CancelInvoke();
		rb.velocity = Vector3.zero;
		gameObject.SetActive(false);
	}

	void launch () 
	{
		//transform.LookAt(player.position);
		launch_physics = true;
	}

	void FixedUpdate () 
	{

		if(launch_physics)
		{
			transform.LookAt(player.position);
			rb.AddRelativeForce(Vector3.forward*velocity,force_mode);
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Player")
		{
			UI_manager.instance.health_reduction(min_dmg,max_dmg);
		}
		
		destroy();
	}
	
}
