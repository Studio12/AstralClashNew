using UnityEngine;
using System.Collections;

public class AquaMissiles : MonoBehaviour {
	
	//This is for referencing Aquarius specifically
	public GameObject aquaReal;
	//the speed of the projectile
	public float projSpeed;
	//How long the missile lasts for
	public float lifeSpan;
	//The damage it does
	public int missileDamage;
	
	// Use this for initialization
	void Start () {
		Debug.Log ("Spawning " + gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate( new Vector3(projSpeed*Time.deltaTime, 0, 0));
		lifeSpan -= Time.deltaTime;
		if(lifeSpan <= 0) 
			Destroy (this.gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		
		if (coll.tag =="Player") {
			if(coll.gameObject != aquaReal && !coll.transform.IsChildOf(aquaReal.transform))
			{
				print (coll.gameObject.name + " colliding with" + gameObject.name);
				coll.gameObject.SendMessage("Damage", missileDamage);
				Destroy (this.gameObject);
			}
		}
	}
}