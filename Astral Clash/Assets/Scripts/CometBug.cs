using UnityEngine;
using System.Collections;

public class CometBug : Actor {

	public AudioSource source;
	public AudioClip[] sounds;
	
	// Update is called once per frame
	void Update () {
		if ((GameManager.roundNum + 1) != 1) {
			if(health>0){
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (transform.right.x * 5, GetComponent<Rigidbody2D> ().velocity.y);
			GetComponentInChildren<Animator>().SetBool("Walk", true);
			}else{

				GetComponentInChildren<Animator>().SetBool("Walk", false);


			}
		}
		RaycastHit2D hit = Physics2D.Raycast (transform.position + transform.right, -transform.up, 2);
		if(!hit)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y + 180,transform.eulerAngles.z);
		}
		if (health <= 0) {
			StartCoroutine("Death");
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.GetComponent<CometBug> ()) {
			Physics2D.IgnoreCollision(GetComponent<Collider2D> (), coll.gameObject.GetComponent<Collider2D> ());
			return;
		}
		else if (coll.gameObject.GetComponent<Fighter> () && (GameManager.roundNum + 1) > 2) {
			GetComponentInChildren<Animator>().SetTrigger("Attack");
			source.clip = sounds[0];
			source.Play();
			coll.gameObject.SendMessage ("Damage", 5);
		}
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
	}

	/// RINGOUT
	/// If character falls outside of the arena, reduce health and respawn.
	/// 
	public override void Ringout ()
	{
		health = 0;
	}

	IEnumerator Death ()
	{
		print ("Comet Bug Dead");
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
		GetComponentInChildren<Animator> ().SetTrigger ("Dead");
		source.clip = sounds [1];
		source.Play ();
		yield return new WaitForSeconds (1f);
		Destroy (gameObject);
	}
}