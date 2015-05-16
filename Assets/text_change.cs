using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class text_change : MonoBehaviour 
{
	public Text[] txt;
	public Animator AC;

	public string[] tx_array;

	public void change (int round) 
	{
		if(round == 5)
		{
			for(int i = 0; i < txt.Length; i++)
			{
				txt[i].text = "DEATH";
				txt[i].color = Color.red;
			}
			Invoke("ee",5);
			return;
		}

		for(int i = 0; i < txt.Length; i++)
		{
			txt[i].text = tx_array[Random.Range(0,tx_array.Length)];
		}

		Invoke("ee",2);
	}

	void ee () 
	{
		AC.SetTrigger("trigger");
	}
	
}
