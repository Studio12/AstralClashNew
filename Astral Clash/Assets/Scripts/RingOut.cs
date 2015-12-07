using UnityEngine;
using System.Collections;

public class RingOut : MonoBehaviour {

	public GameObject parts;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	void OnTriggerEnter2D(Collider2D collide){
//
//		print ("Something collided");
//		if (collide.tag == "Player") {
//		
//			print("Player collided");
//
//			collide.GetComponent<Fighter>().Ringout();
//			GetComponent<AudioSource>().Play();
//		
//		}
//
//	}

	void OnCollisionEnter2D(Collision2D collide){

		if (collide.gameObject.GetComponent<Actor>()) {
		
			foreach (ContactPoint2D contact in collide.contacts) {
		
				GameObject particles = (GameObject)Instantiate (parts, contact.point, this.transform.rotation);
				particles.GetComponent<Lookat>().killbox = this.gameObject;
		
			}

			collide.gameObject.GetComponent<Actor>().Ringout();
			GetComponent<AudioSource>().Play();
		}

	}

}
