using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AirfieldProgression : MonoBehaviour {
	private GameObject kaPow;
	private ForestProgression storyManager;
	private SwipeCamera cameraMover;
	public static bool itemsCollectible = false;
	private IEnumerator wiggling;
	private DodoController dodo;

	// Use this for initialization
	void Start () {
		storyManager = GameObject.Find ("_GameManager").GetComponent<ForestProgression>();
		cameraMover = Camera.main.GetComponent<SwipeCamera> ();
		kaPow = GameObject.Find ("KAPOW");
		dodo = GameObject.Find ("Dodo").GetComponent<DodoController> ();
		wiggling = wiggleAround ();
	}

	IEnumerator wiggleAround() {
		int i = 1;
		yield return new WaitForSeconds (Random.Range(0f, 2f));

		while(true){
			for (int j = 0; j < 10; j++) {
				this.transform.Rotate (0, 0, i * 10);
				i *= -1;
				yield return new WaitForSeconds(0.5f);
			}
			yield return new WaitForSeconds(5);
		}

	}

	IEnumerator acquireThisPart() {
		StopCoroutine (wiggling);
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
		dodo.dodoApproval ();
		yield return new WaitForSeconds (1);
		kaPow.transform.localScale = new Vector3 (0, 0, 0);
		this.GetComponent<SpriteRenderer> ().enabled = false;
		itemsCollectible = true;
		cameraMover.cameraCanMove = true;
	}

	public void StartWiggle(){
		StartCoroutine (wiggling);
	}
	
	// Good lord.
	void OnMouseDown() {
		if (itemsCollectible) {
			switch (this.name) {
			case "Dodo":
				if (storyManager.getKitePrint ()) {
					this.GetComponent<DodoController>().startDodoSpeech ();
				}
				break;
			case GizmoPrefabs.StringName:
				print ("Acquired wire.");
				itemsCollectible = false;
				storyManager.inventory.AddPart (KiteBuilder.STRING, GizmoPrefabs.StringName);
				StartCoroutine (acquireThisPart ());
				break;
			case GizmoPrefabs.ClothName:
				print ("Acquired cloth.");
				itemsCollectible = false;
				storyManager.inventory.AddPart (KiteBuilder.CLOTH, GizmoPrefabs.ClothName);
				StartCoroutine (acquireThisPart ());
				break;
			case GizmoPrefabs.StrawName:
				print ("Acquired straw.");
				itemsCollectible = false;
				storyManager.inventory.AddPart (KiteBuilder.LONG_ROD, GizmoPrefabs.StrawName);
				StartCoroutine (acquireThisPart ());
				break;
			case GizmoPrefabs.PenName:
				print ("Acquired pen.");
				itemsCollectible = false;
				storyManager.inventory.AddPart (KiteBuilder.SHORT_ROD, GizmoPrefabs.PenName);
				StartCoroutine (acquireThisPart ());
				break;
			}
		}

		if (this.name == "Dodo") {
			if (storyManager.getKitePrint ()) {
				this.GetComponent<DodoController> ().startDodoSpeech ();
			}
		}
	}
}
