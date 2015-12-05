using UnityEngine;
using System.Collections;

public class LevelSelect : Menu {

	public GameObject charMenu;
	
	public void selectOption(string Level){
		match.Level = Level;
		charMenu.GetComponent<CharSelect> ().SetMatch (match);
		print ("Received input");
		Invoke ("SwitchToChars",1.5f);
	}

	public void SwitchToChars()
	{
		print ("Switching...");
		SwitchTo (charMenu);
	}

}
