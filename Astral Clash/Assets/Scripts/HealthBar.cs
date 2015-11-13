using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public float maxValue;
	public float curValue;
	public float scaleMax;
	public Fighter character;
	public float propScale;
	public float gradScaleSpeed;

	// Use this for initialization
	void Start () {
	
		maxValue = character.maxHealth;
		curValue = character.health;
		scaleMax = this.transform.localScale.x;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (curValue<=0) {
		
			if (this.name == "gradBar") {
				if (this.transform.localScale.x>0) {
					this.transform.localScale = new Vector3 (this.transform.localScale.x - gradScaleSpeed, this.transform.localScale.y, this.transform.localScale.z);
				}else{

					print(this.name);
					print("Destroying parent");
					Destroy(transform.parent.gameObject);

				}
			}else{
				Destroy (this);
			}
		
		} else {

			curValue = character.health;
			propScale = curValue / maxValue;

			if (curValue >= 0) {

				if (this.name == "instaBar") {
		
					this.transform.localScale = new Vector3 (scaleMax * propScale, this.transform.localScale.y, this.transform.localScale.z);
		
				} else if (this.name == "gradBar" && this.transform.localScale.x > scaleMax * propScale) {

					this.transform.localScale = new Vector3 (this.transform.localScale.x - gradScaleSpeed, this.transform.localScale.y, this.transform.localScale.z);
		
				}
	
			} else {
		
				this.transform.localScale = new Vector3 (0, this.transform.localScale.y, this.transform.localScale.z);

			}
		}
	}


}
