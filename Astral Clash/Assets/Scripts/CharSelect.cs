using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CharSelect : Menu {

	public Sprite[,] CharSprite = new Sprite[2, 2];
	public Sprite[] sprites;
	public int selected2;
	private int Players;
	public Sprite[] indicators;
	public List<GameObject> spawnedIndicators;
	public GameObject currentIndicator;
	public Text pText;

	// Use this for initialization
	void OnEnable () {
		EventSystem.current.SetSelectedGameObject (firstSelected);
		CharSprite [0, 0] = sprites[0];
		CharSprite [1, 0] = sprites[1];
		CharSprite [0, 1] = sprites[2];
		CharSprite [1, 1] = sprites[3];
		selected = 0;
		selected2 = 0;
		Players = 1;
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
		print (EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().rect );
		Vector2 buttonSize = new Vector2 (EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform> ().rect.width, EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform> ().rect.height);
		newIndicator.GetComponent<RectTransform> ().sizeDelta = buttonSize;
		newIndicator.GetComponent<RectTransform> ().transform.localScale = new Vector3 (1, 1, 1);
		spawnedIndicators.Add (newIndicator);
		currentIndicator = newIndicator;
	}

	void selectionEffect ()
	{
		print ("Changing sprite...");
		this.GetComponent<SpriteRenderer> ().sprite = CharSprite [selected, selected2];
	}

	public void updateIndicator ()
	{
		currentIndicator.transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
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
			}
		
		} if(Players == match.maxPlayers+1) {
		
			GameObject.Find("GameManager").GetComponent<GameManager>().CreateNewMatch(match);
		
		}
		
	}

	override public void BackMenu()
	{
		print ("Backing out");
		clearPointers ();
		EventSystem.current.GetComponent<AudioSource>().PlayOneShot (BackOutSound);
		SwitchTo (prevMenu);
		
	}

}
