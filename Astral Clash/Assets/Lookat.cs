using UnityEngine;
using System.Collections;

public class Lookat : MonoBehaviour {

	public GameObject killbox;

	// Use this for initialization
	void Start () {

		if (killbox.transform.position.y > this.transform.position.y) {
		
			this.transform.rotation = Quaternion.Euler(0,0,90);
		
		}else if (killbox.transform.position.y < this.transform.position.y) {
			
			this.transform.rotation = Quaternion.Euler(0,0,270);
			
		}
		if (this.transform.position.x < -20) {
			
			this.transform.rotation = Quaternion.Euler (0, 0, 0);
			
		} else if (this.transform.position.x > 20) {
		
			this.transform.rotation = Quaternion.Euler (0, 0, 180);
		
		}

//		Vector3 dir = killbox.transform.position - transform.position;
//		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
//		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}
}
