using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPathfind : MonoBehaviour
{
	public GameObject[] PlatArray;
	
	public GameObject target;
	public Vector2 targetHeight;
	public float ydistance;
	public float xdistance;
	public float ypower;
	public float xpower;
	public float time;
	
	// Use this for initialization
	void Start ()
	{
		

		Invoke ("dumbTempThing", 1); 
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

		void dumbTempThing(){

		print ("Intermediary started");
			BeginNewPath (target);

		}

	public void BeginNewPath(GameObject FindTarget){
	
		print ("Beginning to find new path");
		Stack path = BFS (this.GetComponent<Fighter>().curPlatform, PlatArray, FindTarget.GetComponent<Fighter>().curPlatform);
		print ("Found path, beginning chase");
		StartCoroutine ("Chase", path);
	
	}

	IEnumerator Chase(Stack path){
	
		print ("Chase started");
		GameObject nextPlat = (GameObject)path.Pop ();
		bool leftPlat = false;
		bool rightPlat = false;
		bool jumpPlat = false;
		bool fallPlat = false;
		
		Vector2 jumpOrigin = new Vector2(0,0);
		Vector2 jumpDest = new Vector2(0,0);

		print ("Target platform: " + nextPlat.name);

		if (this.GetComponent<Fighter> ().curPlatform.transform.position.y > nextPlat.transform.position.y + 2) {
		
			print("FALLPLAT TRUE");
			fallPlat = true;
		
		}
		if (this.GetComponent<Fighter> ().curPlatform.transform.position.x > nextPlat.transform.position.x) {
		
			print("LEFTPLAT TRUE");
			leftPlat = true;
		
		} else {
		
			print("RIGHTPLAT TRUE");
			rightPlat = true;
		
		}
		if(!fallPlat){

			jumpPlat=true;

			int jType;

			jType = JumpType(this.GetComponent<Fighter>().curPlatform, nextPlat);


			switch(jType){

			case 1:
				jumpOrigin = new Vector2(this.GetComponent<Fighter>().curPlatform.GetComponent<PlatformIndex>().leftP.x+3f, this.GetComponent<Fighter>().curPlatform.GetComponent<PlatformIndex>().leftP.y);
				jumpDest = nextPlat.GetComponent<PlatformIndex>().leftP;
				break;
			case 2:
				jumpOrigin = new Vector2(this.GetComponent<Fighter>().curPlatform.GetComponent<PlatformIndex>().leftP.x+3f, this.GetComponent<Fighter>().curPlatform.GetComponent<PlatformIndex>().leftP.y);
				jumpDest = nextPlat.GetComponent<PlatformIndex>().rightP;
				break;
			case 3:
				jumpOrigin = new Vector2(this.GetComponent<Fighter>().curPlatform.GetComponent<PlatformIndex>().rightP.x-3f, this.GetComponent<Fighter>().curPlatform.GetComponent<PlatformIndex>().rightP.y);
				jumpDest = nextPlat.GetComponent<PlatformIndex>().leftP;
				break;
			case 4:
				jumpOrigin = new Vector2(this.GetComponent<Fighter>().curPlatform.GetComponent<PlatformIndex>().rightP.x-3f, this.GetComponent<Fighter>().curPlatform.GetComponent<PlatformIndex>().rightP.y);
				jumpDest = nextPlat.GetComponent<PlatformIndex>().rightP;
				break;
			default:
				break;
			}
			if(jumpDest.y<jumpOrigin.y){

				jumpDest.y+=3;

			}

		}

		while (this.GetComponent<Fighter> ().curPlatform != nextPlat) {
		
			//AI MUST FALL
				
			//IF AI MUST FALL RIGHT
			if(fallPlat && rightPlat){
				if(this.transform.position.x<nextPlat.GetComponent<PlatformIndex>().rightP.x-1.6f){
					this.GetComponent<Fighter>().direction = 1f;
				}else{
					this.GetComponent<Fighter>().direction = -1f;
				}
			}

			//IF AI MUST FALL LEFT
			if(fallPlat && leftPlat){
				if(this.transform.position.x<nextPlat.GetComponent<PlatformIndex>().leftP.x+1.6f){
					this.GetComponent<Fighter>().direction = 1f;
				}else{
					this.GetComponent<Fighter>().direction = -1f;
				}
			}

			//AI MUST JUMP
			if(jumpPlat){

				while(this.transform.position.x>jumpOrigin.x+.2f || this.transform.position.x<jumpOrigin.x-.2f){

					if(this.transform.position.x>jumpOrigin.x){

						this.GetComponent<Fighter>().direction = -1f;

					}else if(this.transform.position.x<jumpOrigin.x){

						this.GetComponent<Fighter>().direction = 1f;

					}

						yield return new WaitForEndOfFrame ();
				}

				jumpPlat = false;
				StartCoroutine("jump", jumpDest);
				if((this.transform.position.x<nextPlat.transform.position.x)){this.GetComponent<Fighter>().direction = 1f;}
				else{this.GetComponent<Fighter>().direction = -1f;}

			}
			yield return new WaitForEndOfFrame ();
		
		}

		if (this.GetComponent<Fighter> ().curPlatform != target.GetComponent<Fighter> ().curPlatform) {
		
			StartCoroutine ("Chase", path);
		
		} else {
			this.GetComponent<Fighter> ().direction = 0f;
		}
	
	}

	int JumpType(GameObject p1, GameObject p2){
	
		//jType indicates type of jump. 1 = left to left, 2 = left to right, 3 = right to left, 4 = right to right

		bool llJump = false;
		bool lrJump = false;
		bool rlJump = false;
		bool rrJump = false;
		Vector2 jOriginL = new Vector2 (p1.GetComponent<PlatformIndex> ().leftP.x + 3f, p1.GetComponent<PlatformIndex> ().leftP.y);
		Vector2 jOriginR = new Vector2 (p1.GetComponent<PlatformIndex> ().rightP.x - 3f, p1.GetComponent<PlatformIndex> ().leftP.y);

		if (!Physics2D.Linecast (jOriginL, p2.GetComponent<PlatformIndex> ().leftP)) {
		
			print("Left left jump possible");
			llJump =true;
		
		}
		if (!Physics2D.Linecast (jOriginL, p2.GetComponent<PlatformIndex> ().rightP)) {
		
			lrJump = true;
			if(llJump==true){

				if(Vector2.Distance(jOriginL, p2.GetComponent<PlatformIndex> ().leftP)>Vector2.Distance(jOriginL, p2.GetComponent<PlatformIndex> ().rightP)){

					llJump=false;

				}else{

					lrJump=false;

				}

			}
		
		}
		if (!Physics2D.Linecast (jOriginR, p2.GetComponent<PlatformIndex> ().leftP)) {
		
			rlJump = true;
			if(llJump==true){
				
				if(Vector2.Distance(jOriginL, p2.GetComponent<PlatformIndex> ().leftP)>Vector2.Distance(jOriginR, p2.GetComponent<PlatformIndex> ().leftP)){
					
					llJump=false;
					
				}else{
					
					rlJump=false;
					
				}
			if(lrJump==true && rlJump==true){

					if(Vector2.Distance(jOriginL, p2.GetComponent<PlatformIndex> ().rightP)>Vector2.Distance(jOriginR, p2.GetComponent<PlatformIndex> ().leftP)){
						
						lrJump=false;
						
					}else{
						
						rlJump=false;
						
					}
				}
			}
		
		}
		if (!Physics2D.Linecast (jOriginR, p2.GetComponent<PlatformIndex> ().rightP)) {
			
			rrJump = true;
			if(llJump==true){
				
				if(Vector2.Distance(jOriginL, p2.GetComponent<PlatformIndex> ().leftP)>Vector2.Distance(jOriginR, p2.GetComponent<PlatformIndex> ().rightP)){
					
					llJump=false;
					
				}else{
					
					rrJump=false;
					
				}
				if(lrJump==true && rrJump==true){
					
					if(Vector2.Distance(jOriginL, p2.GetComponent<PlatformIndex> ().rightP)>Vector2.Distance(jOriginR, p2.GetComponent<PlatformIndex> ().rightP)){
						
						lrJump=false;
						
					}else{
						
						rrJump=false;
						
					}
				}
				if(rlJump ==true && rrJump==true){

					if(Vector2.Distance(jOriginR, p2.GetComponent<PlatformIndex> ().leftP)>Vector2.Distance(jOriginR, p2.GetComponent<PlatformIndex> ().rightP)){
						
						rlJump=false;
						
					}else{
						
						rrJump=false;
						
					}

				}
			}
			
		}

		if (llJump == true) {
			return 1;
		} else if (lrJump == true) {
			return 2;
		} else if (rlJump == true) {
			return 3;
		} else if (rrJump == true) {
			return 4;
		} else {
			return 0;
		}

	
	}


	Stack BFS (GameObject StartVertex, GameObject[] Platforms, GameObject goal)
	{
		Queue work = new Queue ();
		work.Enqueue (Platforms [StartVertex.GetComponent<PlatformIndex> ().number]);
		Platforms [StartVertex.GetComponent<PlatformIndex> ().number].GetComponent<PlatformIndex> ().visited = true;
		while (work.Count != 0) {
			bool HasUnvisitedNode = false;
			GameObject cPlat = (GameObject)work.Peek (); 
			
			for (int i = 0; i < Platforms.Length; i++) {
				if (cPlat.GetComponent<PlatformIndex> ().connection [i] == true) {
					if (Platforms [i].GetComponent<PlatformIndex> ().visited == false) {
						HasUnvisitedNode = true;
						Platforms [i].GetComponent<PlatformIndex> ().visited = true;
						Platforms [i].GetComponent<PlatformIndex> ().parObj = cPlat;
						work.Enqueue (Platforms [i]);
						if (Platforms [i] == goal) {
							break;
						}
					}
				}
			}
			if (HasUnvisitedNode == false) {
				work.Dequeue ();
			}
		}
		
		Stack path = new Stack ();
		GameObject curObj = goal;
		
		while (curObj != StartVertex) {
			
			path.Push (curObj);
			curObj = curObj.GetComponent<PlatformIndex> ().parObj;
			
		}
		
		return path;
	}

	IEnumerator jump(Vector2 target){
		
		this.GetComponent<Fighter> ().AIJump = true;
		ydistance = target.y - this.transform.position.y;
		ypower = Mathf.Sqrt(8* Physics2D.gravity.y*-1 * ydistance);
		time = ypower / (4 * Physics2D.gravity.y*-1);
		xdistance = target.x - this.transform.position.x;
		xpower = xdistance / time;
		this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (xpower, ypower);
		yield return new WaitForSeconds(time);
		this.GetComponent<Fighter> ().AIJump = false;
		
	}

}