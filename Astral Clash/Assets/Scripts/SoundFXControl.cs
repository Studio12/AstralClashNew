using UnityEngine;
using System.Collections;

public class SoundFXControl : MonoBehaviour {

	public float multiplier = 1f;

	// Use this for initialization
	void Start () {
	
		foreach (AudioSource a in GetComponents<AudioSource>()) {
		
			a.volume = PlayerPrefs.GetFloat("SFX Volume")*multiplier;
		
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
