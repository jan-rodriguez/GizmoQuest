using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AirfieldProgression : MonoBehaviour {
	private static GameObject kaPow;
	private static ForestProgression storyManager;
	private static SwipeCamera cameraMover;
	public static bool itemsCollectible = false;
	private IEnumerator wiggling;
	private static DodoController dodo;
	private static GameObject goToBenchBtn;
	private static ThoughtBubble buildKite;

	// Use this for initialization
	void Start () {
		if (storyManager == null) {
			storyManager = GameManagerManager.forestProgression;
		}
		if (cameraMover == null) {
			cameraMover = Camera.main.GetComponent<SwipeCamera> ();
		}
		if (goToBenchBtn == null) {
			goToBenchBtn = GameObject.Find("ToWorkshop");
		}
		if (kaPow == null) {
			kaPow = GameObject.Find ("KAPOW");
		}
		if (dodo == null) {
			dodo = GameObject.Find ("Dodo").GetComponent<DodoController> ();
		}
		if(buildKite == null) {
			buildKite = GameObject.Find("BigThoughtBubble").GetComponent<ThoughtBubble>();
		}

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

		itemsCollectible = true;
		cameraMover.cameraCanMove = true;

		Destroy (gameObject);
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
				itemsCollectible = false;
				storyManager.inventory.AddPart (KiteBuilder.STRING, GizmoPrefabs.StringName);
				buildKite.CollectPiece();
				StartCoroutine (acquireThisPart ());
				break;
			case GizmoPrefabs.ClothName:
				itemsCollectible = false;
				storyManager.inventory.AddPart (KiteBuilder.CLOTH, GizmoPrefabs.ClothName);
				buildKite.CollectPiece();
				StartCoroutine (acquireThisPart ());
				break;
			case GizmoPrefabs.StrawName:
				itemsCollectible = false;
				storyManager.inventory.AddPart (KiteBuilder.LONG_ROD, GizmoPrefabs.StrawName);
				buildKite.CollectPiece();
				StartCoroutine (acquireThisPart ());
				break;
			case GizmoPrefabs.PenName:
				itemsCollectible = false;
				storyManager.inventory.AddPart (KiteBuilder.SHORT_ROD, GizmoPrefabs.PenName);
				buildKite.CollectPiece();
				StartCoroutine (acquireThisPart ());
				break;
			}


		}

		if (SwipeCamera.allowClicks && this.name == "Dodo") {
			if (storyManager.getKitePrint ()) {
				storyManager.meetDodo ();
				this.GetComponent<DodoController> ().startDodoSpeech ();
			}
		} else {
			if(storyManager.inventory.HaveAllKiteParts()){
				buildKite.Activate();
			}
		}
	}
	
}
