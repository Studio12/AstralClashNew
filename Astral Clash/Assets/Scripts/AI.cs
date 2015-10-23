using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

	Fighter fighter;
	public float distance;
	public float targetDist;
	public Transform target;

	// Use this for initialization
	void Start () {
		fighter = GetComponent<Fighter>();
	}
	
	// Update is called once per frame
	void Update () {
		distance = Vector2.Distance (this.transform.position, target.transform.position);
		if (distance > targetDist) {
			fighter.direction = (target.position - transform.position - (1.25f * transform.right)).normalized.x;
		} else {
		
			fighter.direction = 0;
		
		}

		if(Physics2D.Raycast(transform.position,transform.right, 7f))
		{
			if(distance>3 && distance<7) {
				fighter.LightAttack();
			}else if(distance<=3){

				fighter.MediumAttack();
			}else{

				fighter.HeavyAttack();

			}
		}
	}
}
