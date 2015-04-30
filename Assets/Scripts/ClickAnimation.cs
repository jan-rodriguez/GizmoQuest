using UnityEngine;
using System.Collections;

public class ClickAnimation : MonoBehaviour {

	Animation anim;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animation>();
	}
	
	void OnMouseDown () {
		anim.Play();
	}
}
