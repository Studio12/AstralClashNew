using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MainMenu : Menu
{
	public GameObject charMenu;
	public float idleTimer = 0;

	void OnEnable () {
		EventSystem.current.SetSelectedGameObject (firstSelected);
		EventSystem.current.GetComponent<AudioSource>().PlayOneShot (entered);
		EventSystem.current.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat ("SFX Volume");
		idleTimer = 0;
	}

	public void prepareSinglePlayer ()
	{
		var match = new Match();
		match.maxPlayers = 1;
		match.rounds = 4;
		match.humans = 1;
		match.AI = 1;
		match.p1 = 1;
		match.p2 = -1;
		match.p3 = -1;
		match.p4 = -1;
		match.Level = "SPPlaceholder";
		charMenu.GetComponent<CharSelect> ().SetMatch (match);
		Invoke ("SwitchToChars",1.5f);
	}

	void Update () {
		if (Input.GetButtonDown ("Cancel")) {
			BackMenu ();
		}
		if (Input.anyKey || Input.GetAxis(EventSystem.current.GetComponent<StandaloneInputModule>().verticalAxis) != 0)
			idleTimer = 0;
		else
			idleTimer += Time.deltaTime;
		
		if (idleTimer >= 30)
			Application.LoadLevel ("openingSPcutscene");
	}
	
	public void SwitchToChars()
	{
		SwitchTo (charMenu);
	}


	public void Quit(){

		Application.Quit ();

	}

}
