using UnityEngine;
using System.Collections;

public class ThoughtBubble : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator MoveToCorner() {
		this.transform.parent = Camera.main.transform;
		Destroy (GameObject.Find ("ThoughtBubbles"));
		Camera.main.GetComponent<SwipeCamera> ().cameraCanMove = false;
		Vector3 destinationPoint = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width * .1f, 
		                                                                        Screen.height * .1f, 
		                                                                        -10));
		print (destinationPoint);
		Vector3 direction = new Vector3(destinationPoint.x - this.transform.position.x, 
		                                destinationPoint.y - this.transform.position.y, 
		                                0);
		for (int i = 60 * 4; i >= 0; i--) {
			this.transform.position += direction / (60f * 4);
			yield return null;
		}
		this.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.5f);
		this.transform.GetChild (0).GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.2f);
		Camera.main.GetComponent<SwipeCamera> ().cameraCanMove = true;
	}

	void OnMouseDown() {
		GameManagerManager.manager.GetComponent<ForestProgression> ().gizmoToBuild = "Kite";
		GameManagerManager.manager.GetComponent<SceneManager>().GoToWorkshop();
	}
}
