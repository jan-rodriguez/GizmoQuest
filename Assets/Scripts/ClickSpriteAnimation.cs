using UnityEngine;
using System.Collections;

public class ClickSpriteAnimation : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	void OnMouseDown () {
		if (!anim.GetBool ("animate")) {
			StartCoroutine (Animate ());
		}

	}

	IEnumerator Animate () {
		anim.SetBool ("animate", true);
		yield return new WaitForSeconds (1f);
		anim.SetBool ("animate", false);
	}
}
