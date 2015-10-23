using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	//This variable is assigned to the PlayerController script on the player
	private Fighter player;
	//This is the health used for this specific script	
	private float currentHealth;
	
	//Keeps track of deaths for the round win script
	public int playerDeaths;
	
	void Start () {
		//Self-explanatory...assigning the player variable to the actual script
		player = GetComponent<Fighter> ();
		playerDeaths = 0;
	}
	
	// Update is called once per frame
	void Update () {
		/*This would assign the health variable here to whatever the current health
		variable of the player is*/
		currentHealth = player.health;
		
		if (currentHealth <= 0f) {
			playerDeaths++;
			this.gameObject.SetActive(false);
		}
		
		//*****Reminder to move this to the regular player controller script
		//***This is for round winning system
		//if (playerDeaths == something)
		//	;
	}
}
