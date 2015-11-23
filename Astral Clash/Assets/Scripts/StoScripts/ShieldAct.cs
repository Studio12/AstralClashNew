using UnityEngine;
using System.Collections;

public class ShieldAct : MonoBehaviour {
	
	
	public Fighter fighter;
	
	
	// Use this for initialization
	void Awake () {
		
		fighter = transform.parent.GetComponent<Fighter> ();
		this.GetComponent<Animator>().Play("ShieldAnim", -1, 0);
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		this.GetComponent<SpriteRenderer> ().color = new Color (fighter.shieldHealth / 30, fighter.shieldHealth / 30, fighter.shieldHealth / 30);
		print (fighter.shieldHealth / 30);
		
	}
	
	
	
	
	
}
