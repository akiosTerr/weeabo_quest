using UnityEngine;
using System.Collections;

public class enemy_behaviour : MonoBehaviour {


	private int health_enemy = 10;
	private float distance;
	private bool attack_state;
	private bool dead_state;
	public Animator AC;
	public Transform player;
	public GameObject[] col_array;

	public Transform ball_position;
	public NavMeshAgent NMA;

	private bool skip = true;

	public float min_dmg = 0.1f,max_dmg = 0.2f;

	public enum enemy_type
	{
		melee,
		ranged
	}

	public enemy_type enemy_mode;


	public float distance_to_follow;
	public float distance_to_attack;
	public float distance_to_ranged;

	void Awake () 
	{
		player = GameObject.FindGameObjectWithTag ("follow").transform;
		AC = GetComponent<Animator>();
	}

	void OnEnable () 
	{
		if(skip)
		{
			skip = false;
			return;
		} 
		health_enemy = 10;
		health_enemy = health_enemy*round_manager.instance.round_count/2;
		print (health_enemy);

		int random_ = Random.Range(1,3);
		print("rand "+random_);

		if(random_ == 1 && round_manager.instance.round_count > 1)
		{
			enemy_mode = enemy_type.ranged;
		}
		else
		{
			enemy_mode = enemy_type.melee;
		} 
			
		for(int i = 0; i < col_array.Length; i++)
		{
			col_array[i].GetComponent<Collider>().enabled = true;
		}
	
		dead_state = false;
		CancelInvoke();
	}
	
	// Update is called once per frame
	void Update () 
	{
		distance = Vector3.Distance (transform.position, player.position);

		if (distance < distance_to_attack) 
		{
			attack ();
		}

		else if (distance < distance_to_ranged && enemy_mode == enemy_type.ranged)
		{
			ranged ();
		}

		else if (distance < distance_to_follow && !dead_state) 
		{
			follow();
		}

		else if (distance > distance_to_follow && !dead_state)
		{
			idle ();
		}

		else
		{
			//NMA.ResetPath();	
		}
	}

	void ranged () 
	{
		AC.SetTrigger("ranged");
		NMA.ResetPath();

		rotate_to();
	}

	void Launch () 
	{
		GameObject obj = asset_manager.current.Get_projetil(asset_manager.projectiles.ball_of_energy);

		obj.transform.position = ball_position.position;
		obj.SetActive(true);
	}

	void rotate_to () 
	{
		Vector3 relativePos = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;
	}

	void idle () 
	{
		AC.SetTrigger("idle");
		NMA.ResetPath();
	}

	void follow () 
	{
		NMA.SetDestination (player.position);
		AC.SetTrigger("walk");
	}

	void attack () 
	{
		rotate_to();
		AC.SetTrigger("melee");
	}

	public void hit (int dmg) 
	{
		health_enemy -= dmg;
		if (health_enemy <= 0) 
		{
			death ();
		}
	}


	void death () 
	{
		for(int i = 0; i < col_array.Length; i++)
		{
			col_array[i].GetComponent<Collider>().enabled = false;
		}

		round_manager.instance.eliminate_enemy();
		print(round_manager.instance.current_enemies_alive);
		dead_state = true;
		AC.SetTrigger("death");
		NMA.ResetPath();
		Invoke("destroy",2);
	}

	void melee () 
	{
		UI_manager.instance.health_reduction(min_dmg,max_dmg);
	}

	void destroy () 
	{
		gameObject.SetActive(false);
	}
}
