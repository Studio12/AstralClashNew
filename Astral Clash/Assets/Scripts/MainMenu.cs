using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MainMenu : Menu
{
	public GameObject charMenu;

	public void prepareSinglePlayer ()
	{
		var match = new Match();
		match.maxPlayers = 1;
		match.rounds = 4;
		match.humans = 1;
		match.AI = 1;
		match.p1 = 1;
		match.p2 = -1;
		match.p3 = -1;
		match.p4 = -1;
		match.Level = "SPPlaceholder";
		charMenu.GetComponent<CharSelect> ().SetMatch (match);
		Invoke ("SwitchToChars",1.5f);
	}
	
	public void SwitchToChars()
	{
		SwitchTo (charMenu);
	}


	public void Quit(){

		Application.Quit ();

	}

}
