using UnityEngine;
using System.Collections;

public class ThoughtBubble : MonoBehaviour {

	public string itemToBuild;
	public Sprite finishedBubble;

	public IEnumerator MoveToCorner() {
		Vector3 currentScale = this.transform.localScale;
		this.transform.localScale = new Vector3(currentScale.x * .75f, currentScale.y * .75f);
		this.transform.parent = Camera.main.transform;
		Camera.main.GetComponent<SwipeCamera> ().cameraCanMove = false;
		Vector3 destinationPoint = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width * .9f, 
		                                                                        Screen.height * .9f, 
		                                                                        transform.position.z - Camera.main.transform.position.z));
		Vector3 direction = new Vector3(destinationPoint.x - this.transform.position.x, 
		                                destinationPoint.y - this.transform.position.y, 
		                                0);
		for (int i = 60 ; i >= 0; i--) {
			this.transform.position += direction / (60f);
			yield return null;
		}

		SpriteRenderer sr = this.GetComponent<SpriteRenderer> ();
		sr.color = new Color (1f, 1f, 1f, 0.5f);
		if(finishedBubble != null) {
			sr.sprite = finishedBubble;
		}

		this.transform.GetChild (0).GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.2f);
		Camera.main.GetComponent<SwipeCamera> ().cameraCanMove = true;


	}

	void OnMouseDown() {
		GameManagerManager.forestProgression.gizmoToBuild = itemToBuild;
		StartCoroutine(GameManagerManager.manager.GetComponent<SceneManager>().GoToWorkShop());
	}
}
