using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour {

	public AudioClip entered;
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
		EventSystem.current.GetComponent<AudioSource>().PlayOneShot (entered);
		EventSystem.current.GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SFX Volume");
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
		if(newMenu != prevMenu) newMenu.GetComponent<Menu> ().prevMenu = gameObject;
		gameObject.SetActive (false);
	}

	public void SetMatch(Match newMatch){
		match = newMatch;
		
	}
	
	public virtual void BackMenu(){
		if (prevMenu) {
			EventSystem.current.GetComponent<AudioSource>().PlayOneShot (BackOutSound);
			SwitchTo(prevMenu);
		}
		
	}
}
