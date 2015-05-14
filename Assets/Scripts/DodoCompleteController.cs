using UnityEngine;
using System.Collections;

public class DodoCompleteController : MonoBehaviour {

	public AudioClip[] introDialogClips;

	public ThanksForPlaying thanks;

	AudioSource audSrc;
	Animator animator;

	// Use this for initialization
	void Start () {
		audSrc = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();

		StartCoroutine(FinishGameRoutine());
	}
	

	IEnumerator FinishGameRoutine () {
		yield return new WaitForSeconds(4f);
		foreach(AudioClip clip in introDialogClips) {
			dodoStartTalking();
			audSrc.clip = clip;
			audSrc.Play();
			yield return new WaitForSeconds(clip.length);

			dodoStopTalking();

			yield return new WaitForSeconds(.6f);
		}

		//TODO: Call final scene here
		StartCoroutine(thanks.EndDemo());
	}

	public void dodoStartTalking() {
		animator.SetBool ("dodoIsTalking", true);
	}
	
	public void dodoStopTalking() {
		animator.SetBool ("dodoIsTalking", false);
	}
}
