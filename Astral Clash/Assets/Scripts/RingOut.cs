using UnityEngine;
using System.Collections;

public class RingOut : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collide){

		print ("Something collided");
		if (collide.tag == "Player") {
		
			print("Player collided");
			collide.GetComponent<Fighter>().Ringout();
		
		}

	}

}
