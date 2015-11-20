using UnityEngine;
using System.Collections;

public class ParticleSort : MonoBehaviour {

	// Use this for initialization
	void Start () {

		//this.GetComponent<ParticleSystem> ().GetComponent<Renderer> ().sortingOrder = 20;

		Invoke ("KillParts", 1.5f);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void KillParts(){

		Destroy (this.gameObject);

	}
}
