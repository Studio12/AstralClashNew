using UnityEngine;
using System.Collections;

public class CharSelectOld : MonoBehaviour {

	public Sprite[,] CharSprite = new Sprite[2, 2];
	public Sprite[] sprites;
	public int selected;
	public int selected2;
	public int max;
	public int max2;
	public bool axisPressed;
	public bool axisPressedLR;
	public Match match;
	private int Players;
	public TextMesh pText;
	public GameObject prevMenu;

	// Use this for initialization
	void Start () {

		CharSprite [0, 0] = sprites[0];
		CharSprite [1, 0] = sprites[1];
		CharSprite [0, 1] = sprites[2];
		CharSprite [1, 1] = sprites[3];
		selected = 0;
		selected2 = 0;
		Players = 1;
	
	}
	
	// Update is called once per frame
	void Update () {

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
		
		if (axisPressedLR == false) {
			if (Input.GetAxis ("MenuLR") > 0) {

				if (selected2 == max2) {
					
					selected2 = 0;
					
				} else {
					selected2++;
				}

				selectionEffect ();
				axisPressedLR = true;
				
			} else if (Input.GetAxis ("MenuLR") < 0) {

				if (selected2 == 0) {
					
					selected2 = max2;
					
				} else {
					selected2--;
				}
				selectionEffect ();
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

	void selectionEffect ()
	{
		print ("Changing sprite...");
		this.GetComponent<SpriteRenderer> ().sprite = CharSprite [selected, selected2];
	}

	void selectOption(){

		/*KEY:

			0-3: Human Players, Taurus-Scorpio-etc.-etc.
			4-7: AI Players, Taurus-Scorpio-etc.-etc.

		 */
		
		if (Players <= match.maxPlayers) {
		
			switch (Players){

			case 1:
				if(match.humans>0){
					if(selected == 0 && selected2 == 0){

						match.p1 = 0;

					}else if(selected == 1 && selected2 == 0){

						match.p1 = 1;

					}else if(selected == 1 && selected2 == 1){

						match.p1 = 3;

					}
				}else{

					if(selected == 0 && selected2 == 0){

						match.p1 = 4;

					}else if(selected == 1 && selected2 == 0){
						
						match.p1 = 5;
						
					}else if(selected == 1 && selected2 == 1){
						
						match.p1 = 7;
							
					}

				}
				break;
			case 2:
				if(match.humans>0){
					if(selected == 0 && selected2 == 0){
						
						match.p2 = 0;
						
					}else if(selected == 1 && selected2 == 0){
						
						match.p2 = 1;
						
					}else if(selected == 1 && selected2 == 1){
						
						match.p2 = 3;
						
					}
				}else{
					
					if(selected == 0 && selected2 == 0){
						
						match.p2 = 4;
						
					}else if(selected == 1 && selected2 == 0){
						
						match.p2 = 5;
						
					}else if(selected == 1 && selected2 == 1){
						
						match.p2 = 7;
						
					}
					
				}
				break;
			case 3:
				if(match.humans>0){
					if(selected == 0 && selected2 == 0){
						
						match.p3 = 0;
						
					}else if(selected == 1 && selected2 == 0){
						
						match.p3 = 1;
						
					}else if(selected == 1 && selected2 == 1){
						
						match.p3 = 3;
						
					}
				}else{
					
					if(selected == 0 && selected2 == 0){
						
						match.p3 = 4;
						
					}else if(selected == 1 && selected2 == 0){
						
						match.p3 = 5;
						
					}else if(selected == 1 && selected2 == 1){
						
						match.p3 = 7;
						
					}
					
				}
				break;
			case 4:
				if(match.humans>0){
					if(selected == 0 && selected2 == 0){
						
						match.p4 = 0;
						
					}else if(selected == 1 && selected2 == 0){
						
						match.p4 = 1;
						
					}else if(selected == 1 && selected2 == 1){
						
						match.p4 = 3;
						
					}
				}else{
					
					if(selected == 0 && selected2 == 0){
						
						match.p4 = 4;
						
					}else if(selected == 1 && selected2 == 0){
						
						match.p4 = 5;
						
					}else if(selected == 1 && selected2 == 1){
						
						match.p4 = 7;
						
					}
					
				}
				break;

			default:
				break;

			}
			Players++;
			match.humans--;
			if(Players != match.maxPlayers+1){
			pText.text = "Player "+Players.ToString();
			}
		
		} if(Players == match.maxPlayers+1) {
			if(match.Level == "cometBugWaves") match.maxPlayers = 2;
			GameObject.Find("GameManager").GetComponent<GameManager>().CreateNewMatch(match);
		
		}
		
	}

	public void SetMatch(Match newMatch){

		match = newMatch;
		
	}

	void BackMenu(){
		
		prevMenu.SetActive (true);
		Camera.current.transform.position = new Vector3 (160, 0, -10);
		Players = 1;
		pText.text = "Player "+Players.ToString();
		this.gameObject.SetActive (false);
		
	}

}
