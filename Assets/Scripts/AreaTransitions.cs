using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AreaTransitions : MonoBehaviour {
	SceneManager gameSceneManager;
	AudioSource audSrc;

	// Use this for initialization
	void Start () {
		gameSceneManager = GameManagerManager.manager.GetComponent<SceneManager> ();
		audSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {
		switch (this.name) {
		case "Airfield to Savannah":
			if(audSrc != null) {
				audSrc.Play();
			}
			StartCoroutine(gameSceneManager.GoToSavannah ());
			break;
		case "Savannah to Cliff":
			StartCoroutine(gameSceneManager.GoToCliff ());
			break;
		case "Cliff to Boulder":
			//TODO: add boulder scene
			break;
		}
	}
}
