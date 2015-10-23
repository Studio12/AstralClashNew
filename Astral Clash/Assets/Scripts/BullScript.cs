using UnityEngine;
using System.Collections;

public class BullScript : MonoBehaviour {

	public GameObject activator;

	// Use this for initialization
	void Start () 
	{
		InvokeRepeating("Moove", 1f, 0.02f);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		if (this.transform.position.x> 36) 
		{
			Destroy(gameObject);
		}
	}

	public void Moove()
	{ 
		this.transform.position = new Vector2 (this.transform.position.x + 1f , this.transform.position.y);
		
	}

	public void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Player" && collider.gameObject != activator)
		{
			print("Bull HIT");
			collider.SendMessage("Damage", 40);
		}
	}
}
