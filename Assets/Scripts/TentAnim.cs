using UnityEngine;
using System.Collections;

public class TentAnim : MonoBehaviour {

	Animator animator;

	IEnumerator openRoutine;

	void Start() {
		animator = this.GetComponent<Animator> ();
	}

	void OnMouseDown() {
		StartCoroutine (OpenAndClose ());
	}

	IEnumerator OpenAndClose () {
		if (!animator.GetBool ("open")) {
			animator.SetBool("open", true);
			
			yield return new WaitForSeconds (1f);
			
			animator.SetBool("open", false);
		}
		yield return null;
	}
}
