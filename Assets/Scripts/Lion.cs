using UnityEngine;
using System.Collections;

public class Lion : MonoBehaviour {

	public AudioClip growl;
	public DodoCliffController dodo;

	bool clicked = false;
	Animator anim;
	AudioSource audSrc;

	void Start () {
		anim = gameObject.GetComponent<Animator>();
		audSrc = gameObject.GetComponent<AudioSource>();
	}

	void OnMouseDown () {
		StartCoroutine(playGrowl());
		if (CliffProgression.canBeginLevel) {
			if (!GameManagerManager.forestProgression.haveBanjo()) {
				StartCoroutine(playGrowl());
			}
		}
	}

	public void WakeUp () {
		audSrc.Stop ();
		anim.SetBool("awake", true);
	}

	IEnumerator playGrowl () {
		if(audSrc.isPlaying){
			audSrc.Stop();
			audSrc.PlayOneShot(growl);
			yield return new WaitForSeconds(growl.length);
			audSrc.Play();
		}

	}

}
