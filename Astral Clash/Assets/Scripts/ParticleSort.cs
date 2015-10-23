using UnityEngine;
using System.Collections;

public class ParticleSort : MonoBehaviour {

	// Use this for initialization
	void Start () {


		Invoke ("KillParts", 1.5f);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void KillParts(){

		Destroy (this.gameObject);

	}
}
