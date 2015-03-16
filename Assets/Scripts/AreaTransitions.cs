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
		switch (this.name) {
		case "City to IMAX":
			SwitchBackgrounds ("IMAX");
			break;
		case "IMAX to City":
			SwitchBackgrounds ("City");
			break;
		}
	}

	void SwitchBackgrounds(string toBackground) {
		Vector3 newPos = GameObject.Find (toBackground + "Master").transform.position;
		Camera.main.transform.position = newPos;
		print (newPos);
		print (Camera.main.transform.position);
		SpriteRenderer newBackground = GameObject.Find (toBackground + "Background").GetComponent<SpriteRenderer>();
		Camera.main.GetComponent<MoveCamera> ().SetBoundarySprite (newBackground);
	}
}
