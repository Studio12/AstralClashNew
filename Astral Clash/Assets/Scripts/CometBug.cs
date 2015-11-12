using UnityEngine;
using System.Collections;

public class CometBug : MonoBehaviour {

	public float health = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if((GameManager.roundNum + 1) != 1) GetComponent<Rigidbody2D> ().velocity = new Vector2 (transform.right.x * 5, GetComponent<Rigidbody2D> ().velocity.y);
		RaycastHit2D hit = Physics2D.Raycast (transform.position + transform.right, -transform.up, 2);
		if(!hit)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y + 180,transform.eulerAngles.z);
		}
		if (health <= 0) {
			CBWaveManager.BugCount--;
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.GetComponent<CometBug> ()) {
			Physics2D.IgnoreCollision(GetComponent<Collider2D> (), coll.gameObject.GetComponent<Collider2D> ());
			return;
		}
		else if (coll.gameObject.GetComponent<Fighter> () && (GameManager.roundNum + 1) > 2) {
			coll.gameObject.SendMessage ("Damage", 5);
		}
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
	}

	void Damage (float amount)
	{
		health -= amount;
	}
}