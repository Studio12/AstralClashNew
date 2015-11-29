using UnityEngine;
using System.Collections;

public class MusicVolume : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat ("Music Volume");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
