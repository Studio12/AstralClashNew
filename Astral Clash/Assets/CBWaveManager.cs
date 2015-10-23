using UnityEngine;
using System.Collections;

public class CBWaveManager : MonoBehaviour {
	public static int wave = 0;
	public Fighter player;
	public GameObject continueMenu;
	public GameObject star;
	public GameObject bug;
	public static int BugCount;
	public bool roundComplete = false;

	// Use this for initialization
	void Start () {
		NewWave ();
	}
	
	// Update is called once per frame
	void Update () {
		//BugCount = FindObjectsOfType (typeof(CometBug)) as CometBug[];
		if (BugCount == 0 && !roundComplete)
			StartCoroutine (WaveComplete ());
		if (player.health == 0 && !roundComplete)
			StartCoroutine (GameOver ());
	}

	void NewWave () {
		wave++;
		if (wave != 4) {
			for (int i = 0; i < wave; i++) {
				Instantiate (bug, transform.position, transform.rotation);
			}
			BugCount = (FindObjectsOfType (typeof(CometBug)) as CometBug[]).Length;
		} else {
			print ("Last wave");
		}
		roundComplete = false;
	}

	IEnumerator WaveComplete () {
		roundComplete = true;

		if (wave < 4) {
			print ("Wave Complete");
			player.stars++;

			yield return new WaitForSeconds (5f);
			NewWave ();
			print ("Round " + wave + ": FIGHT!");
		} else {
			print ("You're winner!");
			yield return new WaitForSeconds (5f);
			Application.LoadLevel ("MainMenu");
		}
	}

	IEnumerator GameOver () {
		roundComplete = true;
		print ("Game Over");
		
		yield return new WaitForSeconds (5f);

		continueMenu.gameObject.SetActive(true);
	}

}
