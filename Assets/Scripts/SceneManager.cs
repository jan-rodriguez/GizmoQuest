using UnityEngine;
using System.Collections;

public class SceneManager: MonoBehaviour {
	
	public static string previousLevel;
	public static bool updatePreviousLevel = true;
	public static string WORKBENCH = "Workbench";
	public static string AIRFIELD = "Airfield";
	public static string SAVANNAH = "Savannah";
	public static string CLIFF = "Cliff";

	// Use this for initialization
	void Start () {
		if (updatePreviousLevel) {
			previousLevel = Application.loadedLevelName;
		}
	}

	public void GoToWorkshop () {
		Application.LoadLevel (WORKBENCH);
	}

	public void GoToAirfield () {
		Application.LoadLevel (AIRFIELD);
	}

	public void GoToSavannah () {
		Application.LoadLevel (SAVANNAH);
	}

	public void GoToCliff () {
		Application.LoadLevel (CLIFF);
	}

	public void LoadPreviousLevel () {
		if (previousLevel != Application.loadedLevelName) {
			Application.LoadLevel (previousLevel);
		}

	}

	void OnLevelWasLoaded(int level) {
		//Only allow the previous level to be updated if we're not going to the workbench
		if (level == 1) {
			updatePreviousLevel = false;
		} else {
			updatePreviousLevel = true;
		}

		if (updatePreviousLevel) {
			previousLevel = Application.loadedLevelName;
		}
	}
}
