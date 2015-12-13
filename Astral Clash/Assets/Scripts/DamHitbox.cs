using UnityEngine;
using System.Collections;

public class DamHitbox : MonoBehaviour {

	public float damage;
	public float knockback;
	public float armorbreak;
	public GameObject activator;

	// Use this for initialization
	void Start () {

		activator = transform.parent.parent.gameObject;
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	void OnTriggerEnter2D(Collider2D coll){

		if (coll.GetComponent <Actor>()) {
			if(coll.gameObject != activator && !coll.transform.IsChildOf(activator.transform))
			{
				print (coll.gameObject.name + " colliding with" + gameObject.name);
				if (knockback > 0){
					coll.GetComponent<Actor> ().isKnockedBack = true;
					coll.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (activator.GetComponent<Fighter> ().facing * knockback, knockback), ForceMode2D.Impulse);
				}
				coll.gameObject.SendMessage("Damage", damage);
				print(damage);
				print(coll.name);
				print(activator.name);
				activator.GetComponent<Fighter>().dPause();
				activator.GetComponent<Fighter>().ShakeFunction(coll.gameObject, damage);
				if(this.name == "HitBox_Heavy" && coll.GetComponent<Fighter>().stars>0){

					coll.gameObject.GetComponent<Fighter>().StarLoss();
					if(activator.GetComponent<Fighter>().stars<activator.GetComponent<Fighter>().starMax){
						activator.GetComponent<Fighter>().stars++;
					}

				}
				if(coll.GetComponent <Fighter>())
				{
					coll.gameObject.SendMessage("ArmorDamage", armorbreak);
				}
			}
		}

	}
}
