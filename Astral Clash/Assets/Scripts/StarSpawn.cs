using UnityEngine;
using System.Collections;

public class StarSpawn : MonoBehaviour {

	public GameObject Star;
	public int StarLimit;
	public int curStars;
	public float timer;

	// Use this for initialization
	void Start () {

		curStars = 0;
		InvokeRepeating ("StarSpawning", timer, timer);
	
	}

	void StarSpawning(){

		if(curStars < StarLimit) {

			GameObject spawnedStar = (GameObject)Instantiate(Star, new Vector3((Random.Range(((this.transform.localScale.x/2)*(-1)), (this.transform.localScale.x/2)+1)+this.transform.position.x),this.transform.position.y, this.transform.position.z), this.transform.rotation);
			spawnedStar.name = "Star"+curStars.ToString();
			spawnedStar.GetComponent<StarPickup>().StarSpawner = this.gameObject;
			curStars++;

		}

	}

}
