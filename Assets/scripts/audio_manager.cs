using UnityEngine;
using System.Collections;

public class audio_manager : MonoBehaviour {

	public static audio_manager instance;

	public AudioClip shoot;
	public AudioClip pillow_melee;


	void Awake () 
	{
		if (instance == null) {
			instance = this;
		} 
		else if (instance != this) 
		{
			Destroy (this);
		}
	}
}
