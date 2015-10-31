using UnityEngine;
using System.Collections;

public class LevelSelectOld : MonoBehaviour {

	public Sprite[] LevelSprite;
	public int selected;
	public int max;
	public bool axisPressedLR;
	public GameObject charMenu;
	public Match match;
	public GameObject prevMenu;
	
	// Use this for initialization
	void Start () {


		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (axisPressedLR == false) {
			if (Input.GetAxis ("MenuLR") > 0) {
				
				if (selected == max) {
					
					selected = 0;
					
				} else {
					selected++;
				}
				
				selectionEffect ();
				axisPressedLR = true;
				
			} else if (Input.GetAxis ("MenuLR") < 0) {
				
				if (selected == 0) {
					
					selected = max;
					
				} else {
					selected--;
				}
				
				selectionEffect ();
				axisPressedLR = true;
				
			}
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
	
	void selectionEffect ()
	{

		this.GetComponent<SpriteRenderer> ().sprite = LevelSprite [selected];
		
	}
	
	
	void selectOption(){

		switch (selected) {
		
		case 0:
			match.Level = "FireStage";
			break;
		default:
			break;
		
		}
		charMenu.SetActive (true);
		charMenu.GetComponent<CharSelect> ().SetMatch (match);
		Camera.current.transform.position = new Vector3 (240, 0, -10);
		this.gameObject.SetActive (false);
		
	}

	public void SetMatch(Match newMatch){

		match = newMatch;

	}

	void BackMenu(){
		
		prevMenu.SetActive (true);
		prevMenu.GetComponent<MatchSettings> ().ResetMatchSettings ();
		Camera.current.transform.position = new Vector3 (80, 0, -10);
		this.gameObject.SetActive (false);
		
	}

}
