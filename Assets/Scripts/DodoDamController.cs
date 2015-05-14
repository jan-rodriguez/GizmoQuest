using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DodoDamController : MonoBehaviour {

	public AudioClip[] introDialogClips;
	public AudioClip[] outroDialogClips;
	public AudioClip[] yesClips;
	public DamSlingshot slingshot;

	private bool stopPlaying = false;
	int affirmationNumber = 0;
	Animator animator;

	GameObject skipButton;
	ThoughtBubble bubbleScript;
	ForestProgression progression;
	AudioSource audSrc;

	// Use this for initialization
	void Start () {
		bubbleScript = transform.GetChild (0).GetComponent<ThoughtBubble> ();
		progression = GameManagerManager.forestProgression;
		audSrc = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();

		if (progression.haveSlingshot ()) {
			StartCoroutine(StartOutroDialog());
			int children = transform.childCount;

			for(int i = 0; i < children; i++) {
				Destroy(transform.GetChild(i).gameObject);
			}
		} else {
			enableSkipButton();
			StartCoroutine(StartIntroDialog());
		}
	}

	void OnMouseDown () {
		if (!progression.haveSlingshotPrint () && DamProgression.canPlayLevel) {
			progression.getSlingshotPrint();
			StartCoroutine(bubbleScript.MoveToCorner());
		}

	}

	public void DodoApproval () {
		StartCoroutine (PlayApproval());
	}
	
	IEnumerator PlayApproval() {
		audSrc.Pause ();
		dodoStartTalking ();
		audSrc.PlayOneShot (yesClips [affirmationNumber]);
		yield return new WaitForSeconds (yesClips [affirmationNumber].length);
		dodoStopTalking ();
		audSrc.UnPause ();
		affirmationNumber = (affirmationNumber + 1) % yesClips.Length;
		yield return null;
	}

	public void skipTalking() {
		stopPlaying = true;
		DamProgression.canPlayLevel = true;
		audSrc.Stop ();
		StopDodoTalks ();
		
		disableSkipButton ();
	}

	void enableSkipButton() {
		if (skipButton == null) {
			skipButton = GameObject.Find ("SkipButton");
		}
		skipButton.GetComponent<Button> ().enabled = true;
		skipButton.GetComponent<Image> ().enabled = true;
	}

	void disableSkipButton() {
		skipButton.GetComponent<Button> ().enabled = false;
		skipButton.GetComponent<Image> ().enabled = false;
	}

	private void StopDodoTalks () {
		stopPlaying = true;
		audSrc.Stop ();
		dodoStopTalking();
		StopCoroutine(StartIntroDialog());
	}

	private IEnumerator StartIntroDialog () {
		foreach(AudioClip clip in introDialogClips) {
			dodoStartTalking();
			if(stopPlaying) {
				stopPlaying = false;
				break;
			}
			audSrc.clip = clip;
			audSrc.Play();

			yield return new WaitForSeconds(clip.length);

			dodoStopTalking();

			yield return new WaitForSeconds(1f);
		}
		dodoStopTalking();
		DamProgression.canPlayLevel = true;
	}

	public IEnumerator StartOutroDialog () {
		for(int i = 0; i < outroDialogClips.Length; i++) {
			AudioClip clip = outroDialogClips[i];
			dodoStartTalking();

			if(i == 1) {
				slingshot.StartSlingshotAnim();
				dodoStopTalking();
				yield return new WaitForSeconds(2.4f);
				dodoStartTalking();
			}

			audSrc.clip = clip;
			audSrc.Play();
			
			yield return new WaitForSeconds(clip.length);
			
			dodoStopTalking();
			
			yield return new WaitForSeconds(1f);


		}
		dodoStopTalking();
		GoToSavannah();
	}

	public void dodoStartTalking() {
		animator.SetBool ("dodoIsTalking", true);
	}

	public void dodoStopTalking() {
		animator.SetBool ("dodoIsTalking", false);
	}

	void GoToSavannah () {
		StartCoroutine(GameManagerManager.manager.GetComponent<SceneManager>().GoToCompletedSavannah());
	}
}
