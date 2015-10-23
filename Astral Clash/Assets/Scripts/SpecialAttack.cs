using UnityEngine;
using System.Collections;

public class SpecialAttack : MonoBehaviour
{


	public GameObject Bull;
	private GameObject BullObj;
	public GameObject smoke;
	public GameObject knife;

	void Start ()
	{
	
		AudioListener.volume = .5f;
	
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
			break;
		case "Leo":
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
		BullObj = (GameObject)Instantiate (Bull, new Vector2 (-32, ypos1), this.transform.rotation);
		BullObj.GetComponent<BullScript> ().activator = player;
		yield return new WaitForSeconds (1f);
		for (int i = 0; i < 6; i++) {
			while(ypos1-ypos2<7 && ypos1-ypos2>-7 || loopPrevention>50){
				ypos1 = Random.Range (-16, 17);
				loopPrevention++;
			}
			loopPrevention = 0;
			BullObj = (GameObject)Instantiate (Bull, new Vector2 (-32, ypos1), this.transform.rotation);
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

		for (int i=0; i<80; i++) {
		
			knifeObj = (GameObject)Instantiate(knife, player.transform.position, Quaternion.Euler(0,0, Random.Range(-60,61)));
			knifeObj.GetComponent<Knife>().activator = player;
			yield return new WaitForSeconds (.05f);
		
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
















}