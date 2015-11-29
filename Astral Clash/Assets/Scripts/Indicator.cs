using UnityEngine;
using System.Collections;

public class Indicator : MonoBehaviour {

	public GameObject indicated;
	public GameObject box;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (indicated != null) {
			RaycastHit2D ray = Physics2D.Linecast (indicated.transform.position, box.transform.position, gameObject.layer);
			this.transform.position = ray.point;
			//transform.LookAt(indicated.transform.position);
			Vector3 dir = indicated.transform.position - transform.position;
			float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			//Quaternion rotation = Quaternion.LookRotation (indicated.transform.position - transform.position, transform.TransformDirection (Vector3.up));
			//transform.rotation = new Quaternion (0, 0, rotation.z, rotation.w);
		} else {
		
			Destroy(this.gameObject);
		
		}
	
	}
}
