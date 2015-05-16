using UnityEngine;
using System.Collections;

public class next_tex : MonoBehaviour {

	public UI_menu instance;
	public Texture[] tex_array;
	public Material MAT;

	private int text_count = 0;

	void Start () 
	{
		instance.change_name (MAT.mainTexture.name);

		for (int i = 0; i < tex_array.Length; i++) 
		{
			if (MAT.mainTexture.name == tex_array [i].name) 
			{
				print (tex_array [i].name);
				print (i);
				text_count = i+1;
				return;
			}
		}
	}

	void nextTex() 
	{

		if (text_count >= tex_array.Length)
			text_count = 0;

		MAT.SetTexture ("_MainTex", tex_array [text_count]);
		instance.change_name (tex_array [text_count].name);
		text_count++;
	}
}
