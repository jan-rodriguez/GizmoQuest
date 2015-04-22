using UnityEngine;
using System.Collections;

public class AreaTransitions : MonoBehaviour {
	public GameObject destination;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {
		SwipeCamera cameraMovement = Camera.main.GetComponent<SwipeCamera> ();
		cameraMovement.ChangeBackground (destination);
	}
}
