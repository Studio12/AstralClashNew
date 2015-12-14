using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour {
	Fighter fighter;
	// Use this for initialization
	void Start () {
		fighter = GetComponent<Fighter>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!fighter.isDead) {
			fighter.direction = Input.GetAxis ("Horizontal" + fighter.playID);
			fighter.jumping = Input.GetButton ("Jump" + fighter.playID);
			if (Input.GetButton ("Block" + fighter.playID) && fighter.attacking == false) {
				fighter.blocking = true;
			}
			if (Input.GetButton ("Block" + fighter.playID) == false) {
				fighter.blocking = false;
				if (Input.GetButtonDown ("Light Attack" + fighter.playID))
					fighter.LightAttack ();
				if (Input.GetButtonDown ("Medium Attack" + fighter.playID))
					fighter.MediumAttack ();
				if (Input.GetButtonDown ("Heavy Attack" + fighter.playID))
					fighter.HeavyAttack ();
				if (Input.GetButtonDown ("Special" + fighter.playID))
					fighter.SpecialAttack ();
			}
		}
	}
}