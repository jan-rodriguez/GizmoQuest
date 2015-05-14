using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AreaTransitions : MonoBehaviour {
	SceneManager gameSceneManager;

	// Use this for initialization
	void Start () {
		gameSceneManager = GameManagerManager.manager.GetComponent<SceneManager> ();
	}
	
	void OnMouseDown() {
		switch (this.name) {
		case "Airfield to Savannah":
			StartCoroutine(gameSceneManager.GoToSavannah ());
			break;
		case "Savannah to Cliff":
			StartCoroutine(gameSceneManager.GoToCliff ());
			break;
		case "Cliff to Dam":
			StartCoroutine(gameSceneManager.GoToDam());
			break;
		}
	}
}
