using UnityEngine;
using System.Collections;

public class StarBar : MonoBehaviour {

	public GameObject character;
	public GameObject[] stars;
	public Sprite[] spriteswitch;
	public SpriteRenderer parSprite;

	// Use this for initialization
	void Start () {

		parSprite = transform.parent.GetComponent<SpriteRenderer> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (character != null) {
			switch (character.GetComponent<Fighter> ().stars) {
		
			case 0:
				if (stars [0].activeSelf == true) {
					stars [0].SetActive (false);
				}
				if (stars [1].activeSelf == true) {
					stars [1].SetActive (false);
				}
				if (stars [2].activeSelf == true) {
					stars [2].SetActive (false);
				}
				if(parSprite.sprite == spriteswitch[1]){
					parSprite.sprite = spriteswitch[0];
				}
				break;

			case 1:
				if (stars [0].activeSelf == false) {
					stars [0].SetActive (true);
				}
				if (stars [1].activeSelf == true) {
					stars [1].SetActive (false);
				}
				if (stars [2].activeSelf == true) {
					stars [2].SetActive (false);
				}
				if(parSprite.sprite == spriteswitch[1]){
					parSprite.sprite = spriteswitch[0];
				}
				break;

			case 2:
				if (stars [0].activeSelf == false) {
					stars [0].SetActive (true);
				}
				if (stars [1].activeSelf == false) {
					stars [1].SetActive (true);
				}
				if (stars [2].activeSelf == true) {
					stars [2].SetActive (false);
				}
				if(parSprite.sprite == spriteswitch[1]){
					parSprite.sprite = spriteswitch[0];
				}
				break;

			case 3:
				if (stars [0].activeSelf == false) {
					stars [0].SetActive (true);
				}
				if (stars [1].activeSelf == false) {
					stars [1].SetActive (true);
				}
				if (stars [2].activeSelf == false) {
					stars [2].SetActive (true);
				}
				if(parSprite.sprite == spriteswitch[0]){
					parSprite.sprite = spriteswitch[1];
				}
				break;
		
			default:
				break;
			}
		}
	}
}
