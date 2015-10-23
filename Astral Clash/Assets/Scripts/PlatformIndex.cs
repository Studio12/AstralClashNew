using UnityEngine;
using System.Collections;

public class PlatformIndex : MonoBehaviour {

	public GameObject[] platforms;
	//public float[] distances;
	public int number;
	public bool[] connection;
	public bool visited;
	public GameObject parObj;

	public Vector2 rightP;
	public Vector2 leftP;


	// Use this for initialization
	void Start () {

//		int i = 0;
//
//		foreach (GameObject p in platforms) {
//		
//			distances[i] = Vector2.Distance(this.transform.position, p.transform.position);
//			i++;
//
//		}

		//rightP = new Vector2 (this.transform.position.x + ((this.transform.localScale.x*this.GetComponent<BoxCollider2D>().size.x)/2)+1.5f, this.transform.position.y + ((this.transform.localScale.y*this.GetComponent<BoxCollider2D>().size.y)/2)+5f);
		//leftP = new Vector2 (this.transform.position.x - ((this.transform.localScale.x*this.GetComponent<BoxCollider2D>().size.x)/2)-1.5f, this.transform.position.y + ((this.transform.localScale.y*this.GetComponent<BoxCollider2D>().size.y)/2)+5f);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
