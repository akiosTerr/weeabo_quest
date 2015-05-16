using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class character_screen : MonoBehaviour {


	public GameObject BP;
	public Animator AC;

	private bool can_press = true;


	void Start () 
	{
		AC = BP.GetComponent<Animator> ();
		//BP.SetActive (false);

	}

	public void iniciate_screen () 
	{
		BP.SetActive (true);
	}


	void Update () 
	{
		BP.transform.Rotate (Vector3.up * 65 * Time.deltaTime);

		if (Input.GetKeyDown (KeyCode.A)  && can_press) 
		{
			can_press = false;
			AC.SetTrigger ("move");
			StartCoroutine ("trigger");
		}

		if (BP.activeInHierarchy == true && Input.GetKeyDown (KeyCode.Return)) 
		{
			start_game ();
		}
	}

	void start_game () 
	{
		Application.LoadLevel (Application.loadedLevel + 1);
	}

	IEnumerator trigger () 
	{
		yield return new WaitForSeconds (0.5f);
		AC.SetTrigger ("move2");
		yield return new WaitForSeconds (0.8f);
		AC.SetTrigger ("move3");
		can_press = true;

		yield return null;
	}

}
