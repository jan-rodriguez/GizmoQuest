using UnityEngine;
using System.Collections;

public class AirfieldProgression : MonoBehaviour {
	private GameObject kaPow;
	private ForestProgression storyManager;
	private GameObject toSavannah;
	private SwipeCamera cameraMover;
	
	// Use this for initialization
	void Start () {
		storyManager = GameObject.Find ("_GameManager").GetComponent<ForestProgression>();
		toSavannah = GameObject.Find ("Airfield to Savannah");
		cameraMover = Camera.main.GetComponent<SwipeCamera> ();
		kaPow = GameObject.Find ("KAPOW");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void tempKiteMaker() {
		if (storyManager.inventory.HaveAllKiteParts ()) {
			print ("Built kite.");
			storyManager.makeKite ();
			toSavannah.GetComponent<SpriteRenderer>().enabled = true;
			toSavannah.GetComponent<BoxCollider2D>().enabled = true;
		}
	}

	IEnumerator acquireThisPart() {
		this.GetComponent<BoxCollider2D>().enabled = false;
		cameraMover.cameraCanMove = false;
		Vector3 centerCam = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 2, 0));
		Vector3 direction = new Vector3(centerCam.x - this.transform.position.x, 
		                                centerCam.y - this.transform.position.y, 
		                                0);
		this.GetComponent<AudioSource> ().Play ();
		for (float f = 1f; f >= 0; f -= 0.05f) {
			this.transform.position += direction * 0.05f;
			this.transform.Rotate(0, 0, 370 / 10);
			yield return null;
		}

		Vector3 scaleUp = new Vector3 (0.1f / 20, 0.1f / 20, 0);
		kaPow.GetComponent<AudioSource> ().Play ();
		for (float f = 1f; f >= 0; f -= 0.05f) {
			kaPow.transform.localScale += scaleUp;
			yield return null;
		}
		yield return new WaitForSeconds (2);
		kaPow.transform.localScale = new Vector3 (0, 0, 0);
		this.GetComponent<SpriteRenderer> ().enabled = false;
		cameraMover.cameraCanMove = true;
	}
	
	// Good lord.
	void OnMouseDown() {
		switch (this.name) {
		case "Dodo":
			if (storyManager.getKitePrint ()) {
				print ("Acquired kite print.");
			} else {
				tempKiteMaker ();
			}
			break;
		case GizmoPrefabs.StringName:
			print ("Acquired wire.");
			storyManager.inventory.AddPart (KiteBuilder.STRING, GizmoPrefabs.StringName);
			StartCoroutine(acquireThisPart ());
			break;
		case GizmoPrefabs.ClothName:
			print ("Acquired cloth.");
			storyManager.inventory.AddPart (KiteBuilder.CLOTH, GizmoPrefabs.ClothName);
			StartCoroutine(acquireThisPart ());
			break;
		case GizmoPrefabs.StrawName:
			print ("Acquired straw.");
			storyManager.inventory.AddPart (KiteBuilder.LONG_ROD, GizmoPrefabs.StrawName);
			StartCoroutine(acquireThisPart ());
			break;
		case GizmoPrefabs.PenName:
			print ("Acquired pen.");
			storyManager.inventory.AddPart (KiteBuilder.SHORT_ROD, GizmoPrefabs.PenName);
			StartCoroutine(acquireThisPart ());
			break;
		}
	}
}
