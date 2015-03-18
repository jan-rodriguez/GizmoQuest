using UnityEngine;
using System.Collections;

public class AreaTransitions : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {
		MoveCamera cameraMovement = Camera.main.GetComponent<MoveCamera> ();
		switch (this.name) {
		case "City to IMAX":
			cameraMovement.SetBackground ("IMAX");
			break;
		case "IMAX to City":
			cameraMovement.SetBackground ("City");
			break;
		}
	}
}
