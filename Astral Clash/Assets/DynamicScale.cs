using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DynamicScale : MonoBehaviour {

	public class distAngles
	{
	
		public float distance;
		//public float angle;
		public float ydist;
		public float xdist;
		public Vector3 midpoint;
		//public GameObject a;
		//public GameObject b;
	
	}

	public float minX;
	public float minY;
	public float maxX;
	public float maxY;
	public GameObject[] players;
	public Vector2 cameraBuffer = new Vector2(0,7);
	public float camSize = 2f;
	public float camSpeed = 10f;

	private Round round;

	// Use this for initialization
	void Start () {

		round = GameObject.Find ("RoundControls").GetComponent<Round> ();
	
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

//	void ChangeScale(){
//
//		List<distAngles> SortedList = new List<distAngles>();
//
//		foreach (GameObject a in round.Players) {
//			
//			foreach(GameObject b in round.Players){
//				
//				distAngles temp = new distAngles();
//				temp.distance = Vector2.Distance(a.transform.position, b.transform.position);
//				//temp.angle = Mathf.Sin(Vector2.Angle(a.transform.forward, b.transform.forward));
//				temp.ydist = Mathf.Abs(a.transform.position.y-b.transform.position.y);
//				temp.xdist = Mathf.Abs(a.transform.position.x-b.transform.position.x);
//				//temp.a = a;
//				//temp.b = b;
//				temp.midpoint = new Vector3(a.transform.position.x+(b.transform.position.x-a.transform.position.x)/2, a.transform.position.y+(b.transform.position.y-a.transform.position.y)/2, -10);
//				
//				SortedList.Add(temp);
//				
//			}
//			
//		}
//		
//		SortedList = SortedList.OrderBy (x => x.distance).ToList();
//
//		//Mathf.Abs (SortedList [SortedList.Count - 1].distance * SortedList [SortedList.Count - 1].angle);
//
//		Camera.main.transform.position = SortedList [SortedList.Count - 1].midpoint;
//		Camera.main.orthographicSize = SortedList [SortedList.Count - 1].distance*.3f+3;
//
////		if (SortedList [SortedList.Count - 1].xdist > SortedList [SortedList.Count - 1].ydist) {
////		
////			Camera.main.orthographicSize = SortedList [SortedList.Count - 1].xdist*.293f+2;
////			print("using xdist");
////		
////		} else {
////		
////			Camera.main.orthographicSize = SortedList [SortedList.Count - 1].ydist;
////			print("using ydist");
////		
////		}
//
////		if (SortedList [SortedList.Count - 1].a.GetComponentInChildren<SpriteRenderer> ().isVisible) {
////		
////			Camera.main.orthographicSize = Camera.main.orthographicSize-.1f;
////		
////		} else {
////		
////			Camera.main.orthographicSize = Camera.main.orthographicSize+.1f;
////		
////		}

	//}

}
