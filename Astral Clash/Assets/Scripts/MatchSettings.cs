using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MatchSettings : Menu {

	public Text[] MenuOptions;
	public RectTransform selector;
	public GameObject levelMenu;

	private int players;
	private int rounds;
	private int fighters;

	// Use this for initialization
	void Awake () {

		ResetMatchSettings ();

	}
	
	// Update is called once per frame
	void Update () {

		if (axisPressedLR == false) {
			if (Input.GetAxis ("MenuLR") < 0) {

				selectionEffect2 (-1);
				axisPressedLR = true;
				
			} else if (Input.GetAxis ("MenuLR") > 0) {

				selectionEffect2 (1);
				axisPressedLR = true;
				
			}
		}
		
		if (Input.GetAxis ("MenuDPad") == 0) {
			
			axisPressed = false;
			
		}

		if (Input.GetAxis ("MenuLR") == 0) {
			
			axisPressedLR = false;
			
		}
		
		if(Input.GetButtonDown("Submit")){
			
			selectOption ();
			
		}

		if(Input.GetButtonDown("Cancel")){
			BackMenu();
		}


	}

	public void SetSelected (int newSelected)
	{
		selected = newSelected;
		selectionEffect ();
	}

	void selectionEffect ()
	{

		selector.position = new Vector2(MenuOptions [selected].transform.position.x + 2.4f, MenuOptions [selected].transform.position.y + 5f);

	}

	void selectionEffect2 (int increment)
	{
		
		/*switch (selected) {
		
		case 0:
			if(match.rounds+increment == 0){

				match.rounds = 99;

			}else if(match.rounds+increment == 100){

				match.rounds = 1;

			}
			else{match.rounds += increment;}
			MenuOptions [0].text = match.rounds.ToString ();
			break;

		case 1:
			if(match.maxPlayers+increment == 1){
				
				match.maxPlayers = 4;
					
			}else if(match.maxPlayers+increment == 5){
				
				match.maxPlayers = 2;
					
			}
			else{match.maxPlayers += increment;}
			if(match.humans>match.maxPlayers){

				match.humans = match.maxPlayers;
				match.AI = match.maxPlayers-match.humans;
				MenuOptions [2].text = match.humans.ToString ();

			}
			MenuOptions [1].text = match.maxPlayers.ToString ();
			break;

		case 2:
			if(match.humans+increment == -1){
				
				match.humans = match.maxPlayers;
				
			}else if(match.humans+increment == match.maxPlayers+1){
				
				match.humans = 0;
				
			}
			else{match.humans += increment;}
			MenuOptions [2].text = match.humans.ToString ();
			match.AI = match.maxPlayers-match.humans;
			break;
		
		default:
			break;
		}*/

		
	}
	
	
	void selectOption(){
		levelMenu.GetComponent<LevelSelect> ().SetMatch(match);
		SwitchTo (levelMenu);
			
	}

	public void ResetMatchSettings(){

		match = new Match();
		match.maxPlayers = 2;
		match.rounds = 1;
		match.humans = 2;
		match.AI = 0;
		match.p1 = -1;
		match.p2 = -1;
		match.p3 = -1;
		match.p4 = -1;

		MenuOptions [0].text = match.rounds.ToString ();
		MenuOptions [1].text = match.maxPlayers.ToString ();
		MenuOptions [2].text = match.humans.ToString ();

	}

	}