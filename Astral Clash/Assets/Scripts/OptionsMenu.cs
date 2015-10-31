using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsMenu : Menu {

	public Sprite[] ValueSliders;

	public Toggle resOption1;
	public Toggle resOption2;
	public Toggle resOption3;
	public Toggle windowedButton;
	public Toggle fullscreenButton;
	public Slider MusicVolumeDisplay;
	public Slider SFXVolumeDisplay;
	
	void Start ()
	{
		windowedButton.isOn = !Screen.fullScreen;
		fullscreenButton.isOn = Screen.fullScreen;
		MusicVolumeDisplay.value = PlayerPrefs.GetFloat ("Music Volume");
		SFXVolumeDisplay.value = PlayerPrefs.GetFloat ("SFX Volume");
	}

	public void IncreaseMusic ()
	{
		MusicVolumeDisplay.value = Mathf.Round (MusicVolumeDisplay.value * 4.0f) / 4.0f; 
		MusicVolumeDisplay.value += 0.25f;
	}

	public void DecreaseMusic ()
	{
		MusicVolumeDisplay.value = Mathf.Round (MusicVolumeDisplay.value * 4.0f) / 4.0f; 
		MusicVolumeDisplay.value -= 0.25f;
	}

	public void IncreaseSFX ()
	{
		SFXVolumeDisplay.value = Mathf.Round (SFXVolumeDisplay.value * 4.0f) / 4.0f; 
		SFXVolumeDisplay.value += 0.25f;
	}
	
	public void DecreaseSFX ()
	{
		SFXVolumeDisplay.value = Mathf.Round (SFXVolumeDisplay.value * 4.0f) / 4.0f; 
		SFXVolumeDisplay.value -= 0.25f;
	}

	public void SetFullScreen (bool value)
	{
		Screen.fullScreen = value;
	}

	public void SetVolume (float value) {
		PlayerPrefs.SetFloat ("Music Volume", value);
		MusicVolumeDisplay.transform.Find ("Bars").GetComponent<Image>().sprite  = ValueSliders [Mathf.RoundToInt (value * 4)];
		Camera.main.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat ("Music Volume");
	}

	public void SetSFX (float value) {
		PlayerPrefs.SetFloat ("SFX Volume", value);
		SFXVolumeDisplay.transform.Find ("Bars").GetComponent<Image>().sprite  = ValueSliders [Mathf.RoundToInt (value * 4)];
	}

	public void Change360p (bool value)
	{
		if (resOption1.isOn) {
			Screen.SetResolution (640, 360, Screen.fullScreen);
		}
	}

	public void Change720p (bool value)
	{
		if (resOption2.isOn) {
			Screen.SetResolution (1280, 720, Screen.fullScreen);
		}
	}

	public void Change1080p (bool value)
	{
		if (resOption3.isOn) {
			Screen.SetResolution (1920, 1080, Screen.fullScreen);
		}
	}


}
