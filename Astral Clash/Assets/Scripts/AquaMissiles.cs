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

	public float knockback;

	public float facing;

	public GameObject part;

	public float armorbreak;
	
	
	
	// Use this for initialization
	void Start () {
		Debug.Log ("Spawning " + gameObject.name);

		GameObject notThis = GameObject.Find("AquaHeavyProj(Clone)");

		if (this.name == "AquaHeavyProj(Clone)") {
		
			transform.position = new Vector2(this.transform.position.x+(facing*2), transform.position.y);

			if(this.gameObject != notThis){

				Destroy(this.gameObject);

			}
		
		}

		facing = aquaReal.GetComponent<Fighter> ().facing;
		if (facing == 0) {
		
			facing = 1;
		
		}
		//transform.localScale = new Vector2(transform.localScale.x*facing, transform.localScale.y);

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
				if (knockback > 0){
					coll.GetComponent<Fighter> ().isKnockedBack = true;
					coll.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (facing * knockback, knockback), ForceMode2D.Impulse);
				}
				coll.gameObject.SendMessage("Damage", missileDamage);
				coll.gameObject.SendMessage("ArmorDamage", armorbreak, SendMessageOptions.DontRequireReceiver);
				if(this.name == "AquaHeavyProj(Clone)" && coll.GetComponent<Fighter>().stars>0){
					
					coll.gameObject.GetComponent<Fighter>().StarLoss();
					if(aquaReal.GetComponent<Fighter>().stars<aquaReal.GetComponent<Fighter>().starMax){
						aquaReal.GetComponent<Fighter>().stars++;
					}
					
				}
				Instantiate(part, transform.position, transform.rotation);
				Destroy (this.gameObject);
			}
		}
	}
}
