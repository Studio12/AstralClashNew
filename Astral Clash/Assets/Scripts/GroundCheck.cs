using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {


	private Fighter ThePlayer;
	public GameObject animRef;

	// Use this for initialization
	void Start () {
		ThePlayer = gameObject.GetComponentInParent<Fighter> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Platform") {
			ThePlayer.isGrounded = true;
			ThePlayer.exJump = ThePlayer.resetJumpVal;
			ThePlayer.jump2 = true;
			ThePlayer.curPlatform = coll.gameObject;
			animRef.GetComponent<Animator>().SetBool("Jump", false);
		}

	}


	void OnTriggerStay2D(Collider2D coll){
		if (coll.gameObject.tag == "Platform") {
			ThePlayer.isKnockedBack = false;
			ThePlayer.isGrounded = true;
		}

	}

	void OnTriggerExit2D(Collider2D coll){
		//if (coll.gameObject.tag == "Platform") {
			ThePlayer.isGrounded = false;
		//}

	}
}
