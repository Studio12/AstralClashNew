using UnityEngine;
using System.Collections;

public class AquaMissiles : Projectile {

	public GameObject part;


	
	
	// Use this for initialization
	void Start () {
		Debug.Log ("Spawning " + gameObject.name);

		GameObject notThis = GameObject.Find("AquaHeavyProj(Clone)");

		if (this.name == "AquaHeavyProj(Clone)") {
		
			transform.position = new Vector2(this.transform.position.x+(facing*3), transform.position.y);

			if(this.gameObject != notThis){

				Destroy(this.gameObject);

			}
		
		}

		facing = activator.GetComponent<Fighter> ().facing;
		if (facing == 0) {
		
			facing = 1;
		
		}
		//transform.localScale = new Vector2(transform.localScale.x*facing, transform.localScale.y);

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
					if(this.name == "AquaHeavyProj(Clone)" && coll.GetComponent<Fighter>().stars>0){
						
						coll.gameObject.GetComponent<Fighter>().StarLoss();
						if(activator.GetComponent<Fighter>().stars<activator.GetComponent<Fighter>().starMax){
							activator.GetComponent<Fighter>().stars++;
						}
						
					}
				}
				Instantiate(part, transform.position, transform.rotation);
				Destroy (this.gameObject);
			}
		}
	}
}
