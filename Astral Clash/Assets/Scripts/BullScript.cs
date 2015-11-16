using UnityEngine;
using System.Collections;

public class BullScript : MonoBehaviour {

	public GameObject activator;
	public GameObject indicator;
	public GameObject indObj;
	public GameObject box;
	public LayerMask layer;

	// Use this for initialization
	void Start () 
	{
		box = GameObject.Find ("OffScreenBox");
		if (!GetComponent<SpriteRenderer> ().isVisible) {
		
			OnBecameInvisible();
		
		}
		InvokeRepeating("Moove", 1f, 0.02f);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		if (this.transform.position.x> 60) 
		{
			OnBecameVisible();
			foreach(Transform c in GetComponentsInChildren<Transform>()){
				
				Destroy(c.gameObject);
				
			}
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
			collider.SendMessage("Damage", 25);
		}
	}

	void OnBecameInvisible(){

		RaycastHit2D ray = Physics2D.Linecast (this.transform.position, box.transform.position, layer);
		print (ray.transform.gameObject.name);
		indObj = (GameObject)Instantiate (indicator, ray.point, Quaternion.Euler (0, 0, 0));
		indObj.GetComponent<Indicator>().indicated = this.gameObject;
		indObj.GetComponent<Indicator> ().box = box;
		indObj.name = "BullInd" + Random.value.ToString ();


	}

	void OnBecameVisible(){

		if (indObj != null) {
			Destroy (indObj.gameObject);
		}

	}
}
