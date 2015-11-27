using UnityEngine;
using System.Collections;

public class LeoProj : Projectile {

	public Sprite[] attSprites;
	public float tempLife;
	public float scaleSpeed;

	// Use this for initialization
	void Start () {

		tempLife = lifespan;
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
		lifespan -= Time.deltaTime;

		this.transform.localScale = new Vector2 (transform.localScale.x + (scaleSpeed*Time.deltaTime), transform.localScale.y + (scaleSpeed*Time.deltaTime));

		if(lifespan<(tempLife-(tempLife/3)) && lifespan>(tempLife/3)){

			this.GetComponent<SpriteRenderer>().sprite = attSprites[1];

		}else if(lifespan>(tempLife/3) && lifespan>0){

			this.GetComponent<SpriteRenderer>().sprite = attSprites[2];

		}
		if(lifespan <= 0) {
			Destroy (this.gameObject);
		}

	
	}

	void OnTriggerEnter2D(Collider2D coll){
		
		if (coll.GetComponent <Actor>()) {
			if(coll.gameObject != activator && !coll.transform.IsChildOf(activator.transform))
			{
				print (coll.gameObject.name + " colliding with" + gameObject.name);
				if (knockback > 0){
					coll.GetComponent<Fighter> ().isKnockedBack = true;
					coll.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (facing * knockback, knockback), ForceMode2D.Impulse);
				}
				coll.gameObject.SendMessage("Damage", damage);
				if(coll.GetComponent <Fighter>())
				{
					coll.gameObject.SendMessage("ArmorDamage", armorBreak);
					if(this.name == "LeoHeavyProj(Clone)" && coll.GetComponent<Fighter>().stars>0){
						
						coll.gameObject.GetComponent<Fighter>().StarLoss();
						if(activator.GetComponent<Fighter>().stars<activator.GetComponent<Fighter>().starMax){
							activator.GetComponent<Fighter>().stars++;
						}
						
					}
				}
				if(this.name != "LeoSpecialProj(Clone)"){
				Destroy (this.gameObject);
				}
			}
		}
	}

}
