using UnityEngine;
using System.Collections;

public class pIconCorrect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.parent.GetComponent<Fighter> ().facing == -1) {
		
			this.transform.localRotation = Quaternion.Euler(0,180,0);
		
		} else {
		
			this.transform.localRotation = Quaternion.Euler(0,0,0);
		
		}
	
	}
}
