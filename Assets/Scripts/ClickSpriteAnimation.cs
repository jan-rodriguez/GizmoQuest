using UnityEngine;
using System.Collections;

public class ClickSpriteAnimation : MonoBehaviour {

	Animator anim;
	AudioSource audSrc;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		audSrc = GetComponent<AudioSource>();
	}
	
	void OnMouseDown () {
		if (!anim.GetBool ("animate")) {
			StartCoroutine (Animate ());
			if(audSrc != null) {
				audSrc.Play();
			}
		}

	}

	IEnumerator Animate () {
		anim.SetBool ("animate", true);
		yield return new WaitForSeconds (1f);
		anim.SetBool ("animate", false);
	}
}
