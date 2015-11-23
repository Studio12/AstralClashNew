using UnityEngine;
using System.Collections;

public class AquaSpecial : MonoBehaviour {

	public GameObject activator;
	public float life;
	public float speed;
	public float damage;
	public float stop;

	// Use this for initialization
	void Start () {

		StartCoroutine ("Activate");
	
	}
	
	// Update is called once per frame
	void Update () {

		life -= Time.deltaTime;

		if (life <= 0) {
		
			Destroy(this.gameObject);
		
		}
	
	}

	IEnumerator Activate(){

		switch (Application.loadedLevelName) {
		
		case "FireStage":
			stop = 2;
			break;
		case "FireStage SP":
			stop = 2;
			break;
		case "StarrySky":
			stop = 4;
			break;
		case "Arena":
			stop = 0;
			break;
		case "Storm Coast":
			stop = -2;
			break;
		default:
			stop = 0;
			break;
			
		}

		while (transform.position.y < stop) {
		
			transform.Translate(Vector2.up*Time.deltaTime*speed);
			yield return new WaitForFixedUpdate ();
		
		}

	}

	void OnTriggerStay2D(Collider2D coll){

		if (coll.tag == "Player" && coll.gameObject != activator && !coll.gameObject.transform.IsChildOf(activator.transform)) {

			print("Dealing damage");
		
			coll.SendMessage("Damage", damage*Time.deltaTime);
		
		}

	}

}
