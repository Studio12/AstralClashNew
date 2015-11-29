using UnityEngine;
using System.Collections;

public class SemiSolid : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D player){

		if(player.tag == "Player"){

			GameObject platform = transform.parent.gameObject;
			Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), platform.GetComponent<BoxCollider2D>());

		}

	}

	void OnTriggerExit2D(Collider2D player){

		if (player.tag == "Player") {
		
			GameObject platform = transform.parent.gameObject;
			Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), platform.GetComponent<BoxCollider2D>(), false);
		
		}

	}


}
