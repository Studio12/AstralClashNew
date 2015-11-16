using UnityEngine;
using System.Collections;

public class HeadBounce : MonoBehaviour {

	public float bounceOnPlayer;

	private Rigidbody2D playerRB;


	// Use this for initialization
	void Start () {
		playerRB = transform.parent.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			playerRB.velocity = new Vector2(playerRB.velocity.x, bounceOnPlayer);

		}

	}
}
