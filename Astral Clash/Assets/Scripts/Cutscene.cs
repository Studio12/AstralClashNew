using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cutscene : MonoBehaviour {
	
	private MovieTexture movie;
	public string NextScene;
<<<<<<< HEAD
	//	AsyncOperation async;
	
=======
//	AsyncOperation async;

>>>>>>> refs/remotes/origin/master
	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		movie = GetComponent<RawImage> ().texture as MovieTexture;
		movie.wrapMode = TextureWrapMode.Clamp;
		movie.loop = false;
		movie.Play ();
		movie.wrapMode = TextureWrapMode.Repeat;
		Debug.Log("About to start coroutine");
<<<<<<< HEAD
		//		if (NextScene != "credits" || Application.loadedLevelName != "credits") {
		//			StartCoroutine ("load");
		//		}
=======
//		if (NextScene != "credits" || Application.loadedLevelName != "credits") {
//			StartCoroutine ("load");
//		}
>>>>>>> refs/remotes/origin/master
		StartCoroutine(WaitUntilEnd());
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
		//		if (async != null) {
		//			if(async.progress == .9f){
		if (Input.GetButton ("Submit")) {
			Continue ();
		}
		//			}
		//		} else {
		//		
		//			if (Input.GetButton ("Submit")) {
		//				Continue ();
		//			}
		//
		//		}
=======
//		if (async != null) {
//			if(async.progress == .9f){
			if (Input.GetButton ("Submit")) {
				Continue ();
			}
//			}
//		} else {
//		
//			if (Input.GetButton ("Submit")) {
//				Continue ();
//			}
//
//		}
>>>>>>> refs/remotes/origin/master
	}
	
	IEnumerator WaitUntilEnd () {
		yield return new WaitForSeconds(movie.duration);
		Continue ();
	}
<<<<<<< HEAD
	
	//	IEnumerator load(){
	//
	//		Debug.Log("Load coroutine started");
	//		async = Application.LoadLevelAsync (NextScene);
	//		async.allowSceneActivation = false;
	//
	//		yield return async;
	//
	//	}
	
	void Continue () 
	{
		if (NextScene.IndexOf ("SP") == -1) {
			//			Debug.Log ("Loading next scene");
			//			if(async != null){
			//				async.allowSceneActivation = true;
			//			}else{
			GameManager.ChooseLevel (NextScene);
			//			}
=======

//	IEnumerator load(){
//
//		Debug.Log("Load coroutine started");
//		async = Application.LoadLevelAsync (NextScene);
//		async.allowSceneActivation = false;
//
//		yield return async;
//
//	}

	void Continue () 
	{
		if (NextScene.IndexOf ("SP") == -1) {
//			Debug.Log ("Loading next scene");
//			if(async != null){
//				async.allowSceneActivation = true;
//			}else{
			GameManager.ChooseLevel (NextScene);
//			}
>>>>>>> refs/remotes/origin/master
		}else {
			GameManager.curMatch.Level = NextScene;
			GameObject.Find("GameManager").GetComponent<GameManager>().CreateNewMatch(GameManager.curMatch);
		}
	}
}
