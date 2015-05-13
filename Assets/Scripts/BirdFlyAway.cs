using UnityEngine;
using System.Collections;

public class BirdFlyAway : MonoBehaviour {

	Animator anim;

	void Start() {
		anim = GetComponent<Animator> ();
	}

	public void FlyAway () {
		anim.SetBool ("flying", true);
	}
}
