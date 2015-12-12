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
	private GameObject SPWaves;
	public GameObject CBWaveManager;
	public AudioClip p1Wins;
	public AudioClip p2Wins;
	public AudioClip p3Wins;
	public AudioClip p4Wins;


	// Use this for initialization
	void Start () {
		Players = new List<GameObject>();
		//SPWaves.AddComponent<CBWaveManager> ();
		print ("List created.");
	}
	
	// Update is called once per frame
	void Update () {
	
		if (roundStarted == true) {
			if(Application.loadedLevelName.IndexOf("SP") > -1 && CBWaveManager.activeSelf == false)
			{
				CBWaveManager.SetActive(true);
				maxPlayers = 2;
			}
			for (int p = Players.Count-1; p >= 0; p--) {
				if ((Players[p].GetComponent<Fighter> () && Players[p].GetComponent<Fighter> ().health <= 0 )||( Players[p].GetComponent<CometBug> () && Players[p].GetComponent<CometBug> ().health <= 0)) {
					deadPlayers++;
					Players.Remove(Players[p]);
				}
			}

			//if (Players.Count <= 1) {
			if (deadPlayers >= (maxPlayers - 1)) {
				roundStarted = false;
				print ("Dead players all dead.");
				StartCoroutine ("RoundEnd");
			}
		}


	}

	IEnumerator RoundEnd(){

		if(Players.Count > 0 && Players[0].GetComponentInChildren<Animator> ()) Players[0].GetComponentInChildren<Animator> ().SetTrigger ("Victory");
		yield return new WaitForSeconds (1f);

		if (Application.loadedLevelName.IndexOf ("SP") == -1) {
			if (GameManager.roundNum < GameManager.curMatch.rounds - 1) {
		
				GameManager.roundNum++;
				if (GameOverUI)
					GameOverUI.SetActive (true);
				if (OverText) {
					OverText.text = "Round Over!";
					OverText.gameObject.GetComponent<Renderer> ().sortingOrder = 21;
				}
				if (Players.Count == 0)
					WinnerText.text = "Round is tied!";
				else {
					WinnerText.text = "Winner is " + Players [0].name + "!";
					switch (Players [0].name) {
					
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
				WinnerText.gameObject.GetComponent<Renderer> ().sortingOrder = 21;
				yield return new WaitForSeconds (3.0f);
				GameObject.Find ("GameManager").GetComponent<GameManager> ().StartCoroutine ("ResetRound", GameManager.curMatch);
		
			} else {
		
				if (GameOverUI) GameOverUI.SetActive (true);
				if (Players.Count > 0) {
					switch (Players [0].name) {
					
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
				int bestWinner = MaxValue (GameManager.pWins);
				string winnerName = null;
				switch (bestWinner) {

				case 0:
					winnerName = "Player 1";
					this.GetComponent<AudioSource> ().clip = p1Wins;
					break;
				case 1:
					winnerName = "Player 2";
					this.GetComponent<AudioSource> ().clip = p2Wins;
					break;
				case 2:
					winnerName = "Player 3";
					this.GetComponent<AudioSource> ().clip = p3Wins;
					break;
				case 3:
					winnerName = "Player 4";
					this.GetComponent<AudioSource> ().clip = p4Wins;
					break;
				default:
					break;

				}

				if (OverText) {
					OverText.text = "Match Over!";
					OverText.gameObject.GetComponent<Renderer> ().sortingOrder = 21;
				}
				if (Players.Count > 0) {
					WinnerText.text = "Round winner is " + Players [0].name + "!\nFinal Winner: " + winnerName + "!";
					this.GetComponent<AudioSource> ().Play ();
				} else {
					WinnerText.text = "Round is tied!\nFinal Winner: " + winnerName + "!";
				}
				WinnerText.gameObject.GetComponent<Renderer> ().sortingOrder = 21;
				yield return new WaitForSeconds (3.0f);

				GameManager.ChooseLevel ("MainMenu");
			}
		} else {
			if(Players[0].GetComponentInChildren<Fighter> ())
			{
				if (GameManager.roundNum < GameManager.curMatch.rounds - 1) {
					
					GameManager.roundNum++;
					if (GameOverUI)
						GameOverUI.SetActive (true);
					if (OverText) {
						OverText.text = "Round Over!";
						OverText.gameObject.GetComponent<Renderer> ().sortingOrder = 21;
					}
					if (Players.Count == 0)
						WinnerText.text = "Round is tied!";
					else {
						WinnerText.text = "All bugs destroyed!";
						GameManager.pWins [0]++;
					}
					WinnerText.gameObject.GetComponent<Renderer> ().sortingOrder = 21;
					yield return new WaitForSeconds (3.0f);
					GameObject.Find ("GameManager").GetComponent<GameManager> ().StartCoroutine ("ResetRound", GameManager.curMatch);
					
				} else {
					
					if (GameOverUI) GameOverUI.SetActive (true);
					if (Players.Count > 0) GameManager.pWins [0]++;
					
					if (OverText) {
						OverText.text = "You win!";
						OverText.gameObject.GetComponent<Renderer> ().sortingOrder = 21;
					}
					if (Players.Count > 0) {
						WinnerText.text = "The Comet Bug threat is no more!";
					} else {
						WinnerText.text = "The Comet Bugs have been vanquished,\nbut at a cost!";
					}
					WinnerText.gameObject.GetComponent<Renderer> ().sortingOrder = 21;
					yield return new WaitForSeconds (3.0f);

					switch (Players[0].GetComponent<Fighter>().charType) {
					case "Taurus":
						GameManager.ChooseLevel ("taurusEnding");
						break;
					case "Scorpio":
						GameManager.ChooseLevel ("scorpioEnding");
						break;
					case "Aquarius":
						GameManager.ChooseLevel ("aquaEnding");
						break;
					case "Leo":
						GameManager.ChooseLevel ("leoEnding");
						break;
					default:
						break;
					}
				}
			}
			else StartCoroutine (GameObject.Find ("WaveManager").GetComponent<CBWaveManager> ().GameOver ());
		}


	}


	int MaxValue (int[] wins){
		int max = Mathf.Max (wins);
		int index = System.Array.IndexOf(wins, max);

		return index;
	}

}
