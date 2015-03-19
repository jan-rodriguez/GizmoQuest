using UnityEngine;
using System.Collections;

public class SceneManager: MonoBehaviour {
	
	public static int previousLevel;
	public static bool updatePreviousLevel = true;

	enum Level
	{
		WorkBench = 0,
		Map
	}

	// Use this for initialization
	void Start () {
		if (updatePreviousLevel) {
			previousLevel = Application.loadedLevel;
		}
	}

	public void GoToWorkshop () {
		// TODO: this
		Application.LoadLevel ((int)Level.WorkBench);
	}

	public void LoadPreviousLevel () {
		if (previousLevel != Application.loadedLevel) {
			Application.LoadLevel (previousLevel);
		}

	}

	void OnLevelWasLoaded(int level) {
		//Only allow the previous level to be updated if we're not going to the workbench
		if (level == (int)Level.WorkBench) {
			updatePreviousLevel = false;
		} else {
			updatePreviousLevel = true;
		}
	}
}
