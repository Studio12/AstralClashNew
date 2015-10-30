using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AICentral : MonoBehaviour {

	private Round round;
	private AIPathfind pathfind;
	private GameObject target;
	private bool chase;
	private bool combat;

	// Use this for initialization
	void Start () {

		round = GameObject.Find ("RoundControls").GetComponent<Round> ();
		pathfind = GetComponent<AIPathfind> ();


		Invoke ("StartAI", 1);
	
	}
	
	// Update is called once per frame
	void Update () {




	}

	void StartAI(){
	
		StartCoroutine ("ModUpdate");
	
	}

	IEnumerator ModUpdate(){
	
		while (round.Players.Count > 1) {

			if (target == null && round.Players.Count > 1) {
			
				print ("TARGET NULL, ACQUIRE NEW");
				target = FindNewTarget ();
				print (target.name);
			
				if (this.GetComponent<Fighter> ().curPlatform != target.GetComponent<Fighter> ().curPlatform) {
				
					chase = false;
					combat = true;
				
				} else {
				
					chase = true;
					combat = false;
				
				}
			
			}
		
			if (this.GetComponent<Fighter> ().curPlatform != target.GetComponent<Fighter> ().curPlatform && chase == false) {
				print ("Finding target...");
				StartCoroutine ("FindTarget");
				chase = true;
				combat = false;
			} else if (this.GetComponent<Fighter> ().curPlatform == target.GetComponent<Fighter> ().curPlatform && combat == false) {
				print ("Entering combat...");
				StartCoroutine ("Combat");
				combat = true;
				chase = false;
			
			}

			yield return new WaitForFixedUpdate ();

		}
	
	}

	IEnumerator FindTarget(){

			GameObject oldPlat;
			pathfind.BeginNewPath (target);
			oldPlat = target.GetComponent<Fighter> ().curPlatform;

			while (this.GetComponent<Fighter> ().curPlatform != target.GetComponent<Fighter> ().curPlatform) {

				if (target.GetComponent<Fighter> ().curPlatform != oldPlat) {

					ReconfigurePath ();
					break;

				}

				oldPlat = target.GetComponent<Fighter> ().curPlatform;

				yield return new WaitForFixedUpdate ();

			}

	}

	void ReconfigurePath(){

		StartCoroutine ("FindTarget");

	}

	IEnumerator Combat(){

		yield return new WaitForSeconds(.1f);

	}

	GameObject FindNewTarget(){

		GameObject newTarget = round.Players [Random.Range (0, round.Players.Count)];
		while (newTarget == this.gameObject) {
		
			newTarget = round.Players [Random.Range (0, round.Players.Count)];
		
		}
		return newTarget;

	}

}
