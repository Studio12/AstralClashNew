using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*

 This script will be the pathfinding controller for the main AI script. 
 When fed a target, this will find the shortest path to the target, and then take the AI Object to the same platform
 as the target, and will then hand off to the battle AI.
 
*/


public class AIPathfind : MonoBehaviour
{


	public List<GameObject> PlatArray; //An array storing the platforms of the stage, and their connections to eachother, set via game manager on level load
	public GameObject target; //The target to chase, set by main AI control


	
	// On load, find provided target, just for demonstration purposes
	void Start ()
	{


		PlatArray = new List<GameObject>();

		for (int i = 0; i<GameObject.FindGameObjectsWithTag("Platform").Length; i++) {
		
			PlatArray.Add(GameObject.Find(i.ToString()));
		
		}

	}


	///BEGIN NEW PATH
	/// Takes the target, calls function to get a general path of platforms, then feeds it to the chase routine
	///
	public void BeginNewPath (GameObject FindTarget)
	{
	
		target = FindTarget;
		Stack path = BFS (this.GetComponent<Fighter> ().curPlatform, PlatArray, FindTarget.GetComponent<Fighter> ().curPlatform);
		StartCoroutine ("Chase", path);
	
	}


	///CHASE
	/// Moves object along platform path, until the final platform is reached.
	/// To Do: Pass any necessary data to battle AI on chase completion.
	IEnumerator Chase (Stack path)
	{
	

		//Local Variables
		GameObject nextPlat = (GameObject)path.Pop (); //Next platform is top of path stack

		//Platform type, relation to current position
		bool leftPlat = false; //Left?
		bool rightPlat = false; //Right?
		bool jumpPlat = false; //Needs jump?
		bool fallPlat = false; //Can fall to?

		Vector2 jumpOrigin = new Vector2 (0, 0); //Point to jump from
		Vector2 jumpDest = new Vector2 (0, 0); //Point to jump to



		//If platform is lower than the current platform by more than 2 y distance, a fall is possible
		if (this.GetComponent<Fighter> ().curPlatform.transform.position.y > nextPlat.transform.position.y + 2) {
		
			fallPlat = true;
		
		}

		//Determines if platform is left or right of current platform
		if (this.GetComponent<Fighter> ().curPlatform.transform.position.x > nextPlat.transform.position.x) {
		
			leftPlat = true;
		
		} else {
		
			rightPlat = true;
		
		}

		//If the platform needs to be jumped to, determine specifics of jump
		if (!fallPlat) {

			jumpPlat = true;

			int jType;

			//Determine what sort of jump needs to be made, from left point of platform to left point, left right, etc.
			jType = JumpType (this.GetComponent<Fighter> ().curPlatform, nextPlat);


			//Depending on jump type, set jump origin and destination accordingly based on values in platform index
			switch (jType) {

				//Left to Left
			case 1: 
				jumpOrigin = new Vector2 (this.GetComponent<Fighter> ().curPlatform.GetComponent<PlatformIndex> ().leftP.x + 3f, this.GetComponent<Fighter> ().curPlatform.GetComponent<PlatformIndex> ().leftP.y);
				jumpDest = nextPlat.GetComponent<PlatformIndex> ().leftP;
				break;

				//Left to Right
			case 2:
				jumpOrigin = new Vector2 (this.GetComponent<Fighter> ().curPlatform.GetComponent<PlatformIndex> ().leftP.x + 3f, this.GetComponent<Fighter> ().curPlatform.GetComponent<PlatformIndex> ().leftP.y);
				jumpDest = nextPlat.GetComponent<PlatformIndex> ().rightP;
				break;

				//Right to Left
			case 3:
				jumpOrigin = new Vector2 (this.GetComponent<Fighter> ().curPlatform.GetComponent<PlatformIndex> ().rightP.x - 3f, this.GetComponent<Fighter> ().curPlatform.GetComponent<PlatformIndex> ().rightP.y);
				jumpDest = nextPlat.GetComponent<PlatformIndex> ().leftP;
				break;

				//Right to Right
			case 4:
				jumpOrigin = new Vector2 (this.GetComponent<Fighter> ().curPlatform.GetComponent<PlatformIndex> ().rightP.x - 3f, this.GetComponent<Fighter> ().curPlatform.GetComponent<PlatformIndex> ().rightP.y);
				jumpDest = nextPlat.GetComponent<PlatformIndex> ().rightP;
				break;
			default:
				break;
			}

			//If the next platform is lower, but not a fall platform, adjust destination to not break math on jump
			if (jumpDest.y < jumpOrigin.y) {

				jumpDest.y += 3;

			}

		}


		//While the character is not yet at their next platform destination
		while (this.GetComponent<Fighter> ().curPlatform != nextPlat) {
		
			//AI must fall to next plat
				
			//If AI falls right, move right without passing past edge of destination platform
			if (fallPlat && rightPlat) {
				if (this.transform.position.x < nextPlat.GetComponent<PlatformIndex> ().rightP.x - 1.6f) {
					this.GetComponent<Fighter> ().direction = 1f;
				} else {
					this.GetComponent<Fighter> ().direction = -1f;
				}
			}

			//If AI falls left, move left without passing past edge of destination platform
			if (fallPlat && leftPlat) {
				if (this.transform.position.x < nextPlat.GetComponent<PlatformIndex> ().leftP.x + 1.6f) {
					this.GetComponent<Fighter> ().direction = 1f;
				} else {
					this.GetComponent<Fighter> ().direction = -1f;
				}
			}

			//AI must jump to next plat
			if (jumpPlat) {

				//While character's position is not on or very near jump origin
				while (this.transform.position.x>jumpOrigin.x+.2f || this.transform.position.x<jumpOrigin.x-.2f) {

					//Move left to jump origin
					if (this.transform.position.x > jumpOrigin.x) {

						this.GetComponent<Fighter> ().direction = -1f;

					}
					//Move right to jump origin
					else if (this.transform.position.x < jumpOrigin.x) {

						this.GetComponent<Fighter> ().direction = 1f;

					}

					//Wait for next frame to check, reduce workload
					yield return new WaitForEndOfFrame ();

				}

				jumpPlat = false; //Prevent loop from accessing this part again

				//Begin the jump to the next platform
				StartCoroutine ("jump", jumpDest);

				//Sets character to move left or right once jump is complete to make it to next plat
				if ((this.transform.position.x < nextPlat.transform.position.x)) {
					this.GetComponent<Fighter> ().direction = 1f;
				} else {
					this.GetComponent<Fighter> ().direction = -1f;
				}

			}

			//Run loop once per frame
			yield return new WaitForEndOfFrame ();
		
		}

		//Once next platform is found, if it isn't target platform, continue chase
		if (this.GetComponent<Fighter> ().curPlatform != target.GetComponent<Fighter> ().curPlatform) {
		
			StartCoroutine ("Chase", path);
		
		}

		//Stop moving on finding target's platform. This is where Battle AI will initiate.
		else {
			this.GetComponent<Fighter> ().direction = 0f;
		}
	
	}


	///JUMPTYPE
	/// Determines the position of start of jump, and end of jump, returns int based on type.
	///
	int JumpType (GameObject p1, GameObject p2)
	{
	
		//jType indicates type of jump. 1 = left to left, 2 = left to right, 3 = right to left, 4 = right to right


		//Local Variables.
		//Left edge of plat to left edge of next plat, etc.
		bool llJump = false;
		bool lrJump = false;
		bool rlJump = false;
		bool rrJump = false;

		//Takes edge points defined in platform index and shifts left or right, to ground where character can reach
		Vector2 jOriginL = new Vector2 (p1.GetComponent<PlatformIndex> ().leftP.x + 3f, p1.GetComponent<PlatformIndex> ().leftP.y);
		Vector2 jOriginR = new Vector2 (p1.GetComponent<PlatformIndex> ().rightP.x - 3f, p1.GetComponent<PlatformIndex> ().leftP.y);


		//If there are no obstacles between the left origin and the left destination
		if (!Physics2D.Linecast (jOriginL, p2.GetComponent<PlatformIndex> ().leftP)) {
		
			llJump = true;
		
		}

		//Left to Right point obstacle check
		if (!Physics2D.Linecast (jOriginL, p2.GetComponent<PlatformIndex> ().rightP)) {
		
			lrJump = true;

			//If left-left jump true, determine shorter distance, discard farther jump
			if (llJump == true) {

				if (Vector2.Distance (jOriginL, p2.GetComponent<PlatformIndex> ().leftP) > Vector2.Distance (jOriginL, p2.GetComponent<PlatformIndex> ().rightP)) {

					llJump = false;

				} else {

					lrJump = false;

				}

			}
		
		}

		//Right to Left obstacle check
		if (!Physics2D.Linecast (jOriginR, p2.GetComponent<PlatformIndex> ().leftP)) {
		
			rlJump = true;

			//Check previous jumps, determine shortest distance
			if (llJump == true) {
				
				if (Vector2.Distance (jOriginL, p2.GetComponent<PlatformIndex> ().leftP) > Vector2.Distance (jOriginR, p2.GetComponent<PlatformIndex> ().leftP)) {
					
					llJump = false;
					
				} else {
					
					rlJump = false;
					
				}
				if (lrJump == true && rlJump == true) {

					if (Vector2.Distance (jOriginL, p2.GetComponent<PlatformIndex> ().rightP) > Vector2.Distance (jOriginR, p2.GetComponent<PlatformIndex> ().leftP)) {
						
						lrJump = false;
						
					} else {
						
						rlJump = false;
						
					}
				}
			}
		
		}

		//Right Right Obstacle Check
		if (!Physics2D.Linecast (jOriginR, p2.GetComponent<PlatformIndex> ().rightP)) {
			
			rrJump = true;

			//Check previous jump types
			if (llJump == true) {
				
				if (Vector2.Distance (jOriginL, p2.GetComponent<PlatformIndex> ().leftP) > Vector2.Distance (jOriginR, p2.GetComponent<PlatformIndex> ().rightP)) {
					
					llJump = false;
					
				} else {
					
					rrJump = false;
					
				}
				if (lrJump == true && rrJump == true) {
					
					if (Vector2.Distance (jOriginL, p2.GetComponent<PlatformIndex> ().rightP) > Vector2.Distance (jOriginR, p2.GetComponent<PlatformIndex> ().rightP)) {
						
						lrJump = false;
						
					} else {
						
						rrJump = false;
						
					}
				}
				if (rlJump == true && rrJump == true) {

					if (Vector2.Distance (jOriginR, p2.GetComponent<PlatformIndex> ().leftP) > Vector2.Distance (jOriginR, p2.GetComponent<PlatformIndex> ().rightP)) {
						
						rlJump = false;
						
					} else {
						
						rrJump = false;
						
					}

				}
			}
			
		}

		//Return int based on shortest viable jump type available
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

	/// BREADTH FIRST SEARCH
	/// Uses a queue to find the shortest path of platforms to target's platform, returns stack containing this path
	///
	Stack BFS (GameObject StartVertex, List<GameObject> Platforms, GameObject goal)
	{
		//Queue of platforms to navigate through
		Queue work = new Queue ();
		work.Enqueue (Platforms [StartVertex.GetComponent<PlatformIndex> ().number]); //Starting position
		Platforms [StartVertex.GetComponent<PlatformIndex> ().number].GetComponent<PlatformIndex> ().visited = true; //Starting position has been visited in tree

		//While there is still items in queue to go through. Break condition of reaching goal.
		while (work.Count != 0) {

			bool HasUnvisitedNode = false;
			GameObject cPlat = (GameObject)work.Peek (); //Analyze current platform in queue

			//For every platform on stage
			for (int i = 0; i < Platforms.Count; i++) {

				//Does current platform connect to this platform in index?
				if (cPlat.GetComponent<PlatformIndex> ().connection [i] == true) {

					//Has this platform been checked before?
					if (Platforms [i].GetComponent<PlatformIndex> ().visited == false) {
						HasUnvisitedNode = true; //Current platform has connections
						Platforms [i].GetComponent<PlatformIndex> ().visited = true; //Connected platform has checked.
						Platforms [i].GetComponent<PlatformIndex> ().parObj = cPlat; //Make current platform a parent node of checked platform
						work.Enqueue (Platforms [i]); //Queue up connected platform as part of possible path

						//If connected platform is the goal platform, break
						if (Platforms [i] == goal) {
							break;
						}
					}
				}
			}

			//Dequeue platforms that have no unvisited connections, dead ends
			if (HasUnvisitedNode == false) {
				work.Dequeue ();
			}
		}

		//Ready stack path
		Stack path = new Stack ();
		GameObject curObj = goal;

		//Work backwards from goal to start through parented nodes
		while (curObj != StartVertex) {

			//Add all objects on parent path to stack, reversing queue
			path.Push (curObj);
			curObj = curObj.GetComponent<PlatformIndex> ().parObj;
			
		}

		foreach (GameObject p in PlatArray) {
		
			p.GetComponent<PlatformIndex>().visited = false;
		
		}

		//Final platform path is a stack.
		return path;
	}

	/// JUMP
	/// Determines forces necessary to achieve a realistic jump to target point, according to current physics
	///
	IEnumerator jump (Vector2 target)
	{
		//Local Variables
		float ydistance; //Vertical distance to target
		float xdistance; //Horizontal distance to target
		float ypower; //Up force
		float xpower; //Left/Right force
		float time; //Time to reach target

		//Disable character movement while jump is happening.
		this.GetComponent<Fighter> ().AIJump = true;

		//Set ydistance, use formula to find apex of jump for power (character is at 4 times gravity)
		ydistance = target.y - this.transform.position.y;
		ypower = Mathf.Sqrt (8 * Physics2D.gravity.y * -1 * ydistance);

		//Find time to reach apex of jump via formula, find left/right distance, set horizontal force accordingly
		time = ypower / (4 * Physics2D.gravity.y * -1);
		xdistance = target.x - this.transform.position.x;
		xpower = xdistance / time;

		//Apply velocity for the jump, wait until apex is reached, and re-enable character movement.
		this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (xpower, ypower);
		yield return new WaitForSeconds (time);
		this.GetComponent<Fighter> ().AIJump = false;
		
	}

}