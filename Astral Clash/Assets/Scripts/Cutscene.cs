using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cutscene : MonoBehaviour {

	private MovieTexture movie;
	public string NextScene;

	// Use this for initialization
	void Start () {
		movie = GetComponent<RawImage> ().texture as MovieTexture;
		movie.wrapMode = TextureWrapMode.Clamp;
		movie.loop = false;
		movie.Play ();
		movie.wrapMode = TextureWrapMode.Repeat;
		StartCoroutine(WaitUntilEnd());
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Submit"))
			Continue ();
	}

	IEnumerator WaitUntilEnd () {
		yield return new WaitForSeconds(movie.duration);
		Continue ();
	}

	void Continue () 
	{
		Application.LoadLevel (NextScene);
	}
}
