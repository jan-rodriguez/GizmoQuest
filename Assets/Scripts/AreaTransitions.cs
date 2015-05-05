using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AreaTransitions : MonoBehaviour {
	SceneManager gameSceneManager;

	// Use this for initialization
	void Start () {
		gameSceneManager = GameObject.Find ("_GameManager").GetComponent<SceneManager> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {
		switch (this.name) {
		case "Airfield to Savannah":
			gameSceneManager.GoToSavannah ();
			break;
		case "Savannah to Cliff":
			gameSceneManager.GoToCliff ();
			break;
		case "Cliff to Boulder":
			//TODO: add boulder scene
			break;
		}
	}
}
