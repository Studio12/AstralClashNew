using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharSelect : Menu {

	public Sprite[,] CharSprite = new Sprite[2, 2];
	public Sprite[] sprites;
	public int selected2;
	private int Players;
	public Text pText;

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

	void selectionEffect ()
	{
		print ("Changing sprite...");
		this.GetComponent<SpriteRenderer> ().sprite = CharSprite [selected, selected2];
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
			pText.text = "Player "+Players.ToString();
			}
		
		} if(Players == match.maxPlayers+1) {
		
			GameObject.Find("GameManager").GetComponent<GameManager>().CreateNewMatch(match);
		
		}
		
	}

}
