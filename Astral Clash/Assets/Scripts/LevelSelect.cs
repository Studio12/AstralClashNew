using UnityEngine;
using System.Collections;

public class LevelSelect : Menu {

	public Sprite[] LevelSprite;
	public GameObject charMenu;
	
	public void selectOption(string Level){
		match.Level = Level;
		charMenu.GetComponent<CharSelect> ().SetMatch (match);
		SwitchTo (charMenu);
	}



}
