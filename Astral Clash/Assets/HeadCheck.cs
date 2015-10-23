using UnityEngine;
using System.Collections;

public class HeadCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D collide){

		if (collide.tag == "Player") {
		
			if(collide.transform.position.x>this.transform.position.x){

				collide.GetComponentInParent<Transform>().Translate(new Vector2(10, 0)*Time.deltaTime, Space.World);


				                            }else{

				collide.GetComponentInParent<Transform>().Translate(new Vector2(-10,0)*Time.deltaTime, Space.World);

				}


		}


	}

}
