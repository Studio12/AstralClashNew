using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CharSelect : Menu {

	private int Players;
	public Sprite[] indicators;
	public List<GameObject> spawnedIndicators;
	public GameObject currentIndicator;
	public Text pText;

	// Use this for initialization
	void OnEnable () {
		EventSystem.current.SetSelectedGameObject (firstSelected);
		EventSystem.current.GetComponent<AudioSource>().PlayOneShot (entered);
		selected = 0;
		Players = 1;
		EventSystem.current.GetComponent<StandaloneInputModule> ().horizontalAxis = "CharLR" + Players;
		EventSystem.current.GetComponent<StandaloneInputModule> ().verticalAxis = "CharUD" + Players;
		pText.text = "Player "+Players.ToString();
		createIndicator ();
	
	}

	void createIndicator ()
	{
		GameObject newIndicator = new GameObject ("P" + Players);
		newIndicator.transform.parent = GameObject.Find ("Canvas").transform;
		newIndicator.AddComponent<Image> ();
		newIndicator.GetComponent<Image> ().sprite = indicators [Players - 1];
		newIndicator.transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
		Vector2 buttonSize = new Vector2 (EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform> ().rect.width, EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform> ().rect.height);
		newIndicator.GetComponent<RectTransform> ().sizeDelta = buttonSize;
		newIndicator.GetComponent<RectTransform> ().transform.localScale = new Vector3 (1, 1, 1);
		newIndicator.GetComponent<Image> ().raycastTarget = false;
		spawnedIndicators.Add (newIndicator);
		currentIndicator = newIndicator;
	}

	public void updateIndicator ()
	{
		if(currentIndicator) currentIndicator.transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
	}

	public void printMessage (string message)
	{
		print (gameObject.name + " says: " + message);
	}

	public void clearPointers ()
	{
		foreach(GameObject ind in spawnedIndicators) {
			Destroy(ind);
		}
		spawnedIndicators.Clear ();
	}

	public void selectOption(int value){

		/*KEY:

			0-3: Human Players, Taurus-Scorpio-etc.-etc.
			4-7: AI Players, Taurus-Scorpio-etc.-etc.

		 */
		
		if (Players <= match.maxPlayers) {
		
			switch (Players){

			case 1:
				if(match.humans>0){
					match.p1 = value;
				}else{
					match.p1 = value + 4;
				}
				break;
			case 2:
				if(match.humans>0){
					match.p2 = value;
				}else{
					match.p2 = value + 4;
				}
				break;
			case 3:
				if(match.humans>0){
					match.p3 = value;
				}else{
					match.p3 = value + 4;
				}
				break;
			case 4:
				if(match.humans>0){
					match.p4 = value;
				}else{
					match.p4 = value + 4;
				}
				break;

			default:
				break;

			}
			Players++;
			match.humans--;
			if(Players != match.maxPlayers+1){
				createIndicator();
				pText.text = "Player "+Players.ToString();
				EventSystem.current.GetComponent<StandaloneInputModule> ().horizontalAxis = "CharLR" + Players;
				EventSystem.current.GetComponent<StandaloneInputModule> ().verticalAxis = "CharUD" + Players;
			}
		
		} if(Players == match.maxPlayers+1) {
			if(match.Level == "SPPlaceholder")
			{
				//Stuff
				if(match.p1 == 0) match.Level = "taurusOpening";
				else if(match.p1 == 1) match.Level = "scorpioOpening";
				else if(match.p1 == 2) match.Level = "aquaOpening";
				else if(match.p1 == 3) match.Level = "leoOpening";
			}
			BeginMatch();
		
		}
		
	}

	public void BeginMatch()
	{
		GameObject.Find("GameManager").GetComponent<GameManager>().CreateNewMatch(match);
	}

	override public void BackMenu()
	{
		print ("Backing out");
		clearPointers ();
		EventSystem.current.GetComponent<AudioSource>().PlayOneShot (BackOutSound);
		EventSystem.current.GetComponent<StandaloneInputModule> ().horizontalAxis = "MenuLR";
		EventSystem.current.GetComponent<StandaloneInputModule> ().verticalAxis = "MenuDPad";

		SwitchTo (prevMenu);
		
	}

}
