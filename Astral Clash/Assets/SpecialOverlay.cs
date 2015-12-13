using UnityEngine;
using System.Collections;

public class SpecialOverlay : MonoBehaviour {

	public string charType;

	// Use this for initialization
	void Start () {

		this.transform.SetParent (Camera.main.transform);
		this.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y,-33.8f);

		Time.timeScale = .1f;
		switch (charType) {
		
		case "Taurus":
			GetComponent<Animator>().Play("TaurusSpOverlay", -1, 0f);
			break;

		case "Leo":
			GetComponent<Animator>().Play("LeoSpOverlay", -1, 0f);
			break;

		case "Scorpio":
			GetComponent<Animator>().Play("ScorpSpOverlay", -1, 0f);
			break;

		case "Aquarius":
			GetComponent<Animator>().Play("AquaSpOverlay", -1, 0f);
			break;

		default:
			break;
		
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Killself(){

		Time.timeScale = 1f;
		Destroy (this.gameObject);
			
	}
}
