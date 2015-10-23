using UnityEngine;
using System.Collections;

public class lavaAnim : MonoBehaviour {

	public MovieTexture movie;

	// Use this for initialization
	void Awake() {
		movie.wrapMode = TextureWrapMode.Clamp;
		movie.loop = true;
		movie.Play ();
		movie.wrapMode = TextureWrapMode.Repeat;
		GetComponent<Renderer> ().sortingOrder = 19;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
