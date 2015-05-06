using UnityEngine;
using System.Collections;

public class Lion : MonoBehaviour {

	bool clicked = false;
	Animator anim;
	ThoughtBubble bubbleScript;

	void Start () {
		anim = gameObject.GetComponent<Animator>();
		bubbleScript = GetComponentInChildren<ThoughtBubble> ();

		if (GameManagerManager.forestProgression.haveBanjo()) {
			Destroy(transform.GetChild(0).gameObject);
			WakeUp();
		}
	}

	void OnMouseDown () {
		if(!clicked && !GameManagerManager.forestProgression.haveBanjo()) {
			StartCoroutine(bubbleScript.MoveToCorner());
			clicked = true;
		}
	}

	public void WakeUp () {
		anim.SetBool("awake", true);
	}

}
