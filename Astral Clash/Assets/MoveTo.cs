using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {

	public GameObject Star;
	public GameObject StarBomb;
	public Transform point;
	public GameObject pass;


	// Use this for initialization
	void Start () {
	
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Vector3.Distance (this.transform.position, point.position) <= 1) {
			
			GameObject spawnStar = (GameObject)Instantiate (Star, point.position, point.rotation);
			Instantiate (StarBomb, spawnStar.transform.position, spawnStar.transform.rotation);
			spawnStar.GetComponent<StarPickup>().StarSpawner = pass;
			spawnStar.name = "Star"+pass.GetComponent<StarSpawn>().curStars.ToString();
			Destroy(this.gameObject);
			
		} else {

			transform.position = Vector3.MoveTowards(this.transform.position, point.position, 70*Time.deltaTime);
			
		}

	}


}
