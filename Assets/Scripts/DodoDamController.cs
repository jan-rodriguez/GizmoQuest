using UnityEngine;
using System.Collections;

public class DodoDamController : MonoBehaviour {
	
	GameObject skipButton;
	ThoughtBubble bubbleScript;
	ForestProgression progression;

	// Use this for initialization
	void Start () {
		bubbleScript = transform.GetChild (0).GetComponent<ThoughtBubble> ();
		progression = GameManagerManager.forestProgression;

		if (progression.haveSlingshot ()) {
			int children = transform.childCount;

			for(int i = 0; i < children; i++) {
				Destroy(transform.GetChild(i).gameObject);
			}
		}
	}

	void OnMouseDown () {
		if (!progression.haveSlingshotPrint ()) {
			progression.getSlingshotPrint();
			StartCoroutine(bubbleScript.MoveToCorner());
		}

	}
}
