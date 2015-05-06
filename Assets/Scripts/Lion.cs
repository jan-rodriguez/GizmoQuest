using UnityEngine;
using System.Collections;

public class Lion : MonoBehaviour {

	bool clicked = false;
	Animator anim;

	void Start () {
		anim = gameObject.GetComponent<Animator>();
	}

	void OnMouseDown () {
		if(!clicked) {
			clicked = true;
			anim.SetBool("awake", true);
		}
	}

}
