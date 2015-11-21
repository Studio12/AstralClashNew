using UnityEngine;
using System.Collections;

public class LevelSelect : Menu {

	public Sprite[] LevelSprite;
	public GameObject charMenu;
	
	public void selectOption(string Level){
		match.Level = Level;
		charMenu.GetComponent<CharSelect> ().SetMatch (match);
		Invoke ("SwitchToChars",1.5f);
	}

	public void SwitchToChars()
	{
		SwitchTo (charMenu);
	}

}
