using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

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
		EventSystem.current.GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SFX Volume");
	}

	public void BeginCoroutine (string coroutine)
	{
		StartCoroutine (coroutine);
	}

	public IEnumerator Change360p ()
	{
		if (resOption1.isOn) {
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			Screen.SetResolution (640, 360, Screen.fullScreen);
		}
	}

	public IEnumerator Change720p ()
	{
		if (resOption2.isOn) {
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			Screen.SetResolution (1280, 720, Screen.fullScreen);
		}
	}

	public IEnumerator Change1080p ()
	{
		if (resOption3.isOn) {
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			Screen.SetResolution (1920, 1080, Screen.fullScreen);
		}
	}


}
