using UnityEngine;
using System.Collections;

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
		match.Level = "FireStage SP";
		charMenu.GetComponent<CharSelect> ().SetMatch (match);
		SwitchTo (charMenu);
	}

}
