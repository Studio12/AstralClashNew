using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Fighter : MonoBehaviour
{
	[System.Serializable]
	public class Attack
	{
		public int damage;
		public float prep;
		public GameObject projectile;
		public float recovery;
		public float reach;
		public float armor;
		public float armorBreak;
		public float knockback;
		public AudioClip sound;
	}
	
	public float health;
	public float armor;
	public float speed;
	public Attack lightAttack;
	public Attack mediumAttack;
	public Attack heavyAttack;
	public float cooldown;
	public float jumpPower;
	public float direction;
	public bool jumping;
	public bool isGrounded = true;
	public int playID;
	public int stars;
	public int starMax;
	public GameObject SpawnPoint;
	public float exJump;
	public float resetJumpVal;
	public float negAcc;
	public float maxHealth;
	public float dodgeCool;
	public bool blocking;
	public bool attacking;
	public string charType;
	public GameObject specialControl;
	public bool isKnockedBack;
	public int facing;


	public AudioClip[] Sounds;


	public GameObject curPlatform;
	public bool AIJump = false;



	// Use this for initialization
	void Start ()
	{
		health = maxHealth;
		dodgeCool = 0;
		cooldown = 0;
		resetJumpVal = 1500;
		exJump = resetJumpVal;
		jumpPower = Mathf.Sqrt(8* Physics2D.gravity.y*-1 * 4);
		this.GetComponentInChildren<Animator> ().SetTrigger ("OpenTaunt");
		specialControl = GameObject.Find ("SpecialManager");
		this.transform.position = SpawnPoint.transform.position;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (direction > 0) {
		
			facing = 1;
		
		} else if (direction < 0) {
		
			facing = -1;
		
		}
		if (health <= 0) {
			
			StartCoroutine ("Death");

		} else {
			if(blocking == true){
				GetComponentInChildren<Animator>().SetBool("Block", true);
				if(direction>.3 && dodgeCool<=0){
					
					StartCoroutine("Dodge", 1);
				}else if(direction<-.3 && dodgeCool<=0){
					StartCoroutine("Dodge", -1);
				}
				
			}
			if(blocking==false && attacking == false && AIJump == false && !isKnockedBack && !this.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("taunt")){
				GetComponentInChildren<Animator>().SetBool("Block", false);
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (direction * speed * 0.25f, GetComponent<Rigidbody2D> ().velocity.y);
				if(Time.timeScale>0){transform.LookAt (transform.position + new Vector3 (0, 0, direction));}
				if(direction != 0){

					this.GetComponentInChildren<Animator>().SetBool("Run", true);
				
				}else{

					this.GetComponentInChildren<Animator>().SetBool("Run", false);
				
				}
				if (jumping == true) {
					if (isGrounded) {
						
						isGrounded = false;
						GetComponent<Rigidbody2D> ().velocity = new Vector2 (this.GetComponent<Rigidbody2D>().velocity.x, jumpPower);
						this.GetComponentInChildren<Animator>().SetBool("Jump", true);
						
					} else if (!isGrounded) {
						
						GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, exJump));
						if (exJump > 0)
							exJump -= negAcc;
						
					}
				}
			}
			if (cooldown > 0)
				cooldown -= Time.deltaTime;
			if(dodgeCool > 0)
				dodgeCool -= Time.deltaTime;
			Debug.DrawLine (transform.position, transform.position + transform.right * 1);
		}
	}
	
	IEnumerator PerformAttack (Attack attack)
	{
		float waitedTime=0;
		bool armorBroken = false;
		print ("Whoosh from " + gameObject.name);
		armor = attack.armor;
		attacking = true;
		cooldown = attack.recovery + attack.prep;
		if (isGrounded == true) {
		
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		
		}
		while (waitedTime<attack.prep) {
		
			if(armor<=0){
				armorBroken = true;
				this.GetComponentInChildren<Animator>().SetTrigger("Interrupt");
				break;
			}
			waitedTime+=Time.deltaTime;
			yield return new WaitForFixedUpdate();
		
		}
//		for (waitedTime=0; waitedTime<attack.prep; waitedTime+=.1f) {
//			
//			if(armor<=0){
//				armorBroken = true;
//				this.GetComponentInChildren<Animator>().SetTrigger("Interrupt");
//				break;
//			}
//			yield return new WaitForSeconds(.1f);
//			
//		}
		if (!armorBroken) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.right, attack.reach);
			if (hit.collider != null && hit.collider != this.GetComponent<Collider2D> ()) {
				print ("Pow from " + gameObject.name);
				if(hit.collider.gameObject.GetComponent<Fighter> ())
				{
					hit.collider.SendMessage ("Damage", attack.damage);
					hit.collider.SendMessage ("ArmorDamage", attack.armorBreak);
					hit.collider.GetComponent<Fighter>().isKnockedBack = true;
					if(attack.knockback>0)hit.collider.GetComponentInChildren<Animator>().SetTrigger("Interrupt");
					hit.collider.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (facing * attack.knockback, attack.knockback), ForceMode2D.Impulse);
				}

			}
			if (attack.projectile)
				Instantiate (attack.projectile, transform.position, transform.rotation);
		}
		print ("Attack ended");
		armor = 1;
		attacking = false;
	}
	
	public void LightAttack ()
	{
		if (cooldown <= 0) {
			StartCoroutine (PerformAttack (lightAttack));
			this.GetComponentInChildren<Animator> ().SetTrigger ("Light");
		}
	}
	
	public void MediumAttack ()
	{
		if (cooldown <= 0) {
			StartCoroutine (PerformAttack (mediumAttack));
			this.GetComponentInChildren<Animator> ().SetTrigger ("Medium");
		}
	}
	
	public void HeavyAttack ()
	{
		if (cooldown <= 0) {
			StartCoroutine (PerformAttack (heavyAttack));
			this.GetComponentInChildren<Animator> ().SetTrigger ("Heavy");
		}
	}
	
	void Damage (float amount)
	{
		if (blocking == false) {
			health -= amount;
			StartCoroutine("ShowDamage");
		}
	}

	void ArmorDamage (float amount)
	{
		armor -= amount;
	}


	IEnumerator Death(){
		yield return new WaitForSeconds(.1f);
		Destroy (this.gameObject);
	}

	public void Ringout(){
		if (health > (2 * (maxHealth / 10))) {
			
			health -= (maxHealth / 10);
			
		} else {
			
			health = maxHealth/10;
			
		}
		this.transform.position = SpawnPoint.transform.position;
	}

	IEnumerator Dodge(float dir){
		dodgeCool = .5f;
		this.GetComponentInChildren<Animator> ().SetTrigger ("Dodge");
		foreach (Transform t in GetComponentsInChildren<Transform>()) {
		
			t.gameObject.layer = LayerMask.NameToLayer("Dodge");
		
		}
		this.gameObject.layer = LayerMask.NameToLayer("Dodge");
		foreach (SpriteRenderer s in GetComponentsInChildren<SpriteRenderer>()) {
		
			s.color = new Color(1f,1f,1f, .2f);

		}
		for (int i=0; i<15; i++) {
			this.transform.position = new Vector2(this.transform.position.x + (dir * .5f), this.transform.position.y);
			yield return new WaitForSeconds (.01f);
		}
		foreach (SpriteRenderer s in GetComponentsInChildren<SpriteRenderer>()) {
			
			s.color = new Color(1f,1f,1f, 1f);
			
		}
		this.gameObject.layer = LayerMask.NameToLayer("Player");
		foreach (Transform t in GetComponentsInChildren<Transform>()) {
			
			t.gameObject.layer = LayerMask.NameToLayer("Player");
			
		}
	}

	public void SpecialAttack()
	{
		print ("Special Attack button pressed");

		if (stars == starMax) {
			print ("Special Attack button pressed");
			this.GetComponentInChildren<Animator> ().SetTrigger ("Special");
			specialControl.GetComponent<SpecialAttack> ().Special (charType, this.gameObject);
			
			stars = 0;
		}
	}

	void PlaySound(AudioClip clip)
	{
		GetComponent<AudioSource>().clip = clip;
		GetComponent<AudioSource>().Play();
	}

	IEnumerator ShowDamage(){

		foreach (SpriteRenderer s in GetComponentsInChildren<SpriteRenderer>()) {
		
			s.color = Color.red;
		
		}
		yield return new WaitForSeconds(.1f);
		foreach (SpriteRenderer s in GetComponentsInChildren<SpriteRenderer>()) {
			
			s.color = new Color(1,1,1);
			
		}

	}
}