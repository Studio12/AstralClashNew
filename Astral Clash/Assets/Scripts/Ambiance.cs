using UnityEngine;
using System.Collections;

public class Ambiance : MonoBehaviour {

	public AudioClip[] ambSounds;
	public int maxSounds;
	public float startDelay;
	public float repeatRate;

	// Use this for initialization
	void Start () {

		InvokeRepeating ("PlayAmbiance", startDelay, repeatRate);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

		void PlayAmbiance(){

		GetComponent<AudioSource> ().clip = ambSounds[Random.Range(0,maxSounds)];
			GetComponent<AudioSource> ().Play ();

		}

}
