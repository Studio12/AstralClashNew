using UnityEngine;
using System.Collections;

public class SoundFXControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		foreach (AudioSource a in GetComponents<AudioSource>()) {
		
			a.volume = PlayerPrefs.GetFloat("SFX Volume");
		
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
