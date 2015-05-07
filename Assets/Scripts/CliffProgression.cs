using UnityEngine;
using System.Collections;

public class CliffProgression : MonoBehaviour {
	private static GameObject kaPow;
	public static bool itemsCollectible = true;
	public static bool canBeginLevel = false;
	private static ForestProgression storyManager;
	private static SwipeCamera cameraMover;
	private static ThoughtBubble buildBanjo;
	// Use this for initialization
	void Start () {
		if (storyManager == null) {
			storyManager = GameManagerManager.forestProgression;
		}
		if (cameraMover == null) {
			cameraMover = Camera.main.GetComponent<SwipeCamera> ();
		}
		if (kaPow == null) {
			kaPow = GameObject.Find ("KAPOW");
		}
		if(buildBanjo == null) {
			buildBanjo = GameObject.Find("BigThoughtBubble").GetComponent<ThoughtBubble>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator acquireThisPart() {
		this.GetComponent<BoxCollider2D>().enabled = false;
		cameraMover.cameraCanMove = false;
		Vector3 centerCam = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 2, 0));
		Vector3 direction = new Vector3(centerCam.x - this.transform.position.x, 
		                                centerCam.y - this.transform.position.y, 
		                                0);
//		this.GetComponent<AudioSource> ().Play ();
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
		yield return new WaitForSeconds (1);
		kaPow.transform.localScale = new Vector3 (0, 0, 0);
		Destroy (gameObject);
		itemsCollectible = true;
		cameraMover.cameraCanMove = true;
	}

	// Good lord.
	void OnMouseDown() {
		if (itemsCollectible && GameManagerManager.forestProgression.haveBanjoPrint()) {
			switch (this.name) {
			case GizmoPrefabs.Vine1Name:
				itemsCollectible = false;
				storyManager.inventory.AddPart (BanjoBuilder.RUBBERBAND, GizmoPrefabs.Vine1Name);
				buildBanjo.CollectPiece();
				StartCoroutine (acquireThisPart ());
				break;
			case GizmoPrefabs.Vine2Name:
				itemsCollectible = false;
				storyManager.inventory.AddPart (BanjoBuilder.RUBBERBAND, GizmoPrefabs.Vine2Name);
				buildBanjo.CollectPiece();
				StartCoroutine (acquireThisPart ());
				break;
			case GizmoPrefabs.Vine3Name:
				itemsCollectible = false;
				storyManager.inventory.AddPart (BanjoBuilder.RUBBERBAND, GizmoPrefabs.Vine3Name);
				buildBanjo.CollectPiece();
				StartCoroutine (acquireThisPart ());
				break;
			case GizmoPrefabs.PaperTowelRollName:
				itemsCollectible = false;
				storyManager.inventory.AddPart (BanjoBuilder.POLE, GizmoPrefabs.PaperTowelRollName);
				storyManager.collectPaperTowelRoll();
				buildBanjo.CollectPiece();
				StartCoroutine (acquireThisPart ());
				break;
			case GizmoPrefabs.TissueBoxName:
				itemsCollectible = false;
				storyManager.inventory.AddPart (BanjoBuilder.BOX, GizmoPrefabs.TissueBoxName);
				buildBanjo.CollectPiece();
				StartCoroutine (acquireThisPart ());
				break;
			}
			
			if(storyManager.inventory.HaveAllBanjoPars()){
				buildBanjo.Activate();
			}

		}
	}


	public void CollectTissueBox () {
		if(name == GizmoPrefabs.PaperTowelRollName) {
			if(itemsCollectible && GameManagerManager.forestProgression.haveBanjoPrint()) {
				itemsCollectible = false;
				storyManager.inventory.AddPart (BanjoBuilder.BOX, GizmoPrefabs.TissueBoxName);
				storyManager.collectTissueBox();
				buildBanjo.CollectPiece();
				StartCoroutine (acquireThisPart ());
				if(storyManager.inventory.HaveAllBanjoPars()){
					buildBanjo.Activate();
				}
			}
		}
	}
}
