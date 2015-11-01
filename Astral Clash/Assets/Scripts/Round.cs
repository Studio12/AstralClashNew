using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This will be used to control all round settings eventually, keeping tracks of kills, deaths, etc.


public class Round : MonoBehaviour {

	public List<GameObject> Players;
	private int deadPlayers;
	public int maxPlayers;
	public GameObject GameOverUI;
	public TextMesh OverText;
	public TextMesh WinnerText;
	public bool roundStarted;
	private GameObject tempRemove;
	public GameObject CBWaveManager;


	// Use this for initialization
	void Start () {
		Players = new List<GameObject>();
		print ("List created.");
		tempRemove = null;	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (roundStarted == true) {
			if(Application.loadedLevelName == "cometBugWaves" && CBWaveManager.activeSelf == false)
			{
				CBWaveManager.SetActive(true);
				maxPlayers = 2;
			}
			foreach (GameObject p in Players) {
				
				if ((p.GetComponent<Fighter> () && p.GetComponent<Fighter> ().health <= 0 )||( p.GetComponent<CometBug> () && p.GetComponent<CometBug> ().health <= 0)) {
					deadPlayers++;
					tempRemove = p;
					
				}
				
			}

			Players.Remove(tempRemove);
			tempRemove = null;
			
			if (deadPlayers >= (maxPlayers - 1)) {
				
				roundStarted = false;
				print ("Dead players all dead.");
				StartCoroutine ("RoundEnd", Players[0]);
				
			}
		}


	}

	IEnumerator RoundEnd(GameObject winner){

		winner.GetComponentInChildren<Animator> ().SetTrigger ("Victory");
		yield return new WaitForSeconds (1f);

		if (GameManager.roundNum < GameManager.curMatch.rounds-1) {
		
			GameManager.roundNum++;
			GameOverUI.SetActive (true);
			OverText.text = "Round Over!";
			OverText.gameObject.GetComponent<Renderer>().sortingOrder = 21;
			if(winner == null) WinnerText.text = "Round is tied!";
			else
			{
				WinnerText.text = "Winner is " + winner.name + "!";
				switch (winner.name) {
					
				case "Player 1":
					GameManager.pWins [0]++;
					break;
				case "Player 2":
					GameManager.pWins [1]++;
					break;
				case "Player 3":
					GameManager.pWins [2]++;
					break;
				case "Player 4":
					GameManager.pWins [3]++;
					break;
				}
			}
			WinnerText.gameObject.GetComponent<Renderer>().sortingOrder = 21;
			yield return new WaitForSeconds (3.0f);
			GameObject.Find ("GameManager").GetComponent<GameManager> ().StartCoroutine("ResetRound", GameManager.curMatch);
		
		} else {
		
			GameOverUI.SetActive (true);
			if(winner != null)
			{
				switch (winner.name) {
					
				case "Player 1":
					GameManager.pWins [0]++;
					break;
				case "Player 2":
					GameManager.pWins [1]++;
					break;
				case "Player 3":
					GameManager.pWins [2]++;
					break;
				case "Player 4":
					GameManager.pWins [3]++;
					break;
				default:
					break;
					
				}
			}
			int bestWinner = MaxValue(GameManager.pWins);
			string winnerName = null;
			switch(bestWinner){

			case 0:
				winnerName = "Player 1";
				break;
			case 1:
				winnerName = "Player 2";
				break;
			case 2:
				winnerName = "Player 3";
				break;
			case 3:
				winnerName = "Player 4";
				break;
			default:
				break;

			}

			OverText.text = "Match Over!";
			OverText.gameObject.GetComponent<Renderer>().sortingOrder = 21;
			if(winner != null) WinnerText.text = "Round winner is " + winner.name + "!\nFinal Winner: "+winnerName+"!";
			else WinnerText.text = "Round is tied!\nFinal Winner: "+winnerName+"!";
			WinnerText.gameObject.GetComponent<Renderer>().sortingOrder = 21;
			yield return new WaitForSeconds (3.0f);
			GameManager.ChooseLevel ("MainMenu");
		
		}


	}

	int MaxValue (int[] wins){
		int max = wins[0];
		int i;
		int index = 0;
		for (i = 1; i < wins.Length; i++) {
			if (wins[i] > max) {
				max = wins[i];
			}
		}
		for (i = 1; i < wins.Length; i++) {
			if (wins[i] == max) {
				index = i;
			}
		}

		return index;

	}

}
