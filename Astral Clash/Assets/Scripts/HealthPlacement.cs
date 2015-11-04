using UnityEngine;
using System.Collections;

public class HealthPlacement : MonoBehaviour {

	public float players;
	public float correction = -3;
	public Round roundC;

	// Use this for initialization
	void Start () {

		this.transform.parent = Camera.main.gameObject.transform;
		//players = GameManager.curMatch.maxPlayers+1;
		roundC = GameObject.Find ("RoundControls").GetComponent<Round> ();
	
	}

	void Place ()
	{

		this.transform.localScale = new Vector3(Camera.main.orthographicSize / 10, Camera.main.orthographicSize / 10, 1);
		GameObject[] healthbars = GameObject.FindGameObjectsWithTag ("HealthBar");
		players = healthbars.Length + 1;
		float screenwidth = Camera.main.orthographicSize*2*(16f/9f);
		float screenheight = Camera.main.orthographicSize;
		float incrementx = screenwidth/players;
		float incrementy = screenheight / -5;
		float startx = (screenwidth/2) * (-1);
		float starty = (screenheight);


		for(int i = 0; i<healthbars.Length;i++){

			healthbars[i].transform.localPosition = new Vector3 (startx + (incrementx*(i+1))+correction, starty + incrementy, 1);

		}

//		switch (this.name) {
//		case "HealthBar1":
//			this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, new Vector3 (startx + incrementx+correction, starty + incrementy, 1), 5);
//			break;
//		case "HealthBar2":
//			this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, new Vector3 (startx + (incrementx*2)+correction, starty + incrementy, 1), 5);
//			break;
//		case "HealthBar3":
//			this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, new Vector3 (startx + incrementx*3+correction, starty + incrementy, 1), 5);
//			break;
//		case "HealthBar4":
//			this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, new Vector3 (startx + incrementx*4+correction, starty + incrementy, 1), 5);
//			break;
//		default:
//			break;
//		}
	}
	
	// Update is called once per frame
	void LateUpdate () {

		Place ();
	
	}
}
