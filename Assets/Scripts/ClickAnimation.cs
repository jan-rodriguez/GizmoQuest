using UnityEngine;
using System.Collections;

public class ClickAnimation : MonoBehaviour {

	Animation anim;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animation>();
		audio = gameObject.GetComponent<AudioSource>();
	}
	
	void OnMouseDown () {
		anim.Play();

		if(audio != null) {
			audio.PlayOneShot(audio.clip);
		}
	}
}
