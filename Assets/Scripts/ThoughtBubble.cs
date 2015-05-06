using UnityEngine;
using System.Collections;

public class ThoughtBubble : MonoBehaviour {

	public IEnumerator MoveToCorner() {
		GameObject thoughtBubbles = transform.parent.gameObject;
		this.transform.parent = Camera.main.transform;
		Destroy (thoughtBubbles);
		Camera.main.GetComponent<SwipeCamera> ().cameraCanMove = false;
		Vector3 destinationPoint = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width * .1f, 
		                                                                        Screen.height * .1f, 
		                                                                        -10));
		Vector3 direction = new Vector3(destinationPoint.x - this.transform.position.x, 
		                                destinationPoint.y - this.transform.position.y, 
		                                0);
		for (int i = 60 ; i >= 0; i--) {
			this.transform.position += direction / (60f );
			yield return null;
		}
		this.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.5f);
		this.transform.GetChild (0).GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.2f);
		Camera.main.GetComponent<SwipeCamera> ().cameraCanMove = true;
	}

	void OnMouseDown() {
		GameManagerManager.forestProgression.gizmoToBuild = GizmoPrefabs.KITE;
		StartCoroutine(GameManagerManager.manager.GetComponent<SceneManager>().GoToWorkShop());
	}
}
