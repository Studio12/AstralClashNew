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

		if (Application.loadedLevelName.IndexOf ("SP") == -1) {
			GetComponent<AudioSource> ().clip = count3;
			GetComponent<AudioSource> ().Play ();
			GetComponent<TextMesh> ().text = "3";
			GetComponent<TextMesh> ().characterSize = 1;
			yield return new WaitForSeconds (.75f);
			for (int i = 0; i<10; i++) {
			
				yield return new WaitForSeconds (.025f);
				GetComponent<TextMesh> ().color = new Color (1, 1, 1, (GetComponent<TextMesh> ().color.a - .15f));
				GetComponent<TextMesh> ().characterSize -= .05f;
			
			
			}


			GetComponent<TextMesh> ().color = new Color (1, 1, 1, 1);
			GetComponent<TextMesh> ().characterSize = 1;
			GetComponent<AudioSource> ().clip = count2;
			GetComponent<AudioSource> ().Play ();
			GetComponent<TextMesh> ().text = "2";
			yield return new WaitForSeconds (.75f);
			for (int i = 0; i<10; i++) {
			
				yield return new WaitForSeconds (.025f);
				GetComponent<TextMesh> ().color = new Color (1, 1, 1, (GetComponent<TextMesh> ().color.a - .15f));
				GetComponent<TextMesh> ().characterSize -= .05f;
			
			
			}



			GetComponent<TextMesh> ().color = new Color (1, 1, 1, 1);
			GetComponent<TextMesh> ().characterSize = 1;
			GetComponent<AudioSource> ().clip = count1;
			GetComponent<AudioSource> ().Play ();
			GetComponent<TextMesh> ().text = "1";
			yield return new WaitForSeconds (.75f);
			for (int i = 0; i<10; i++) {
			
				yield return new WaitForSeconds (.025f);
				GetComponent<TextMesh> ().color = new Color (1, 1, 1, (GetComponent<TextMesh> ().color.a - .15f));
				GetComponent<TextMesh> ().characterSize -= .05f;
			
			
			}



			GetComponent<TextMesh> ().color = new Color (1, 1, 1, 1);
			GetComponent<TextMesh> ().characterSize = 1;
			GetComponent<AudioSource> ().clip = countFight;
			GetComponent<AudioSource> ().Play ();
			GetComponent<TextMesh> ().text = "FIGHT!";

			yield return new WaitForSeconds (.75f);

			for (int i = 0; i<10; i++) {
		
				yield return new WaitForSeconds (.025f);
				GetComponent<TextMesh> ().color = new Color (1, 1, 1, (GetComponent<TextMesh> ().color.a - .15f));
				GetComponent<TextMesh> ().characterSize -= .05f;

		
			}
		} else {
			GetComponent<TextMesh> ().color = new Color (1, 1, 1, 1);
			GetComponent<TextMesh> ().characterSize = 0.2f;
			string[] tutText = new string[] {"Be a hero! Fight the bugs!","Use dodge and block to win!","The threat of the\nComet Bugs grows larger still!\nKeep pushing!","You now have three stars!\nUse a special attack\non the bugs with RT"};
			GetComponent<TextMesh> ().text = tutText[GameManager.roundNum];
			
			yield return new WaitForSeconds (3f);
			
			for (int i = 0; i<10; i++) {
				
				yield return new WaitForSeconds (.025f);
				GetComponent<TextMesh> ().color = new Color (1, 1, 1, (GetComponent<TextMesh> ().color.a - .15f));
				GetComponent<TextMesh> ().characterSize -= .05f;
				
				
			}
		}

		gameObject.SetActive (false);



	}
}
