using UnityEngine;
using System.Collections;

public class TimedDestroy : MonoBehaviour {


	public float timer;

	// Use this for initialization
	void Start () {

		Invoke ("kill", timer);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void kill(){

		Destroy (this.gameObject);

	}
}
