using UnityEngine;
using System.Collections;

public class LeoProj : MonoBehaviour {

	public Sprite[] attSprites;
	public float damAmt;
	public float speed;
	public float life;
	public float tempLife;
	public float scalespeed;
	public float knockback;
	public GameObject activator;
	public float facing;
	public float armorbreak;

	// Use this for initialization
	void Start () {

		tempLife = life;
		facing = activator.GetComponent<Fighter> ().facing;
		if (facing == 0) {
		
			facing = 1;
		
		}
		if (this.name == "LeoMedProj(Clone)") {
		
			this.transform.position = new Vector2(this.transform.position.x+(facing*2), transform.position.y);
		
		}else if (this.name == "LeoHeavyProj(Clone)") {
			
			this.transform.position = new Vector2(this.transform.position.x, transform.position.y+1);
			
		}
		//transform.localScale = new Vector2(transform.localScale.x*facing, transform.localScale.y);


	
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.Translate( new Vector3(speed*Time.deltaTime, 0, 0));
		life -= Time.deltaTime;

		this.transform.localScale = new Vector2 (transform.localScale.x + (scalespeed*Time.deltaTime), transform.localScale.y + (scalespeed*Time.deltaTime));

		if(life<(tempLife-(tempLife/3)) && life>(tempLife/3)){

			this.GetComponent<SpriteRenderer>().sprite = attSprites[1];

		}else if(life>(tempLife/3) && life>0){

			this.GetComponent<SpriteRenderer>().sprite = attSprites[2];

		}
		if(life <= 0) {
			Destroy (this.gameObject);
		}

	
	}

	void OnTriggerEnter2D(Collider2D coll){
		
		if (coll.tag =="Player") {
			if(coll.gameObject != activator && !coll.transform.IsChildOf(activator.transform))
			{
				print (coll.gameObject.name + " colliding with" + gameObject.name);
				if (knockback > 0){
					coll.GetComponent<Fighter> ().isKnockedBack = true;
					coll.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (facing * knockback, knockback), ForceMode2D.Impulse);
				}
				coll.gameObject.SendMessage("Damage", damAmt);
				coll.gameObject.SendMessage("ArmorDamage", armorbreak);
				if(this.name == "LeoHeavyProj(Clone)" && coll.GetComponent<Fighter>().stars>0){
					
					coll.gameObject.GetComponent<Fighter>().StarLoss();
					if(activator.GetComponent<Fighter>().stars<activator.GetComponent<Fighter>().starMax){
						activator.GetComponent<Fighter>().stars++;
					}
					
				}
				if(this.name != "LeoSpecialProj(Clone)"){
				Destroy (this.gameObject);
				}
			}
		}
	}

}
