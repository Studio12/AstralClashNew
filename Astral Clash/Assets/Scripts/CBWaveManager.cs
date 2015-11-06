﻿using UnityEngine;
using System.Collections;

public class CBWaveManager : MonoBehaviour {
	public static int wave = 0;
	public Fighter player;
	public GameObject continueMenu;
	public GameObject star;
	public GameObject bug;
	public static int BugCount;
	public bool roundComplete = false;
	public GameObject[] enemyFighters;
	public Round roundManager;

	// Use this for initialization
	void Start () {
		player = roundManager.Players [0].GetComponent<Fighter> ();
		roundManager.maxPlayers = 2;
		NewWave ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player && player.health == 0 && roundManager.roundStarted)
			StartCoroutine (GameOver ());
		if (GameManager.roundNum == 3 && roundManager.Players.Count == 1 && player.health > 0 && roundManager.maxPlayers == 3)
			roundManager.maxPlayers = 2;
	}

	void NewWave () {
		//wave++;
		print (GameManager.roundNum);
		if ((GameManager.roundNum + 1) != 4) {
			for (int i = 0; i < (GameManager.roundNum + 1); i++) {
				GameObject spawnedBug = (GameObject)Instantiate (bug, GameObject.Find ("SpawnPoint2").transform.position, GameObject.Find ("SpawnPoint2").transform.rotation);
				roundManager.Players.Add (spawnedBug);
			}
			BugCount = (FindObjectsOfType (typeof(CometBug)) as CometBug[]).Length;
			roundManager.maxPlayers = BugCount + 1;
		} else {
			print ("Last wave");
			/*GameObject finalBoss = enemyFighters[Random.Range (0,enemyFighters.Length)];
			while(finalBoss.GetComponent<Fighter>().charType == player.charType) finalBoss = enemyFighters[Random.Range (0,enemyFighters.Length)];
			finalBoss.GetComponent<Fighter>().health = 15;
			GameObject spawnedBoss = (GameObject)Instantiate (finalBoss, GameObject.Find ("SpawnPoint2").transform.position, GameObject.Find ("SpawnPoint2").transform.rotation);
			roundManager.Players.Add (spawnedBoss);*/
			int bossID = Random.Range (5,9);
			GameObject.Find ("GameManager").GetComponent<GameManager>().CreateCharacter (bossID, 2);
			//Bit of a hack so that the player's defeat at the hands of the boss won't trigger the "round over" message
			roundManager.maxPlayers = 3;
			BugCount = 1;
		}
		roundComplete = false;
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

	IEnumerator GameOver () {
		roundComplete = true;
		print ("Game Over");
		
		yield return new WaitForSeconds (5f);

		continueMenu.gameObject.SetActive(true);
	}

}
