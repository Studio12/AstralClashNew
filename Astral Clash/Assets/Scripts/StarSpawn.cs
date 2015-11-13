using UnityEngine;
using System.Collections;

public class StarSpawn : MonoBehaviour {

	public GameObject StarPoint;
	public GameObject[] SSpawns;
	public int StarLimit;
	public int curStars;
	public float timer;

	// Use this for initialization
	void Start () {

		curStars = 0;
		SSpawns = GameObject.FindGameObjectsWithTag ("SSpawn");
		InvokeRepeating ("StarSpawning", timer, timer);
	
	}

	void StarSpawning(){

		if(curStars < StarLimit) {

			GameObject spawnedStar = (GameObject)Instantiate(StarPoint, new Vector3((Random.Range(((this.transform.localScale.x/2)*(-1)), (this.transform.localScale.x/2)+1)+this.transform.position.x),this.transform.position.y, this.transform.position.z), this.transform.rotation);
			spawnedStar.name = "Starp"+curStars.ToString();
			spawnedStar.GetComponent<MoveTo>().point = SSpawns[Random.Range(0,SSpawns.Length)].transform;
			spawnedStar.GetComponent<MoveTo>().pass = this.gameObject;
			//spawnedStar.GetComponent<MoveTo>().Move(SSpawns[Random.Range(0,SSpawns.Length+1)].transform, this.gameObject);
			curStars++;

		}

	}

}
