using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

	public float health = 100;
	public float maxHealth;
	public bool isDead = false;
	public bool isKnockedBack;
	public GameObject damParts;
	public GameObject[] hits;

	// Use this for initialization
	void Start () {
		health = maxHealth;
	}
	
	// Update is called once per frame
	void Alive () {
		//If health is 0 or less, begin death routine, stop everything else
		if (health <= 0 && isDead == false) {
			
			StartCoroutine ("Death");
			isDead = true;
			
		} 
	}

	IEnumerator ShowDamage ()
	{
		
		
		Instantiate (damParts, new Vector2(transform.position.x, transform.position.y+1.5f), transform.rotation);
		
		int r = Random.Range (0, 3);
		
		Instantiate(hits[r], new Vector2(transform.position.x, transform.position.y+1.5f), transform.rotation);
		
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

	void Damage (float amount)
	{
		health -= amount;
		StartCoroutine ("ShowDamage");
	}
}
