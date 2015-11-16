using UnityEngine;
using System.Collections;

public class StarPickup : MonoBehaviour {

	public GameObject StarSpawner;

	public GameObject indicator;
	public GameObject indObj;
	public GameObject box;
	public LayerMask layer;

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

		box = GameObject.Find ("OffScreenBox");
		if (!GetComponent<SpriteRenderer> ().isVisible) {
			
			OnBecameInvisible();
			
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
					if(coll.transform.parent.GetComponent<Fighter> ().stars == 3){
						coll.transform.parent.GetComponent<Fighter> ().StartCoroutine("ShowStarMax");
					}
				}
				StarSpawner.GetComponent<StarSpawn> ().curStars--;
				if(indObj!=null){
					Destroy(indObj.gameObject);
				}
				foreach(Transform c in GetComponentsInChildren<Transform>()){
					
					Destroy(c.gameObject);
					
				}
				Destroy (this.gameObject);


			} else{
				if (coll.GetComponent<Fighter> ().stars < coll.GetComponent<Fighter> ().starMax) {
					coll.GetComponent<Fighter> ().stars++;
					if(coll.GetComponent<Fighter> ().stars == 3){
						coll.GetComponent<Fighter> ().StartCoroutine("ShowStarMax");
					}
				}
				StarSpawner.GetComponent<StarSpawn> ().curStars--;
				if(indObj!=null){
					Destroy(indObj.gameObject);
				}
				foreach(Transform c in GetComponentsInChildren<Transform>()){
					
					Destroy(c.gameObject);
					
				}
				Destroy (this.gameObject);
			}
		}
		if (coll.name == "Killbox") {
		
			StarSpawner.GetComponent<StarSpawn> ().curStars--;
			if(indObj!=null){
				Destroy(indObj.gameObject);
			}

			foreach(Transform c in GetComponentsInChildren<Transform>()){

				Destroy(c.gameObject);

			}
			Destroy (this.gameObject);


		}
	}

	void OnBecameInvisible(){
		if (this.gameObject != null) {
			RaycastHit2D ray = Physics2D.Linecast (this.transform.position, box.transform.position, layer);
			if (ray.collider != null) {
				indObj = (GameObject)Instantiate (indicator, ray.point, Quaternion.Euler (0, 0, 0));
				indObj.GetComponent<Indicator> ().indicated = this.gameObject;
				indObj.GetComponent<Indicator> ().box = box;
				indObj.name = "StarInd" + Random.value.ToString ();
			}
		}

		
		
	}
	
	void OnBecameVisible(){
		
		if (indObj != null) {
			Destroy (indObj.gameObject);
		}
		
	}

}
