using UnityEngine;
using System.Collections;

public class ThanksForPlaying : MonoBehaviour {

	Animation anim;
	Animator childAnimator;
	AudioSource childAudSrc;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation>();
		childAnimator = GetComponentInChildren<Animator>();
		childAudSrc = GetComponentInChildren<AudioSource>();
	}

	public IEnumerator EndDemo () {
		anim.Play();

		yield return new WaitForSeconds(2f);
		childAnimator.SetBool("talking", true);
		childAudSrc.Play();

		yield return null;
	}
}
