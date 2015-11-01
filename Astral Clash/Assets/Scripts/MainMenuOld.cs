using UnityEngine;
using System.Collections;

public class MainMenuOld : MonoBehaviour
{
	
	public GameObject[] MenuOptions;
	public GameObject RegMenu;
	public GameObject ControlsMenu;
	public GameObject selector;
	public Vector2 oldPos;
	public Vector2 newPos;
	public int selected;
	public int max;
	public bool axisPressed;
	public GameObject MatchMenu;
	public GameObject charMenu;
	
	// Use this for initialization
	void Start ()
	{
		
		oldPos = RegMenu.transform.position;
		newPos = new Vector2 (5000, 0);
		selected = 0;
		selectionEffect ();
		axisPressed = false;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (axisPressed == false) {
			if (Input.GetAxis ("MenuDPad") < 0) {
				
				if (selected == max) {
					
					selected = 0;
					
				} else {
					selected++;
				}
				
				selectionEffect ();
				axisPressed = true;
				
			} else if (Input.GetAxis ("MenuDPad") > 0) {
				
				if (selected == 0) {
					
					selected = max;
					
				} else {
					selected--;
				}
				
				selectionEffect ();
				axisPressed = true;
				
			}
		}
		
		if (Input.GetAxis ("MenuDPad") == 0) {
			
			axisPressed = false;
			
		}
		
		if(Input.GetButtonDown("Submit")){
			
			selectOption ();
			
		}
		
		if (ControlsMenu.activeSelf == true && Input.GetButtonDown ("Cancel")) {
			
			ControlsMenu.SetActive(false);
			selector.SetActive(true);
			RegMenu.transform.position = oldPos;
			
		}
		
		
		
	}
	
	void selectionEffect ()
	{
		//if (selected != 2) {
		selector.transform.position = MenuOptions [selected].transform.position;
		//} else {
		
		//selector.transform.position = new Vector2(MenuOptions [selected].transform.position.x, MenuOptions [selected].transform.position.y-1);
		
		//}
	}
	
	
	void selectOption(){
		
		
		switch (MenuOptions [selected].name) {
			
		case "Options":
			RegMenu.transform.position = newPos;
			ControlsMenu.SetActive (true);
			selector.SetActive(false);
			break;
		case "singleplayer":
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
			charMenu.SetActive (true);
			charMenu.GetComponent<CharSelectOld> ().SetMatch (match);
			Camera.main.transform.position = new Vector3 (240, 0, -10);
			this.gameObject.SetActive (false);
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
		
	}
	
}
