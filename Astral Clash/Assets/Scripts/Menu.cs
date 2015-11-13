using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour {

	public int selected;
	public Match match;
	public GameObject firstSelected;
	public GameObject prevMenu;
	public AudioClip BackOutSound;
	
	// Use this for initialization
	void Start () {

	}

	void OnEnable () {
		EventSystem.current.SetSelectedGameObject (firstSelected);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Cancel")){
			BackMenu();
		}
	}

	public void SwitchTo (GameObject newMenu)
	{
		newMenu.SetActive (true);
		gameObject.SetActive (false);
	}

	public void SetMatch(Match newMatch){
		match = newMatch;
		
	}
	
	public void BackMenu(){

		EventSystem.current.GetComponent<AudioSource>().PlayOneShot (BackOutSound);
		SwitchTo (prevMenu);
		
	}
}
