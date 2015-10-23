using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

	public Sprite[] ValueSliders;

	public GameObject resOption1;
	public GameObject resOption2;
	public GameObject resOption3;
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
	}

	public void SetSFX (float value) {
		PlayerPrefs.SetFloat ("SFX Volume", value);
		SFXVolumeDisplay.transform.Find ("Bars").GetComponent<Image>().sprite  = ValueSliders [Mathf.RoundToInt (value * 4)];
	}

	public void Change360p (bool value)
	{
		if (resOption1.GetComponentInChildren<Toggle> ().isOn) {
			Screen.SetResolution (640, 360, Screen.fullScreen);
		}
	}

	public void Change720p (bool value)
	{
		if (resOption2.GetComponentInChildren<Toggle> ().isOn) {
			Screen.SetResolution (1280, 720, Screen.fullScreen);
		}
	}

	public void Change1080p (bool value)
	{
		if (resOption3.GetComponentInChildren<Toggle> ().isOn) {
			Screen.SetResolution (1920, 1080, Screen.fullScreen);
		}
	}


}
