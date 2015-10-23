using UnityEngine;
using System.Collections;

public class RoundWin : MonoBehaviour {


	//Checks how many deaths have occurred
	private Death pDeaths;

	//For ...
	private int pLeft;



	// Use this for initialization
	void Start () {
		pDeaths = GetComponent<Death> ();
	}
	
	// Update is called once per frame
	void Update () {
		pLeft = pDeaths.playerDeaths;

		//This is where we compare the number of deaths to the number of players-1
		if (pLeft == 1)
			//Insert Application.loadlevel and stuff for whatever the win screen would be...or make a canvas
			return;
	}
}
