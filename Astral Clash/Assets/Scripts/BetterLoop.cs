using UnityEngine;
using System.Collections;

public class BetterLoop : MonoBehaviour {

	public AudioSource source;
	public AudioClip clip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (source.clip == clip) {
			if (source.isPlaying) {
		
				if (source.time / source.clip.length >= .8) {

					source.PlayScheduled (source.clip.length / 5);

				}
		
			}
		}
	
	}
}
