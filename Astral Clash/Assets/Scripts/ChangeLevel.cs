using UnityEngine;
using System.Collections;

public class ChangeLevel : MonoBehaviour {

	public void NewLevel(string level)
	{
		Application.LoadLevel (level);
	}

}
