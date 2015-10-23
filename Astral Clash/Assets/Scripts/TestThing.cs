using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestThing : MonoBehaviour {
	public GameObject[] PlatArray;
	public GameObject goalPlat;
	public GameObject startPlat;
	/*public class Platform
	{
		public int number;
		public bool[] connection;
		public bool visited;

		public Platform (int Num, bool[] Con, bool Viz)
		{
			number = Num;
			connection = Con;
			visited = Viz;

		}


	}*/ 

	public Stack BFS(GameObject StartVertex, GameObject[] Platforms, GameObject goal)
	{
		Queue work = new Queue();
		//List<Queue> plans;
		//Place starting platform in line to be processed
		work.Enqueue(Platforms[StartVertex.GetComponent<PlatformIndex>().number]);
		Platforms[StartVertex.GetComponent<PlatformIndex>().number].GetComponent<PlatformIndex>().visited = true;
		while(work.Count != 0)
		{
			//print ("Current Platform: " + work.Peek ().GetComponent<PlatformIndex>().number);
			bool HasUnvisitedNode = false;
			GameObject cPlat =(GameObject) work.Peek(); 

			for(int i = 0; i < Platforms.Length; i++)
			{
				//print("Looking at Platform: " + Platforms[i].GetComponent<PlatformIndex>().number);
				if(cPlat.GetComponent<PlatformIndex>().connection[i] == true)
				{
					//print("Connected platform: " + Platforms[i].GetComponent<PlatformIndex>().number);
					if(Platforms[i].GetComponent<PlatformIndex>().visited == false)
					{
						HasUnvisitedNode = true;
						Platforms[i].GetComponent<PlatformIndex>().visited = true;
						Platforms[i].GetComponent<PlatformIndex>().parObj = cPlat;
						print (Platforms[i].GetComponent<PlatformIndex>().number);
						work.Enqueue(Platforms[i]);
						if(Platforms[i] == goal){

							break;

						}
					}


				}
			}
			if(HasUnvisitedNode == false){
				//print("Dequeueueueueuue");
				work.Dequeue();
			}
		}

		Stack path = new Stack ();
		GameObject curObj = goal;

		while (curObj != StartVertex) {
		
			path.Push(curObj);
			curObj = curObj.GetComponent<PlatformIndex>().parObj;

		}

		print ("Everything is fine now. Go to bed.");

		return path;
	}

	// Use this for initialization
	void Start () {

		//BFS (startPlat, PlatArray, goalPlat);
		Stack path = BFS (startPlat, PlatArray, goalPlat);
		while (path.Count >0) {
		
			GameObject platformthing = (GameObject)path.Pop();
			print(platformthing.GetComponent<PlatformIndex>().number);
		
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
