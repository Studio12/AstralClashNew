﻿using UnityEngine;
using System.Collections;

public class SpecialAttack : MonoBehaviour
{


	public GameObject Bull;
	private GameObject BullObj;
	public GameObject smoke;
	public GameObject knife;
	public GameObject Flame;
	public GameObject Water;

	public AudioClip[] SoundArray;

	public AudioSource source;

	void Start ()
	{

		source = GetComponent<AudioSource> ();
	
	}

	void Update ()
	{
		
	}

	public void Special (string type, GameObject player)
	{
		switch (type) {
		case "Taurus":
			StartCoroutine ("TSpecial", player);
			break;
		case "Scorpio":
			StartCoroutine ("SSpecial", player);
			break;
		case "Aquarius":
			StartCoroutine("ASpecial", player);
			break;
		case "Leo":
			StartCoroutine("LSpecial", player);
			break;
		default:
			break;
		}
	}

	IEnumerator TSpecial (GameObject player)
	{
		int ypos1 = Random.Range (-16, 17);
		int ypos2 = ypos1;
		int loopPrevention = 0;
		BullObj = (GameObject)Instantiate (Bull, new Vector2 (-60, ypos1), this.transform.rotation);
		source.clip = SoundArray [0];
		source.Play ();

		BullObj.GetComponent<BullScript> ().activator = player;
		yield return new WaitForSeconds (1f);
		for (int i = 0; i < 6; i++) {
			while(ypos1-ypos2<7 && ypos1-ypos2>-7 || loopPrevention>50){
				ypos1 = Random.Range (-16, 17);
				loopPrevention++;
			}
			loopPrevention = 0;
			BullObj = (GameObject)Instantiate (Bull, new Vector2 (-60, ypos1), this.transform.rotation);
			BullObj.GetComponent<BullScript> ().activator = player;
			ypos2 = ypos1;
			yield return new WaitForSeconds (1f);
		}
	}

	IEnumerator SSpecial (GameObject player)
	{
		Vector2 origPos = player.transform.position;
		player.gameObject.layer = LayerMask.NameToLayer("Special");
		GameObject knifeObj;
		yield return new WaitForSeconds (.4f);
		Instantiate (smoke, player.transform.position, player.transform.rotation);
		yield return new WaitForSeconds (.05f);
		Instantiate (smoke, new Vector2 (0, 18), player.transform.rotation);
		yield return new WaitForSeconds (.05f);
		player.transform.position = new Vector2 (0, 18);
		player.GetComponent<Rigidbody2D> ().isKinematic = true;
		int rotation = Random.Range (-60, 61);
		int increment = Random.Range(-2,2);
		if (increment < 0) {
		
			increment = -15;
		
		} else {
		
			increment = 15;
		
		}

		source.clip = SoundArray [1];
		source.loop = true;
		source.Play ();

		for (int i=0; i<60; i++) {
		
			knifeObj = (GameObject)Instantiate(knife, player.transform.position, Quaternion.Euler(0,0, rotation));
			knifeObj.GetComponent<Knife>().activator = player;
			rotation+=increment;
			if(rotation>=60){

				increment = -15;

			}else if(rotation<=-60){

				increment = 15;

			}
			yield return new WaitForSeconds (.05f);
		
		}

		source.loop = false;
		if (source.isPlaying) {
		
			source.Stop();
		
		}

		player.GetComponentInChildren<Animator> ().SetTrigger ("Special2");
		Instantiate (smoke, player.transform.position, player.transform.rotation);
		yield return new WaitForSeconds (.05f);
		Instantiate (smoke, origPos, player.transform.rotation);
		yield return new WaitForSeconds (.05f);
		player.GetComponent<Rigidbody2D> ().isKinematic = false;
		player.transform.position = origPos;
		player.gameObject.layer = LayerMask.NameToLayer("Player");

	}


	IEnumerator ASpecial(GameObject player){

		GameObject WaterObj = (GameObject)Instantiate (Water, new Vector2 (0, -20), this.transform.rotation);
		WaterObj.GetComponent<AquaSpecial> ().activator = player;
		yield return new WaitForEndOfFrame();

	}


	IEnumerator LSpecial(GameObject player){

		GameObject FlameObj = (GameObject)Instantiate (Flame, player.transform.position, player.transform.rotation);
		FlameObj.GetComponent<LeoProj> ().activator = player;

		yield return new WaitForEndOfFrame();

	}


}