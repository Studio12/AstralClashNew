using UnityEngine;
using System.Collections;

public class CBWaveManager : MonoBehaviour {
	public static int wave = 0;
	public Fighter player;
	public GameObject star;
	public GameObject bug;
	public static int BugCount;

	// Use this for initialization
	void Start () {
		player = GetComponent<Round>().Players [0].GetComponent<Fighter> ();
		GameObject.Find ("HealthBar1").GetComponentInChildren<TextMesh> ().text = GetComponent<Round>().Players [0].GetComponent<Fighter>().charType;
		GetComponent<Round>().maxPlayers = 2;
		player.stars = GameManager.roundNum;
		NewWave ();
	}

	// Update is called once per frame
	void Update () {
		if (player && player.health == 0 && GetComponent<Round>().roundStarted)
			StartCoroutine (GameOver ());
		if (GameManager.roundNum == 3 && GetComponent<Round>().Players.Count == 1 && player.health > 0 && GetComponent<Round>().maxPlayers == 3)
			GetComponent<Round>().maxPlayers = 2;
	}

	void NewWave () {
		/*if ((GameManager.roundNum + 1) != 4) {
			for (int i = 0; i < (GameManager.roundNum + 1); i++) {
				GameObject spawnedBug = (GameObject)Instantiate (bug, GameObject.Find ("SpawnPoint2").transform.position, GameObject.Find ("SpawnPoint2").transform.rotation);
				GetComponent<Round>().Players.Add (spawnedBug);
			}
		} else {
			print ("Last wave");
			GameManager.curMatch.p2 = Random.Range (4,6);
			while((GameManager.curMatch.p2 - 4) == GameManager.curMatch.p1) GameManager.curMatch.p2 = Random.Range (4,8);
			GameObject.Find ("GameManager").GetComponent<GameManager>().CreateCharacter (GameManager.curMatch.p2, 2);
			GameObject.Find ("HealthBar2").GetComponentInChildren<TextMesh> ().text = GetComponent<Round>().Players [1].GetComponent<Fighter>().charType;
			GameObject.Destroy (GetComponent<Round>().Players [1].transform.Find ("P2(Clone)").gameObject);
			//Bit of a hack so that the player's defeat at the hands of the boss won't trigger the "round over" message
			GetComponent<Round>().maxPlayers = 3;
		}*/
		for (int i = 0; i < (GameManager.roundNum + 1); i++) {
			GameObject.Find ("GameManager").GetComponent<GameManager>().CreateCharacter (8, i + 2);
		}
		GetComponent<Round>().maxPlayers = FindObjectsOfType (typeof(Actor)).Length;
	}

	public void Restart () {
		var match = new Match();
		match.maxPlayers = 1;
		match.rounds = 4;
		match.humans = 1;
		match.AI = 1;
		match.p1 = 1;
		match.p2 = -1;
		match.p3 = -1;
		match.p4 = -1;
		match.Level = Application.loadedLevelName;
		GameObject.Find("GameManager").GetComponent<GameManager>().CreateNewMatch(match);
	}

	public IEnumerator GameOver () {
		yield return new WaitForSeconds (1f);
		GetComponent<Round>().GameOverUI.SetActive (true);

		GetComponent<Round>().OverText.text = "You failed!";
		GetComponent<Round>().OverText.gameObject.GetComponent<Renderer> ().sortingOrder = 21;
		
		GetComponent<Round>().WinnerText.text = "The Comet Bugs have overpowered you!";
		GetComponent<Round>().WinnerText.gameObject.GetComponent<Renderer> ().sortingOrder = 21;
		yield return new WaitForSeconds (3.0f);
		GameManager.ChooseLevel ("MainMenu");
	}

}
