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
		match.Level = "cometBugWaves";
		charMenu.GetComponent<CharSelect> ().SetMatch (match);
		SwitchTo (charMenu);
	}


	/*void selectOption(){


		switch (MenuOptions [selected].name) {
		
		case "Options":
			RegMenu.transform.position = newPos;
			ControlsMenu.SetActive (true);
			selector.SetActive(false);
			break;
		case "singleplayer":
			GameManager.ChooseLevel (MenuOptions [selected].name);
			break;
		case "multiplayer":
			Camera.main.transform.position = new Vector3(80, 0, -10);
			MatchMenu.SetActive(true);
			this.gameObject.SetActive(false);
			break;
		case "Quit":
			GameManager.ChooseLevel (MenuOptions [selected].name);
			break;
		default:
			break;
		
		}

	}*/

}
