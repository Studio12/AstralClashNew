using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MatchSettings : Menu {

	public Text[] MenuOptions;
	public RectTransform selector;
	public GameObject levelMenu;

	private int players;
	private int rounds;
	private int fighters;

	// Use this for initialization
	void Awake () {

		ResetMatchSettings ();

	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButtonDown("Cancel")){
			BackMenu();
		}

	}

	public void SetSelected (int newSelected)
	{
		selected = newSelected;
		selectionEffect ();
	}

	void selectionEffect ()
	{

		if (selected < 3) {
			selector.gameObject.SetActive(true);
			selector.position = new Vector2 (MenuOptions [selected].transform.position.x + 2.4f, MenuOptions [selected].transform.position.y + 5f);
		}
		else selector.gameObject.SetActive(false);

	}

	public void adjustText (float value)
	{
		MenuOptions [selected].text = ((int)value).ToString ();
	}

	public void adjustRounds (float value)
	{
		adjustText (value);
		match.rounds = (int)value;
	}

	public void adjustFighters (float value)
	{
		adjustText (value);

		match.maxPlayers = (int)value;
		Slider humanSlider = MenuOptions [2].gameObject.transform.parent.GetComponent<Slider>();
		if(match.humans>match.maxPlayers){
			
			match.humans = match.maxPlayers;
			match.AI = match.maxPlayers-match.humans;
			MenuOptions [2].text = match.humans.ToString ();
		}
		humanSlider.maxValue = match.maxPlayers;
	}

	public void adjustPlayers (float value)
	{
		adjustText (value);
		match.humans = (int)value;
	}

	public void selectionEffect2 (int increment)
	{
		Slider targetSlider;
		switch (selected) {
		
		case 0:
			targetSlider = MenuOptions [0].gameObject.transform.parent.GetComponent<Slider>();
			if(match.rounds+increment < targetSlider.minValue){
				match.rounds = (int)targetSlider.maxValue;

			}else if(match.rounds+increment > targetSlider.maxValue){
				match.rounds = (int)targetSlider.minValue;

			}
			else{match.rounds += increment;}
			targetSlider.value = match.rounds;
			break;

		case 1:
			targetSlider = MenuOptions [1].gameObject.transform.parent.GetComponent<Slider>();
			if(match.maxPlayers+increment < targetSlider.minValue){
				match.maxPlayers = (int)targetSlider.maxValue;
				
			}else if(match.maxPlayers+increment > targetSlider.maxValue){
				match.maxPlayers = (int)targetSlider.minValue;
				
			}
			else{match.maxPlayers += increment;}
			if(match.humans>match.maxPlayers){

				match.humans = match.maxPlayers;
				match.AI = match.maxPlayers-match.humans;
				MenuOptions [2].text = match.humans.ToString ();
			}
			MenuOptions [1].text = match.maxPlayers.ToString ();
			MenuOptions [2].gameObject.transform.parent.GetComponent<Slider>().maxValue = match.maxPlayers;
			targetSlider.value = match.maxPlayers;
			break;

		case 2:
			targetSlider = MenuOptions [2].gameObject.transform.parent.GetComponent<Slider>();
			if(match.humans+increment == -1){
				
				match.humans = match.maxPlayers;
				
			}else if(match.humans+increment == match.maxPlayers+1){
				
				match.humans = 0;
				
			}
			else{match.humans += increment;}
			MenuOptions [2].text = match.humans.ToString ();
			match.AI = match.maxPlayers-match.humans;
			targetSlider.value = match.humans;
			break;
		
		default:
			break;
		}

		
	}
	
	
	public void selectOption(){
		levelMenu.GetComponent<LevelSelect> ().SetMatch(match);
		SwitchTo (levelMenu);
			
	}

	public void ResetMatchSettings(){

		match = new Match();
		match.maxPlayers = 2;
		match.rounds = 1;
		match.humans = 2;
		match.AI = 0;
		match.p1 = -1;
		match.p2 = -1;
		match.p3 = -1;
		match.p4 = -1;

		MenuOptions [0].text = match.rounds.ToString ();
		MenuOptions [1].text = match.maxPlayers.ToString ();
		MenuOptions [2].text = match.humans.ToString ();

	}

	}