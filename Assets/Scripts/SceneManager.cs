using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneManager: MonoBehaviour {
	
	public static string previousLevel;
	public static bool updatePreviousLevel = true;
	public static string WORKBENCH = "Workbench";
	public static string AIRFIELD = "Airfield";
	public static string SAVANNAH = "Savannah";
	public static string CLIFF = "Cliff";
	public static string COMPLETED_SAVANNAH = "CompletedSavannah";

	private CanvasGroup loadingPanelCanvas;

	// Use this for initialization
	void Start () {
		GameObject loadingPanel = GameObject.Find ("LoadingPanel");
		if (loadingPanel != null) {
			loadingPanelCanvas = loadingPanel.GetComponent<CanvasGroup>();
		}
		 
		if (updatePreviousLevel) {
			previousLevel = Application.loadedLevelName;
		}
	}

	public IEnumerator GoToWorkShop () {
		AsyncOperation loadingLvl = Application.LoadLevelAsync (WORKBENCH);
		loadingPanelCanvas.alpha = 1;
		yield return loadingLvl;
		Application.LoadLevel (WORKBENCH);
	}

	public IEnumerator GoToAirfield () {
		AsyncOperation loadingLvl = Application.LoadLevelAsync (AIRFIELD);
		loadingPanelCanvas.alpha = 1;
		yield return loadingLvl;
		Application.LoadLevel (AIRFIELD);
	}

	public IEnumerator GoToSavannah () {
		AsyncOperation loadingLvl = Application.LoadLevelAsync (SAVANNAH);
		loadingPanelCanvas.alpha = 1;
		yield return loadingLvl;
		Application.LoadLevel (SAVANNAH);
	}

	public IEnumerator GoToCompletedSavannah() {
		AsyncOperation loadingLvl = Application.LoadLevelAsync (COMPLETED_SAVANNAH);
		loadingPanelCanvas.alpha = 1;
		yield return loadingLvl;
		Application.LoadLevel (COMPLETED_SAVANNAH);
	}

	public IEnumerator GoToCliff () {
		AsyncOperation loadingLvl = Application.LoadLevelAsync (CLIFF);
		loadingPanelCanvas.alpha = 1;
		yield return loadingLvl;
		Application.LoadLevel (CLIFF);
	}

	public IEnumerator LoadPreviousLevel () {
		if (previousLevel != Application.loadedLevelName) {
			AsyncOperation loadingLvl = Application.LoadLevelAsync (previousLevel);
			loadingPanelCanvas.alpha = 1;
			yield return loadingLvl;
			Application.LoadLevel (CLIFF);
		}

	}

	private void ResetLoadingPanel () {
		
		GameObject loadingPanel = GameObject.Find ("LoadingPanel");
		if (loadingPanel != null) {
			loadingPanelCanvas = loadingPanel.GetComponent<CanvasGroup>();
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
		ResetLoadingPanel ();
	}
}
