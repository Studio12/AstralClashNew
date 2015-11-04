using UnityEngine;
using System.Collections;

public class Knife : MonoBehaviour {

	public GameObject activator;
	
	// Use this for initialization
	void Start () 
	{
		InvokeRepeating("Move", 0f, 0.001f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (this.transform.position.y < -21) 
		{
			Destroy(gameObject);
		}
	}
	
	public void Move()
	{ 
		transform.Translate (new Vector2(0,-.05f));
		//this.transform.position = new Vector2 (this.transform.position.x + 1f , this.transform.position.y);
		
	}

	public void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Player" && collider.gameObject != activator)
		{
			print("Knife HIT");
			collider.SendMessage("Damage", 10);
		}
	}

}
