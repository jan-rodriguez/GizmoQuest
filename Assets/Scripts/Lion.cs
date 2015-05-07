using UnityEngine;
using System.Collections;

public class Lion : MonoBehaviour {

	public AudioClip growl;
	public DodoCliffController dodo;

	bool clicked = false;
	Animator anim;
	ThoughtBubble bubbleScript;
	AudioSource audSrc;

	void Start () {
		anim = gameObject.GetComponent<Animator>();
		bubbleScript = GetComponentInChildren<ThoughtBubble> ();
		audSrc = gameObject.GetComponent<AudioSource>();

		if (GameManagerManager.forestProgression.haveBanjo()) {
			Destroy(transform.GetChild(0).gameObject);
		}
	}

	void OnMouseDown () {
		if (CliffProgression.canBeginLevel) {
			if(!clicked && !GameManagerManager.forestProgression.haveBanjo()) {
				StartCoroutine(bubbleScript.MoveToCorner());
				GameManagerManager.forestProgression.getBanjoPrint();
				dodo.ClickLion();
				clicked = true;
			}
			else if (!GameManagerManager.forestProgression.haveBanjo()) {
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
