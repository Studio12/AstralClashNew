﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DynamicScale : MonoBehaviour {
	
	public float minX;
	public float minY;
	public float maxX;
	public float maxY;
	public float minBoundsX;
	public float maxBoundsX;
	public float minBoundsY;
	public float maxBoundsY;
	public Actor[] players;
	public Vector2 cameraBuffer = new Vector2(0,7);
	public float camSize = 2f;
	public float camSpeed = 10f;
	public float camMinSize;
	public float camMaxSize;
	public bool shaking;

	// Use this for initialization
	void Start () {

		shaking = false;

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
		
		players = FindObjectsOfType (typeof(Actor)) as Actor[];
		foreach (Actor player in players) {
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
		
		foreach(Actor player in players){
			cameraCenter += player.transform.position;
		}
		Vector3 finalCameraCenter = new Vector3(cameraCenter.x / players.Length, cameraCenter.y/players.Length, -10);
		if(!float.IsNaN(finalCameraCenter.x) && !float.IsNaN(finalCameraCenter.y)  && !shaking) transform.position = new Vector3 (Mathf.Clamp (transform.position.x - (transform.position.x - finalCameraCenter.x) * (1/camSpeed), minBoundsX + (CalculateScreenSizeInWorldCoords().x / 2), maxBoundsX - (CalculateScreenSizeInWorldCoords().x / 2)), Mathf.Clamp (transform.position.y - (transform.position.y - finalCameraCenter.y) * (1/camSpeed), minBoundsY + (CalculateScreenSizeInWorldCoords().y / 2), maxBoundsY - (CalculateScreenSizeInWorldCoords().y / 2)),transform.position.z);
		
		//Size
		float sizeX = maxX - minX + cameraBuffer.x;
		float sizeY = maxY - minY + cameraBuffer.y;
		camSize = (sizeX > sizeY ? sizeX : sizeY);
		Camera.main.orthographicSize -= (Camera.main.orthographicSize - Mathf.Clamp(camSize * 0.5f,camMinSize,camMaxSize)) * (1/camSpeed);
		
	}
	
	Vector2 CalculateScreenSizeInWorldCoords () {
		Vector3 p1 = Camera.main.ViewportToWorldPoint(new Vector3(0,0,Camera.main.nearClipPlane));  
		Vector3 p2 = Camera.main.ViewportToWorldPoint(new Vector3(1,0,Camera.main.nearClipPlane));
		Vector3 p3 = Camera.main.ViewportToWorldPoint(new Vector3(1,1,Camera.main.nearClipPlane));
		
		float width = (p2 - p1).magnitude;
		float height = (p3 - p2).magnitude;
		
		Vector2 dimensions = new Vector2(width,height);
		
		return dimensions;
	}
	
}
