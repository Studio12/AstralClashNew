using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*

This is the main fighter class, controlling every action the fighters onscreen can do.
Controlled via either playercontroller script or main AI script, and interacts directly with special attack and ground check scripts

*/

public class Fighter : MonoBehaviour
{
	[System.Serializable]

	//Attack class, for defining different types of attacks. Light, medium, heavy.
	public class Attack
	{

		public int damage; //Damage of attack
		public float prep; //Time before attack hits
		public GameObject projectile; //Attack's projectile, can be null
		public float recovery; //Time before attack can be used again
		public float reach; //Distance covered by attack
		public float armor; //Interrupt threshold on attack
		public float armorBreak; //Attempts to defeat interrupt threshold of other's attacks
		public float knockback; //Force for knockback on hit

	}

	//Attributes of the class. All public for now as we write code.
	public float health; 				//Manipulatable health of fighter.
	public float armor; 				//Used for interrupt threshold.
	public float speed; 				//Speed multiplier for movement.
	public Attack lightAttack; 			//Character's light attack, defined in editor.
	public Attack mediumAttack; 		//Medium attack
	public Attack heavyAttack; 			//Heavy Attack
	public float cooldown; 				//Attack prep+recovery, time til another attack can be done.
	public float jumpPower; 			//Minimum jump force.
	public float direction; 			//Movement direction multiplier.
	public bool jumping; 				//Jumping state.
	public bool isGrounded = true; 		//Grounded state.
	public int playID; 					//Player controlling character, or 0 for AI.
	public int stars; 					//Stars carried, for special attack trigger.
	public int starMax; 				//Max stars carried.
	public GameObject SpawnPoint; 		//Respawn point, set via game manager.
	public float exJump; 				//Force added on holding jump.
	public float resetJumpVal; 			//Reset value for extra jump force.
	public float negAcc; 				//Reduces extra jump force at this rate in air.
	public float maxHealth; 			//Maximum health value of character, not changed
	public float dodgeCool; 			//Time between dodge actions.
	public bool blocking; 				//Block state.
	public bool attacking; 				//Attacking state.
	public string charType; 			//Which fighter is this?
	public GameObject specialControl; 	//Reference to special attack controller object.
	public bool isKnockedBack; 			//Knockback state.
	public int facing; 					//Reference for last facing of character.
	public AudioClip[] Sounds; 			//Soundclips associated with character.
	public GameObject curPlatform; 		//Current platform the character is on.
	public bool AIJump = false; 		//AI controlling movement for jump state.
	public bool dodgeDelay = false;
	public bool jump2 = true;



	// Use this for initialization
	void Start ()
	{
		//Reset and set all relevant variables.
		health = maxHealth;
		dodgeCool = 0;
		cooldown = 0;
		jumpPower = Mathf.Sqrt (8 * Physics2D.gravity.y * -1 * 6); 			//Minimum jump height equals number on right in y units
		resetJumpVal = 1800;
		negAcc = 150;
		exJump = resetJumpVal;
		this.GetComponentInChildren<Animator> ().SetTrigger ("OpenTaunt"); 	//Play opening taunt animation.
		specialControl = GameObject.Find ("SpecialManager"); 				//Find the special attack controller in scene.
		this.transform.position = SpawnPoint.transform.position; 			//Sets position to spawnpoint.
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Sets facing to left or right depending on direction float axis
		if (direction > 0) {
		
			facing = 1;
		
		} else if (direction < 0) {
		
			facing = -1;
		
		}

		//If health is 0 or less, begin death routine, stop everything else
		if (health <= 0) {
			
			StartCoroutine ("Death");

		} else {

			//Blocking state
			if (blocking == true) {

				//Start block animation
				GetComponentInChildren<Animator> ().SetBool ("Block", true);

				//If attempting movement while blocking and dodge cooldown done, begin dodge in direction
				if (direction > .3 && dodgeCool <= 0) {
					
					StartCoroutine ("Dodge", 1);
				} else if (direction < -.3 && dodgeCool <= 0) {
					StartCoroutine ("Dodge", -1);
				}
				
			}

			//If not Blocking, Attacking, AIJumping, Knocked Back, or in Opening taunt animation, normal movement is possible
			if (blocking == false && attacking == false && AIJump == false && !isKnockedBack && !this.GetComponentInChildren<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("taunt")) {

				//Make sure animator knows not to use block animation, and set x velocity according to direction axis
				GetComponentInChildren<Animator> ().SetBool ("Block", false);
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (direction * speed * 0.25f, GetComponent<Rigidbody2D> ().velocity.y);

				//If not paused, then set character rotation to look in moving direction
				if (Time.timeScale > 0) {
					transform.LookAt (transform.position + new Vector3 (0, 0, direction));
				}

				//Control running animation by direction
				if (direction != 0) {

					this.GetComponentInChildren<Animator> ().SetBool ("Run", true);
				
				} else {

					this.GetComponentInChildren<Animator> ().SetBool ("Run", false);
				
				}

				//If character is attempting to jump
				if (Input.GetButtonDown("Jump"+playID.ToString())) {

					//If character is on the ground
					if (isGrounded) {

						//Character is no longer grounded, apply minimum jump force, start jump animation
						isGrounded = false;
						GetComponent<Rigidbody2D> ().velocity = new Vector2 (this.GetComponent<Rigidbody2D> ().velocity.x, jumpPower);
						this.GetComponentInChildren<Animator> ().SetBool ("Jump", true);
						
					} 

					//If character isn't on the ground
					else if (!isGrounded) {

						if(jump2){

							jump2 = false;
							GetComponent<Rigidbody2D> ().velocity = new Vector2 (this.GetComponent<Rigidbody2D> ().velocity.x, jumpPower);
							this.GetComponentInChildren<Animator> ().SetBool ("Jump", true);

						}
						//While button is held, add a diminishing force to the jump for extra height.
						//exJump+((exJump-negAcc)*(exJump/negAcc))
//						GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, exJump));
//						if (exJump > 0)
//							exJump -= negAcc;
//						
					}
				}
			}

			//Reduce cooldowns according to time passed in the last frame.
			if (cooldown > 0)
				cooldown -= Time.deltaTime;
			if (dodgeCool > 0)
				dodgeCool -= Time.deltaTime;
			Debug.DrawLine (transform.position, transform.position + transform.right * 1); //Line drawn in front of character for debug.
		}
	}


	/// PERFORM ATTACK
	/// Takes an attack type, and performs all relevant actions of that attack.
	///
	IEnumerator PerformAttack (Attack attack)
	{

		print ("Whoosh from " + gameObject.name);
		float waitedTime = 0; 						//Timer control for attack prep
		bool armorBroken = false; 					//Whether attack has been interrupted
		armor = attack.armor; 						//Attack's interrupt value
		attacking = true;							//Character is attacking
		cooldown = attack.recovery + attack.prep;	//Cooldown for next attack waits til current is done

		//If attacking on the ground, prevent movement. Otherwise, character will continue to move in air.
		if (isGrounded == true) {
		
			this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		
		}

		//While the attack hasn't yet happened
		while (waitedTime<attack.prep) {
		
			//If interrupt threshold passed, break, set interrupt animation
			if (armor <= 0) {
				armorBroken = true;
				this.GetComponentInChildren<Animator> ().SetTrigger ("Interrupt");
				break;
			}

			//Add onto timer time for last frame, check once per frame
			waitedTime += Time.deltaTime;
			yield return new WaitForFixedUpdate ();
		
		}

		//If attack isn't interrupted
		if (!armorBroken) {

			//Send out raycast to see if something is hit. Length of raycast is attack's reach.
			RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.right, attack.reach);

			//If it hits something, that's not the current character
			if (hit.collider != null && hit.collider != this.GetComponent<Collider2D> ()) {

				print ("Pow from " + gameObject.name);
				//If the object hit is another fighter
				if (hit.collider.gameObject.GetComponent<Fighter> ()) {

					//Reduce health and armor
					hit.collider.SendMessage ("Damage", attack.damage);
					hit.collider.SendMessage ("ArmorDamage", attack.armorBreak);

					//If attack has a knockback associated with it, knock back hit object, prevent their movement while knocked back, and set interrupt animation
					if (attack.knockback > 0){
						hit.collider.GetComponent<Fighter> ().isKnockedBack = true;
						hit.collider.GetComponentInChildren<Animator> ().SetTrigger ("Interrupt");
						hit.collider.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (facing * attack.knockback, attack.knockback), ForceMode2D.Impulse);
					}
				}
				else if (hit.collider.gameObject.GetComponent<CometBug> ()) {
					
					//Reduce health
					hit.collider.SendMessage ("Damage", attack.damage);
				}

			}

			//If attack has a projectile, send it out
			if (attack.projectile)
				Instantiate (attack.projectile, transform.position, transform.rotation);
		}

		//Reset armor to 1 to prevent overlap on button mash, reset state to not attacking.
		print ("Attack ended");
		armor = 1;
		attacking = false;
	}

	/// ATTACKS
	/// Attacks of current fighter. Intermediary for PerformAttack, sets correct animation and passes correct attack if character isn't cooling down.
	///
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


	/// DAMAGE
	/// Reduces health by specified amount. Accessed via SendMessage.
	/// 
	void Damage (float amount)
	{
		//If not blocking, reduce health and convey damage on character
		if (blocking == false || dodgeDelay==true) {
			health -= amount;
			StartCoroutine ("ShowDamage");
		}
	}

	/// ARMOR DAMAGE
	/// Reduces armor on an attack, bringing closer to interrupt.
	///
	void ArmorDamage (float amount)
	{
		armor -= amount;
	}
	
	/// DEATH
	/// Destroys the current character. This will encompass any visual effects on death when ready.
	///
	IEnumerator Death ()
	{
		yield return new WaitForSeconds (.1f);
		Destroy (this.gameObject);
	}


	/// RINGOUT
	/// If character falls outside of the arena, reduce health and respawn.
	/// 
	public void Ringout ()
	{

		//If character health is greater than at least twice a tenth of max, reduce by one tenth
		if (health > (2 * (maxHealth / 10))) {
			
			health -= (maxHealth / 10);
			
		} 
		//If character is at 1/10 health, or would go under otherwise, set to 1/10 health.
		else {
			
			health = maxHealth / 10;
			
		}
		//Respawn character at spawnpoint.
		this.transform.position = SpawnPoint.transform.position;
	}

	/// DODGE
	/// Dodge in the specified direction.
	/// 
	IEnumerator Dodge (float dir)
	{
		//Set time between dodges, and trigger dodge animation
		dodgeCool = .5f;

		this.GetComponentInChildren<Animator> ().SetTrigger ("Dodge");

		//Make this object and all children use the Dodge physics layer
		foreach (Transform t in GetComponentsInChildren<Transform>()) {
		
			t.gameObject.layer = LayerMask.NameToLayer ("Dodge");
		
		}
		this.gameObject.layer = LayerMask.NameToLayer ("Dodge");

		dodgeDelay = true;

		for (int i=0; i<5; i++) {
			this.transform.position = new Vector2 (this.transform.position.x + (dir * .5f), this.transform.position.y);
			yield return new WaitForSeconds (.01f);
		}

		dodgeDelay = false;

		//Make all children objects semi-transparent, convey dodge state
		foreach (SpriteRenderer s in GetComponentsInChildren<SpriteRenderer>()) {
			
			s.color = new Color (1f, 1f, 1f, .2f);
			
		}

		//Moves character in passed direction, once every small amount of time a certain number of times to simulate smoother movement
		for (int i=0; i<10; i++) {
			this.transform.position = new Vector2 (this.transform.position.x + (dir * .5f), this.transform.position.y);
			yield return new WaitForSeconds (.01f);
		}

		//Reset colors and physics layers of this and all children objects
		foreach (SpriteRenderer s in GetComponentsInChildren<SpriteRenderer>()) {
			
			s.color = new Color (1f, 1f, 1f, 1f);
			
		}
		this.gameObject.layer = LayerMask.NameToLayer ("Player");
		foreach (Transform t in GetComponentsInChildren<Transform>()) {
			
			t.gameObject.layer = LayerMask.NameToLayer ("Player");
			
		}

		blocking = false;
	}

	/// SPECIAL ATTACK
	/// Triggers the special attack controller, passing in character type to determine which special to use
	///
	public void SpecialAttack ()
	{
		print ("Special Attack button pressed");

		//If character has enough stars to use a special attack
		if (stars == starMax) {

			//Set special attack animation, trigger special attack, and reset special stars
			print ("Special Attack button pressed");
			this.GetComponentInChildren<Animator> ().SetTrigger ("Special");
			specialControl.GetComponent<SpecialAttack> ().Special (charType, this.gameObject);
			
			stars = 0;
		}
	}

	/// PLAY SOUND
	/// Switches audiosource clip to passed clip, and plays sound.
	/// 
	void PlaySound (AudioClip clip)
	{
		GetComponent<AudioSource> ().clip = clip;
		GetComponent<AudioSource> ().Play ();
	}

	/// SHOW DAMAGE
	/// Creates visual effects to show attack damage on characters.
	/// 
	IEnumerator ShowDamage ()
	{

		//Causes all children objects to take on a red tint
		foreach (SpriteRenderer s in GetComponentsInChildren<SpriteRenderer>()) {
		
			s.color = Color.red;
		
		}

		//Waits .1s, changes back to normal
		yield return new WaitForSeconds (.1f);
		foreach (SpriteRenderer s in GetComponentsInChildren<SpriteRenderer>()) {
			
			s.color = new Color (1, 1, 1);
			
		}

	}

	IEnumerator ShowStarMax(){

		while (stars == starMax) {
		
			foreach (SpriteRenderer s in GetComponentsInChildren<SpriteRenderer>()) {
				
				s.color = new Color(.5f,.5f,1);
				
			}
			yield return new WaitForFixedUpdate();
			foreach (SpriteRenderer s in GetComponentsInChildren<SpriteRenderer>()) {
				
				s.color = new Color(1f,1f,1);
				
			}
			yield return new WaitForFixedUpdate();
		
		}

	}
}