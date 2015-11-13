using UnityEngine;
using System.Collections;

public class ScaleElements : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.localScale = new Vector3(Camera.main.orthographicSize / 20, Camera.main.orthographicSize / 20, 1);
	
	}
}
