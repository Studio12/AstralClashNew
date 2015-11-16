using UnityEngine;
using System.Collections;

public class ShieldAct : MonoBehaviour {

	public int maxShieldHP = 100;
	public int currentShieldHP;

	/*Variable that can be changed from inspector....this is the cooldown 
	for broken shield or how long it takes for the broken shield to regenerate
	*/
	public float brokenCooldown;
	
	public GameObject myShield;

	public Fighter fighter;

	//Used to figure out if coroutine is running
	public bool CRrunning;
	public bool sBroken;
	
	// Use this for initialization
	void Start () {
		currentShieldHP = maxShieldHP;
		CRrunning = false;
		sBroken = false;

		//Locate the fighter script on the character
		fighter = GetComponent<Fighter> ();
	}
	
	// Update is called once per frame
	void Update () {

		//This tests damage numbers
		/*if (Input.GetKeyDown (KeyCode.D)) {
			currentShieldHP -= 20;
		}*/


		//Put button command for blocking here
		if (Input.GetKey (KeyCode.B) && sBroken == false) {
			myShield.SetActive (true);

			//Sets the blocking variable in Fighter script to true....maybe that helps?
			fighter.blocking = true;

			//Stops the reset health coroutine from running if player presses the block button
			if(CRrunning == true){
				StopCoroutine("resetShieldHP");
				CRrunning = false;
				print("Forced CoRo to stop!");
			}
			
			
		}
		//I would want the shield to reset to max health here, when not using the shield
		else if(sBroken != true){
			myShield.SetActive(false);

			//Could be a bad idea but makes sense
			fighter.blocking = false;
			
			if(currentShieldHP < maxShieldHP && CRrunning == false){
				if(currentShieldHP <= 0){
					StartCoroutine("shieldBreak");
				}
				else{
					StartCoroutine("resetShieldHP");
				}
			}
		}
		
	}
	
	//Function to reset the shield health
	IEnumerator resetShieldHP() {
		CRrunning = true;
		//Debug
		print ("CoRo is running!");
		
		yield return new WaitForSeconds(4.0f);
		currentShieldHP = maxShieldHP;
		CRrunning = false;
		
		print ("CoRo stopped running.");
		
	}
	
	//For when shield health reaches 0
	IEnumerator shieldBreak(){
		//I would want to make it so the player cannot access the shield
		sBroken = true;
		CRrunning = true;

		yield return new WaitForSeconds (brokenCooldown);
		
		currentShieldHP = maxShieldHP;
	}



}
