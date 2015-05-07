using UnityEngine;
using System.Collections;

public class ThoughtBubble : MonoBehaviour {

	public string itemToBuild;
	public Sprite finishedBubble;
	public Sprite[] progressionSprites;
	private int collectedPieces = 0;
	AudioSource audSrc;

	void Start () {
		audSrc = gameObject.GetComponent<AudioSource>();
	}

	public IEnumerator MoveToCorner() {
		SpriteRenderer sr = this.GetComponent<SpriteRenderer> ();
		Destroy(transform.GetChild(0).gameObject);
		Vector3 currentScale = this.transform.localScale;
		if(finishedBubble != null) {
			sr.sprite = progressionSprites[0];
		}
		this.transform.localScale = new Vector3(currentScale.x * 1.5f, currentScale.y * 1.5f);
		this.transform.parent = Camera.main.transform;
		Camera.main.GetComponent<SwipeCamera> ().cameraCanMove = false;
		Vector3 destinationPoint = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width * .85f, 
		                                                                        Screen.height * .85f, 
		                                                                        transform.position.z - Camera.main.transform.position.z));
		Vector3 direction = new Vector3(destinationPoint.x - this.transform.position.x, 
		                                destinationPoint.y - this.transform.position.y, 
		                                0);
		for (int i = 60 ; i >= 0; i--) {
			this.transform.position += direction / (60f);
			yield return null;
		}
		Camera.main.GetComponent<SwipeCamera> ().cameraCanMove = true;


	}

	void OnMouseDown() {
		GameManagerManager.forestProgression.gizmoToBuild = itemToBuild;
		StartCoroutine(GameManagerManager.manager.GetComponent<SceneManager>().GoToWorkShop());
	}

	public void Activate () {
		this.GetComponent<BoxCollider2D>().enabled = true;
		this.GetComponent<Animation>().Play();
		audSrc.Play();
	}

	public void CollectPiece () {
		collectedPieces ++;
		SpriteRenderer sr = this.GetComponent<SpriteRenderer> ();
		sr.sprite = progressionSprites[collectedPieces];
	}
}
