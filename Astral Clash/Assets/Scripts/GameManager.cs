using UnityEngine;
using System.Collections;

//Prototype for the Game Manager class. This script will control things like saving, if we get around to it,
//level control, tracking of variables between stuff like rounds, setting initialization variables, and match settings. 
//Remember to fill me out!

public class GameManager : MonoBehaviour
{

	private GameObject spawn1;
	private GameObject spawn2;
	private GameObject spawn3;
	private GameObject spawn4;
	public GameObject[] characters;
	public GameObject[] healthbars;
	public GameObject[] portraits;
	private static GameObject charRef;
	private static GameObject barRef;
	private static GameObject roundManager;
	public static int[] pWins = new int[4];
	public static Match curMatch;
	public static int roundNum;
	public bool levelLoaded;
	public GameObject p1Obj;
	public GameObject p2Obj;
	public GameObject p3Obj;
	public GameObject p4Obj;
	private static GameObject pObjRef;

	public static GameManager Instance;


	// Use this for initialization
	void Awake ()
	{

		if (Instance) {
		
			DestroyImmediate (gameObject);
		
		} else {
		
			DontDestroyOnLoad (gameObject);
			Instance = this;
		
		}
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetButtonDown ("ForceQuit")) {
			Application.Quit ();
		}

	
	}

	public static void ChooseLevel (string levelName)
	{

		if (levelName == "Quit") {
		
			Application.Quit ();
		
		} else {
			Application.LoadLevel (levelName);
		}

	}

	public void CreateNewMatch (Match match)
	{
		curMatch = match;

		for (int i = 0; i<pWins.Length; i++) {
		
			pWins [i] = 0;
		
		}

		roundNum = 0;

		StartCoroutine("ResetRound",match);

	}

	public void CreateCharacter (int x, int y)
	{
		print ("Character number " + x);
		print ("Player " + y);
		charRef = (GameObject)Instantiate (characters [x], new Vector2 (0, 0), Quaternion.Euler (0, 0, 0));
		if (x < 4) {
			charRef.GetComponent<Fighter> ().playID = y;
		}

		
		switch (y) {

		case 1:
			charRef.GetComponent<Fighter> ().SpawnPoint = spawn1.transform;
			charRef.transform.position = spawn1.transform.position;
			if(charRef.GetComponent<Fighter> ()) {
				charRef.name = "Player 1";
				print ("Player 1 created");
				barRef = (GameObject)Instantiate(healthbars[0], new Vector2(-26f, 17.5f), Quaternion.Euler(0,0,0));
				barRef.name = "HealthBar1";
				pObjRef = (GameObject)Instantiate(p1Obj, new Vector2(0,0), Quaternion.Euler(0,0,0));
			}
			break;

		case 2:
			if(charRef.GetComponent<Fighter> ()) charRef.GetComponent<Fighter> ().SpawnPoint = spawn2.transform;
			charRef.transform.position = spawn2.transform.position;
			if(charRef.GetComponent<Fighter> ()) {
				charRef.name = "Player 2";
				barRef = (GameObject)Instantiate(healthbars[1], new Vector2(-12f, 17.5f), Quaternion.Euler(0,0,0));
				barRef.name = "HealthBar2";
				pObjRef = (GameObject)Instantiate(p2Obj, new Vector2(0,0), Quaternion.Euler(0,0,0));
			}
			break;

		case 3:
			if(charRef.GetComponent<Fighter> ()) charRef.GetComponent<Fighter> ().SpawnPoint = spawn3.transform;
			charRef.transform.position = spawn3.transform.position;
			if(charRef.GetComponent<Fighter> ()) {
				charRef.name = "Player 3";
				barRef = (GameObject)Instantiate(healthbars[2], new Vector2(2f, 17.5f), Quaternion.Euler(0,0,0));
				barRef.name = "HealthBar3";
				pObjRef = (GameObject)Instantiate(p3Obj, new Vector2(0,0), Quaternion.Euler(0,0,0));
			}
			break;

		case 4:
			if(charRef.GetComponent<Fighter> ()) charRef.GetComponent<Fighter> ().SpawnPoint = spawn4.transform;
			charRef.transform.position = spawn4.transform.position;
			if(charRef.GetComponent<Fighter> ()) {
				charRef.name = "Player 4";
				barRef = (GameObject)Instantiate(healthbars[3], new Vector2(16f, 17.5f), Quaternion.Euler(0,0,0));
				barRef.name = "HealthBar4";
				pObjRef = (GameObject)Instantiate(p4Obj, new Vector2(0,0), Quaternion.Euler(0,0,0));
			}
			break;

		default:
			break;


		}

		//charRef.transform.position = charRef.GetComponent<Fighter> ().SpawnPoint.position;
		if (charRef.transform.position.x > 0) {
			if(charRef.GetComponent<Fighter> ()) charRef.GetComponent<Fighter> ().facing = -1;
			charRef.transform.LookAt (charRef.transform.position + new Vector3 (0, 0, -1));
		}

		if (x < 8) {
			GameObject portRef = (GameObject)Instantiate (portraits [x], barRef.transform.position, barRef.transform.rotation);
			portRef.transform.parent = barRef.transform;
			portRef.transform.localPosition = new Vector2 (-0.7864876f, 0.08238244f);
		}

		foreach (HealthBar h in barRef.GetComponentsInChildren<HealthBar>()) {
		
			if(charRef.GetComponent<Fighter> ()) h.character = charRef.GetComponent<Fighter> ();

		}

		if (x < 8) {
			pObjRef.transform.SetParent (charRef.GetComponent<Transform> ());
			if (charRef.GetComponent<Fighter> ().charType == "Scorpio") {
		
				pObjRef.transform.localPosition = new Vector2 (0, 3);
		
			} else {
				pObjRef.transform.localPosition = new Vector2 (0, 4);
			}
		}

		roundManager.GetComponent<Round> ().Players.Add (charRef);
		print ("Adding character to list");
		print (roundManager.GetComponent<Round> ().Players [0].name);
		if (x < 8) {
			barRef.GetComponentInChildren<StarBar> ().character = charRef;
			barRef.GetComponentInChildren<TextMesh> ().text = "Player " + y.ToString () + "\n\t\t\t\t   " + pWins [y - 1].ToString ();
			barRef.GetComponentInChildren<TextMesh> ().gameObject.GetComponent<Renderer> ().sortingOrder = 19;
		}

	}

	public IEnumerator ResetRound(Match match){

		ChooseLevel (match.Level);

		levelLoaded = false;

		while (levelLoaded == false) {
		
			yield return new WaitForEndOfFrame();

		}


		spawn1 = GameObject.Find ("SpawnPoint1");
		spawn2 = GameObject.Find ("SpawnPoint2");
		spawn3 = GameObject.Find ("SpawnPoint3");
		spawn4 = GameObject.Find ("SpawnPoint4");
		roundManager = GameObject.Find ("RoundControls");
		
		roundManager.GetComponent<Round> ().maxPlayers = match.maxPlayers;
		
		if (match.p1 != -1) {
			
			CreateCharacter (match.p1, 1);
			
		}
		if (match.p2 != -1) {
			
			CreateCharacter (match.p2, 2);
			
		}
		if (match.p3 != -1) {
			
			CreateCharacter (match.p3, 3);
			
		}
		if (match.p4 != -1) {
			
			CreateCharacter (match.p4, 4);
			
		}

		roundManager.GetComponent<Round> ().roundStarted = true;

	}

	void OnLevelWasLoaded(){

		levelLoaded = true;

	}

}
