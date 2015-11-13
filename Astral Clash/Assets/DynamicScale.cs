using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DynamicScale : MonoBehaviour {

	public float minX;
	public float minY;
	public float maxX;
	public float maxY;
	public GameObject[] players;
	public Vector2 cameraBuffer = new Vector2(0,7);
	public float camSize = 2f;
	public float camSpeed = 10f;

	// Use this for initialization
	void Start () {
	
	
	}
	
	// Update is called once per frame
	void LateUpdate () {

		//ChangeScale ();
		CalculateBounds ();
		CalculateCameraPosAndSize ();


	}

	void CalculateBounds(){

		minX = Mathf.Infinity; 
		maxX = -Mathf.Infinity; 
		minY = Mathf.Infinity; 
		maxY = -Mathf.Infinity;

		players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players) {
			Vector3 tempPlayer = player.transform.position;
			//X Bounds
			if (tempPlayer.x < minX)
				minX = tempPlayer.x;
			if (tempPlayer.x > maxX)
				maxX = tempPlayer.x;
			//Y Bounds
			if (tempPlayer.y < minY)
				minY = tempPlayer.y;
			if (tempPlayer.y > maxY)
				maxY = tempPlayer.y;
			
		}
	}
		
	void CalculateCameraPosAndSize(){

		Vector3 cameraCenter = Vector3.zero;
		
		foreach(GameObject player in players){
			cameraCenter += player.transform.position;
		}
		Vector3 finalCameraCenter = new Vector3(cameraCenter.x / players.Length, cameraCenter.y/players.Length, -10);
		transform.position = finalCameraCenter;//Vector3.Lerp(transform.position, finalCameraCenter, camSpeed * Time.deltaTime);
		//Size
		float sizeX = maxX - minX + cameraBuffer.x;
		float sizeY = maxY - minY + cameraBuffer.y;
		camSize = (sizeX > sizeY ? sizeX : sizeY);
		if (camSize * .5f > 10 && camSize * .5f < 20) {
			Camera.main.orthographicSize = camSize * 0.5f;
		}else if(camSize * .5f > 20){

			Camera.main.orthographicSize = 20f;

		} else {
		
			Camera.main.orthographicSize = 10f;
		
		}

	}

}
