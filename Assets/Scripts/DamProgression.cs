using UnityEngine;
using System.Collections;

public class DamProgression : MonoBehaviour {

	private static GameObject kaPow;
	private static ForestProgression storyManager;
	private static SwipeCamera cameraMover;
	public static bool itemsCollectible = true;
	private static DodoDamController dodo;
	private static ThoughtBubble buildSlingshot;
	
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
		if (dodo == null) {
			dodo = GameObject.Find ("Dodo").GetComponent<DodoDamController> ();
		}
		if(buildSlingshot == null) {
			buildSlingshot = GameObject.Find("BigThoughtBubble").GetComponent<ThoughtBubble>();
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
//		dodo.dodoApproval ();
		yield return new WaitForSeconds (1);
		kaPow.transform.localScale = new Vector3 (0, 0, 0);
		
		itemsCollectible = true;
		cameraMover.cameraCanMove = true;
		
		Destroy (gameObject);
	}
	
	// Good lord.
	void OnMouseDown() {
		if (itemsCollectible && GameManagerManager.forestProgression.haveSlingshotPrint()) {
			switch (this.name) {
			case "Dodo":
				break;
			case GizmoPrefabs.RulerName:
				itemsCollectible = false;
				storyManager.inventory.AddPart (SlingShotBuilder.RULER, GizmoPrefabs.RulerName);
				buildSlingshot.CollectPiece();
				StartCoroutine (acquireThisPart ());
				break;
			case GizmoPrefabs.ElasticName:
				itemsCollectible = false;
				storyManager.inventory.AddPart (SlingShotBuilder.ELASTIC, GizmoPrefabs.ElasticName);
				buildSlingshot.CollectPiece();
				StartCoroutine (acquireThisPart ());
				break;
			case GizmoPrefabs.VStickName:
				itemsCollectible = false;
				storyManager.inventory.AddPart (SlingShotBuilder.V_STICK, GizmoPrefabs.VStickName);
				buildSlingshot.CollectPiece();
				StartCoroutine (acquireThisPart ());
				break;
			case GizmoPrefabs.RopeName:
				itemsCollectible = false;
				storyManager.inventory.AddPart (SlingShotBuilder.ROPE, GizmoPrefabs.RopeName);
				buildSlingshot.CollectPiece();
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
			if(storyManager.inventory.HaveAllSlingshotParts()){
				buildSlingshot.Activate();
			}
		}
	}

	public void CollectPiece() {
		if(itemsCollectible && GameManagerManager.forestProgression.haveSlingshotPrint()) {
			switch (this.name) {
			case GizmoPrefabs.ElasticName:
				itemsCollectible = false;
				storyManager.inventory.AddPart (SlingShotBuilder.ELASTIC, GizmoPrefabs.ElasticName);
				buildSlingshot.CollectPiece();
				StartCoroutine (acquireThisPart ());
				break;
			case GizmoPrefabs.RulerName:
				itemsCollectible = false;
				storyManager.inventory.AddPart (SlingShotBuilder.RULER, GizmoPrefabs.RulerName);
				buildSlingshot.CollectPiece();
				StartCoroutine (acquireThisPart ());
				break;
			}
		}
		if(storyManager.inventory.HaveAllSlingshotParts()){
			buildSlingshot.Activate();
		}
	}
}
