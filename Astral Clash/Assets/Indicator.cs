using UnityEngine;
using System.Collections;

public class Indicator : MonoBehaviour {

	public GameObject indicated;
	public GameObject box;
	public LayerMask layer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (indicated != null) {
			RaycastHit2D ray = Physics2D.Linecast (indicated.transform.position, box.transform.position, layer);
			this.transform.position = ray.point;
			Quaternion rotation = Quaternion.LookRotation (indicated.transform.position - transform.position, transform.TransformDirection (Vector3.up));
			transform.rotation = new Quaternion (0, 0, rotation.z, rotation.w);
		} else {
		
			Destroy(this.gameObject);
		
		}
	
	}
}
