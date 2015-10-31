using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	public GameObject charMenu;

	public void switchTo (GameObject newMenu)
	{
		newMenu.SetActive (true);
		gameObject.SetActive (false);
	}

	public void prepareSinglePlayer ()
	{
		var match = new Match();
		match.maxPlayers = 2;
		match.rounds = 4;
		match.humans = 1;
		match.AI = 0;
		match.p1 = 1;
		match.p2 = 0;
		match.p3 = -1;
		match.p4 = -1;
		match.Level = "cometBugWaves";
		charMenu.GetComponent<CharSelect> ().SetMatch (match);
		switchTo (charMenu);
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
