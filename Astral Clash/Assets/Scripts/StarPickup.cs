using UnityEngine;
using System.Collections;

public class StarPickup : MonoBehaviour {

	public GameObject StarSpawner;

	private Rigidbody2D starry;

	public float movementSpeed;
	public bool isRight;

	public LayerMask isThereWall;
	public Transform checkingWall;
	public bool hitWall;
	public float wallRadius;


	void Start(){
		starry = GetComponent<Rigidbody2D> ();

		switch (Random.Range (0, 2)) {
		
		case 0:
			isRight = false;
			break;

		case 1:
			isRight = true;
			break;
		
		default:
			break;
		
		}

	}

	void Update(){

		hitWall = Physics2D.OverlapCircle (checkingWall.position, wallRadius, isThereWall);

		if (hitWall) {
			isRight = !isRight;
		}

		if (isRight) {
			transform.localScale = new Vector3 (-3, 3, 1);
			starry.velocity = new Vector2 (-movementSpeed, starry.velocity.y);
		} 
		else {
			transform.localScale = new Vector3(3, 3, 1);
			starry.velocity = new Vector2(movementSpeed, starry.velocity.y);
		}
	}


	void OnTriggerEnter2D(Collider2D coll){

		//Star detects collision with player and does stuff
		if (coll.tag == "Player") {
			if(coll.name == "FeetCheck" || coll.name == "FeetReal"){
				if (coll.transform.parent.GetComponent<Fighter> ().stars < coll.transform.parent.GetComponent<Fighter> ().starMax) {
					coll.transform.parent.GetComponent<Fighter> ().stars++;
				}
				StarSpawner.GetComponent<StarSpawn> ().curStars--;
				Destroy (this.gameObject);


			} else{
				if (coll.GetComponent<Fighter> ().stars < coll.GetComponent<Fighter> ().starMax) {
					coll.GetComponent<Fighter> ().stars++;
				}
				StarSpawner.GetComponent<StarSpawn> ().curStars--;
				Destroy (this.gameObject);
			}
		}
		if (coll.name == "Killbox") {
		
			StarSpawner.GetComponent<StarSpawn> ().curStars--;
			Destroy (this.gameObject);


		}
	}

}
