using UnityEngine;
using System.Collections;

public class ShieldAct : MonoBehaviour {


public Fighter fighter;

	
	// Use this for initialization
	void Awake () {

		fighter = transform.parent.GetComponent<Fighter> ();
		this.GetComponent<Animator>().Play("ShieldAnim", -1, 0);


	}
	
	// Update is called once per frame
	void Update () {

<<<<<<< HEAD
		this.GetComponent<SpriteRenderer> ().color = new Color (fighter.shieldHealth / 40, fighter.shieldHealth / 40, fighter.shieldHealth / 40);

//		//This tests damage numbers
//		/*if (Input.GetKeyDown (KeyCode.D)) {
//			currentShieldHP -= 20;
//		}*/
//
//
//		//Put button command for blocking here
//		if (Input.GetKey (KeyCode.B) && sBroken == false) {
//			myShield.SetActive (true);
//
//			//Sets the blocking variable in Fighter script to true....maybe that helps?
//			fighter.blocking = true;
//
//			//Stops the reset health coroutine from running if player presses the block button
//			if(CRrunning == true){
//				StopCoroutine("resetShieldHP");
//				CRrunning = false;
//				print("Forced CoRo to stop!");
//			}
//			
//			
//		}
//		//I would want the shield to reset to max health here, when not using the shield
//		else if(sBroken != true){
//			myShield.SetActive(false);
//
//			//Could be a bad idea but makes sense
//			fighter.blocking = false;
//			
//			if(currentShieldHP < maxShieldHP && CRrunning == false){
//				if(currentShieldHP <= 0){
//					StartCoroutine("shieldBreak");
//				}
//				else{
//					StartCoroutine("resetShieldHP");
//				}
//			}
//		}
=======
		this.GetComponent<SpriteRenderer> ().color = new Color (fighter.shieldhealth / 30, fighter.shieldhealth / 30, fighter.shieldhealth / 30);
		print (fighter.shieldhealth / 30);
>>>>>>> refs/remotes/origin/master
		
	}
	




}
