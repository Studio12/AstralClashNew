using UnityEngine;
using System.Collections;

public class AquaGrenade : MonoBehaviour {
	
	//This is for referencing Aquarius in the fighter script
	public GameObject aquaHost;
	//Have to reference camera for collision issues======
	public GameObject refCamera;
	//Forward speed of grenade
	public float projSpeedX;
	//Upward speed of grenade
	public float projSpeedY;
	//Maximum speed for the x direction
	public float maxSpeedX;
	//Life span of the object, set to whatever you want
	public float lifeSpan;
	//How long the object will go up into the air before coming down
	public float arcSpan;
	//Maximum time before object starts coming down
	public float maxArc;
	//This thing has a rigidbody2d, so let's use it
	public Rigidbody2D rig2d;
	
	//So we know which direction Aquarius is facing----so the projectile goes in that direction
	private int storeDirection;
	
	// Use this for initialization
	void Start () {
		Debug.Log ("Spawning " + gameObject.name);
		rig2d = this.GetComponent<Rigidbody2D> ();
		
		refCamera = GameObject.FindWithTag ("MainCamera");
		
		storeDirection = aquaHost.GetComponent<Fighter> ().facing;
		rig2d.AddForce(Vector2.up * projSpeedY);
	}
	
	// Update is called once per frame
	void Update () {
		lifeSpan -= Time.deltaTime;
		if(lifeSpan <= 0) 
			Destroy (this.gameObject);
		
		arcSpan += Time.deltaTime;
		if(arcSpan <= maxArc)
			rig2d.AddForce(Vector2.up * projSpeedY);
		
		if(storeDirection == 1)
			rig2d.velocity = new Vector2 (projSpeedX, rig2d.velocity.y);
		else if(storeDirection == -1)
			rig2d.velocity = new Vector2 (-projSpeedX, rig2d.velocity.y);
		
	}
	
	void FixedUpdate(){
		
		if (rig2d.velocity.x > maxSpeedX)
			rig2d.velocity = new Vector2 (maxSpeedX, rig2d.velocity.y);
		if (rig2d.velocity.x < -maxSpeedX)
			rig2d.velocity = new Vector2 (-maxSpeedX, rig2d.velocity.y);
	}
	
	
	void OnTriggerEnter2D(Collider2D coll){
		
		if (coll.tag == "Player") {
			if (coll.gameObject != aquaHost && !coll.transform.IsChildOf (aquaHost.transform)) {
				print (coll.gameObject.name + " colliding with" + gameObject.name);
				Destroy (this.gameObject);
			}
		}
		else if (coll.gameObject != refCamera && !coll.transform.IsChildOf (refCamera.transform)) {
			if (coll.gameObject != aquaHost && !coll.transform.IsChildOf (aquaHost.transform)) {
				print (coll.gameObject.name + " colliding with" + gameObject.name);
				Destroy (this.gameObject);
			}
		}
	}
}