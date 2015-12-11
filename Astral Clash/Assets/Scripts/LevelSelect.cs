using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelect : Menu {

	public GameObject charMenu;
	EventSystem currentSystem = EventSystem.current;

	void OnEnable () {
		currentSystem = EventSystem.current;
		currentSystem.SetSelectedGameObject (firstSelected);
		currentSystem.GetComponent<AudioSource>().PlayOneShot (entered);
		currentSystem.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat ("SFX Volume");
	}
	
	public void selectOption(string Level){
		match.Level = Level;
		charMenu.GetComponent<CharSelect> ().SetMatch (match);
		print ("Received input");
		currentSystem.enabled = false;
		Invoke ("SwitchToChars",1.5f);
	}

	public void SwitchToChars()
	{
		print ("Switching...");
		currentSystem.enabled = true;
		SwitchTo (charMenu);
	}

}
