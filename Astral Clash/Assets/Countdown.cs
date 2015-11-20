using UnityEngine;
using System.Collections;

public class Countdown : MonoBehaviour {

	public AudioClip count1;
	public AudioClip count2;
	public AudioClip count3;
	public AudioClip countFight;


	// Use this for initialization
	void Start () {


		GetComponent<Renderer> ().sortingOrder = 20;
		StartCoroutine ("Count");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Count(){

		GetComponent<AudioSource> ().clip = count3;
		GetComponent<AudioSource> ().Play ();
		GetComponent<TextMesh> ().text = "3";
		GetComponent<TextMesh> ().characterSize = 1;
		yield return new WaitForSeconds(.75f);
		for (int i = 0; i<10; i++) {
			
			yield return new WaitForSeconds(.025f);
			GetComponent<TextMesh>().color = new Color(1,1,1, (GetComponent<TextMesh>().color.a - .15f));
			GetComponent<TextMesh> ().characterSize -= .05f;
			
			
		}


		GetComponent<TextMesh>().color = new Color(1,1,1,1);
		GetComponent<TextMesh> ().characterSize = 1;
		GetComponent<AudioSource> ().clip = count2;
		GetComponent<AudioSource> ().Play ();
		GetComponent<TextMesh> ().text = "2";
		yield return new WaitForSeconds(.75f);
		for (int i = 0; i<10; i++) {
			
			yield return new WaitForSeconds(.025f);
			GetComponent<TextMesh>().color = new Color(1,1,1, (GetComponent<TextMesh>().color.a - .15f));
			GetComponent<TextMesh> ().characterSize -= .05f;
			
			
		}



		GetComponent<TextMesh>().color = new Color(1,1,1,1);
		GetComponent<TextMesh> ().characterSize = 1;
		GetComponent<AudioSource> ().clip = count1;
		GetComponent<AudioSource> ().Play ();
		GetComponent<TextMesh> ().text = "1";
		yield return new WaitForSeconds(.75f);
		for (int i = 0; i<10; i++) {
			
			yield return new WaitForSeconds(.025f);
			GetComponent<TextMesh>().color = new Color(1,1,1, (GetComponent<TextMesh>().color.a - .15f));
			GetComponent<TextMesh> ().characterSize -= .05f;
			
			
		}



		GetComponent<TextMesh>().color = new Color(1,1,1,1);
		GetComponent<TextMesh> ().characterSize = 1;
		GetComponent<AudioSource> ().clip = countFight;
		GetComponent<AudioSource> ().Play ();
		GetComponent<TextMesh> ().text = "FIGHT!";

		yield return new WaitForSeconds(.75f);

		for (int i = 0; i<10; i++) {
		
			yield return new WaitForSeconds(.025f);
			GetComponent<TextMesh>().color = new Color(1,1,1, (GetComponent<TextMesh>().color.a - .15f));
			GetComponent<TextMesh> ().characterSize -= .05f;

		
		}

		gameObject.SetActive (false);



	}
}
