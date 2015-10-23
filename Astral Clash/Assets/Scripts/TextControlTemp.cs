using UnityEngine;
using System.Collections;

public class TextControlTemp : MonoBehaviour {

	// Use this for initialization
	void Start () {

		this.GetComponent<TextMesh> ().text = "Temp Controls:\n" +
			"Joystick: Horizontal Movement+Jump (hold up for higher jump)\n" +
			"A (Xbox)/X (Sony): Block, Menu Select\n" +
			"Block+Left/Right: Dodge\n" +
			"X (Xbox)/Square (Sony): Light Attack\n" +
			"Y (Xbox)/Triangle (Sony): Medium Attack\n" +
			"B (Xbox)/Circle (Sony): Heavy Attack, Menu Cancel\n" +
			"Start: Pause\n" +
			"Escape: Force Quit";
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
