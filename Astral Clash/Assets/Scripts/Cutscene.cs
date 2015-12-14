using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cutscene : MonoBehaviour {

	private MovieTexture movie;
	public string NextScene;
	AsyncOperation async;

	// Use this for initialization
	void Start () {
		movie = GetComponent<RawImage> ().texture as MovieTexture;
		movie.wrapMode = TextureWrapMode.Clamp;
		movie.loop = false;
		movie.Play ();
		movie.wrapMode = TextureWrapMode.Repeat;
		StartCoroutine ("load");
		StartCoroutine(WaitUntilEnd());
	}
	
	// Update is called once per frame
	void Update () {
		if (async != null && async.progress == .9f) {
			if (Input.GetButton ("Submit")){
				Continue ();
			}
		}
	}

	IEnumerator WaitUntilEnd () {
		yield return new WaitForSeconds(movie.duration);
		Continue ();
	}

	IEnumerator load(){

		async = Application.LoadLevelAsync (NextScene);
		async.allowSceneActivation = false;
		yield return async;

	}

	void Continue () 
	{
		if (NextScene.IndexOf ("SP") == -1) {
			Debug.Log ("Loading next scene");
			async.allowSceneActivation = true;
			//GameManager.ChooseLevel (NextScene);
		}else {
			GameManager.curMatch.Level = NextScene;
			GameObject.Find("GameManager").GetComponent<GameManager>().CreateNewMatch(GameManager.curMatch);
		}
	}
}
