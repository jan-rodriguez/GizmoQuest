using UnityEngine;
using System.Collections;

public class SceneManager: MonoBehaviour {
	
	public static string previousLevel;
	public static bool updatePreviousLevel = true;
	public static string WORKBENCH = "Workbench";
	public static string TEST_LEVEL = "TestScene";
	public static string CITY_LEVEL = "CityMap";
	public static string FOREST_LEVEL = "ForestMap";


	// Use this for initialization
	void Start () {
		if (updatePreviousLevel) {
			previousLevel = Application.loadedLevelName;
		}
	}

	public void GoToWorkshop () {
		Application.LoadLevel (WORKBENCH);
	}

	public void GoToCity () {
		Application.LoadLevel (CITY_LEVEL);
	}

	public void GoToForest () {
		Application.LoadLevel (FOREST_LEVEL);
	}

	public void LoadPreviousLevel () {
		if (previousLevel != Application.loadedLevelName) {
			Application.LoadLevel (previousLevel);
		}

	}

	void OnLevelWasLoaded(int level) {
		//Only allow the previous level to be updated if we're not going to the workbench
		if (level == 4) {
			updatePreviousLevel = false;
		} else {
			updatePreviousLevel = true;
		}
	}
}
