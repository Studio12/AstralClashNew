using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	
	//This is for referencing the activator specifically
	public GameObject activator;
	//the speed of the projectile
	public float speed;
	//How long the missile lasts for
	public float lifespan;
	//The damage it does
	public int damage;
	public float knockback;
	public float facing;
	public float armorBreak;
	

	// Use this for initialization
	void Start () {
		Debug.Log ("Spawning " + gameObject.name);

		facing = activator.GetComponent<Fighter> ().facing;
		if (facing == 0) {
			
			facing = 1;
			
		}

		if (this.gameObject.name == "Rocks(Clone)") {
		
			transform.position = new Vector2(this.transform.position.x+(facing*2), transform.position.y-1.5f);
		
		}

	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate( new Vector3(speed*Time.deltaTime, 0, 0));
		lifespan -= Time.deltaTime;
		if(lifespan <= 0) 
			Destroy (this.gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		
		if (coll.GetComponent <Actor>()) {
			if(coll.gameObject != activator && !coll.transform.IsChildOf(activator.transform))
			{
				print (coll.gameObject.name + " colliding with" + gameObject.name);
				if (knockback > 0){
					coll.GetComponent<Actor> ().isKnockedBack = true;
					coll.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (facing * knockback, knockback), ForceMode2D.Impulse);
				}
				coll.gameObject.SendMessage("Damage", damage);
				activator.GetComponent<Fighter>().dPause();
				activator.GetComponent<Fighter>().ShakeFunction(coll.gameObject, damage);
				if(coll.GetComponent <Fighter>())
				{
					coll.gameObject.SendMessage("ArmorDamage", armorBreak);
				}
				Destroy (this.gameObject);
			}
		}
	}
}

