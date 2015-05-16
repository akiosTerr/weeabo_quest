using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_menu : MonoBehaviour {

	public character_screen CS;

	public GameObject panel;
	public GameObject buttons;
	public GameObject credits_tex;
	public GameObject panel_name;
	public Text _name;
	public string current_name;


	void Awake () 
	{
		credits_tex.SetActive (false);
		panel_name.SetActive (false);
	}

	public void play_event () 
	{
		_name.text = current_name;
		panel.SetActive (false);
		panel_name.SetActive (true);
		CS.iniciate_screen ();
	}

	public void change_name (string current_name) 
	{
		_name.text = current_name;
	}

	public void credits_event ()
	{
		buttons.SetActive (!buttons.activeInHierarchy);
		credits_tex.SetActive (!credits_tex.activeInHierarchy);
	}
}
