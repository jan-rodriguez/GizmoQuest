using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DodoCliffController : MonoBehaviour {

	Animator animator;
	SpriteRenderer tissueBoxRenderer;
	CliffProgression tissueBoxProgression;
	ForestProgression progression;
	bool clicked = false;
	AudioSource audSrc;
	bool stopPlaying = false;
	GameObject skipButton;
	ThoughtBubble bubbleScript;
	int affirmationNumber = 1;
	GameObject damButton;

	public AudioClip[] noBanjoClips;
	public AudioClip[] banjoDialogClips;
	public AudioClip[] yesClips;
	public AudioClip lionClickAudio;
	public AudioClip myTissuesClip;
	public Lion lionScript;
	public Ladder ladderScript;

	void Start () {
		damButton = GameObject.Find ("Cliff to Dam");
		bubbleScript = GetComponentInChildren<ThoughtBubble> ();
		audSrc = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();
		GameObject tissueBox = transform.GetChild(0).gameObject;
		tissueBoxRenderer = tissueBox.GetComponent<SpriteRenderer>();
		tissueBoxProgression = tissueBox.GetComponent<CliffProgression> ();
		progression = GameManagerManager.forestProgression;
		if (progression.haveTissueBox ()) {
			animator.SetBool ("hasBox", false);
		} else {
			StartCoroutine(BeginCliffDialog());
			enableSkipButton();
		}

		if(progression.haveBanjo()) {
			int children = transform.childCount;
			for(int i = 0; i < children; i++) {
				Destroy(transform.GetChild(i).gameObject);
			}
			animator.SetBool("hasBanjo", true);
			CliffProgression.canBeginLevel = true;
			StartCoroutine(FinishCliffDialog());
		}
	}

	void ShowDamButton() {
		damButton.GetComponent<SpriteRenderer>().enabled = true;
		damButton.GetComponent<BoxCollider2D>().enabled = true;
		Animation arrowAnimation = damButton.GetComponent<Animation> ();
		if (arrowAnimation != null) {
			arrowAnimation.Play ();
		}
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

	public void skipTalking() {
		stopPlaying = true;
		CliffProgression.canBeginLevel = true;
		audSrc.Stop ();
		StopDodoTalks ();

		disableSkipButton ();
	}

	void OnMouseDown() {
		if (!CliffProgression.canBeginLevel) {
			StartCoroutine(bubbleScript.MoveToCorner());
			GameManagerManager.forestProgression.getBanjoPrint();
		}
		else {
			if(CliffProgression.itemsCollectible && !clicked && progression.haveBanjoPrint()
			   && !progression.haveTissueBox()){
				clicked = true;
				animator.SetBool("hasBox", false);
				tissueBoxRenderer.enabled = true;
				tissueBoxProgression.CollectTissueBox();
				StopDodoTalks();
				StartCoroutine(PlayMyTissuesClip());
			}
		}
	}

	IEnumerator BeginCliffDialog () {
		StartDodoTalking ();
		for (int i = 0; i < noBanjoClips.Length; i++) {
			if(stopPlaying) {
				stopPlaying = false;
				break;
			}
			audSrc.clip = noBanjoClips[i];
			audSrc.Play ();

			yield return new WaitForSeconds(noBanjoClips[i].length);
		}
		CliffProgression.canBeginLevel = true;
		StopDodoTalking ();
	}

	IEnumerator FinishCliffDialog () {
		StartDodoTalking ();
		for (int i = 0; i < banjoDialogClips.Length; i++) {
			if(stopPlaying) {
				stopPlaying = true;
				break;
			}
			if(i == 1) {
				StopDodoTalking();
				animator.SetBool("playBanjo", true);
			}
			audSrc.clip = banjoDialogClips[i];
			audSrc.Play ();

			yield return new WaitForSeconds(banjoDialogClips[i].length);

			if(i == 1) {
				animator.SetBool("playBanjo", false);
				lionScript.WakeUp();
				ladderScript.CollectLadder();
				StartDodoTalking ();
			}
		}
		StopDodoTalking();

		ShowDamButton();
	}

	IEnumerator PlayMyTissuesClip () {
		audSrc.Pause ();
		StartDodoTalking();
		audSrc.PlayOneShot (myTissuesClip);

		yield return new WaitForSeconds (myTissuesClip.length);
		StopDodoTalking();
		audSrc.UnPause ();

		yield return null;
	}

	public void DodoApproval () {
		StartCoroutine (PlayApproval());
	}

	IEnumerator PlayApproval() {
		audSrc.Pause ();
		StartDodoTalking ();
		audSrc.PlayOneShot (yesClips [affirmationNumber]);
		yield return new WaitForSeconds (yesClips [affirmationNumber].length);
		StopDodoTalking ();
		audSrc.UnPause ();
		affirmationNumber = (affirmationNumber + 1) % yesClips.Length;
		yield return null;
	}

	void StartDodoTalking () {
		animator.SetBool ("talking", true);
	}

	void StopDodoTalking () {
		animator.SetBool ("talking", false);
	}

	void StartTalking () {
		animator.SetBool("talking", true);
	}

	public void StopDodoTalks () {
		stopPlaying = true;
		audSrc.Stop ();
		StopDodoTalking ();
		StopCoroutine (BeginCliffDialog());
		StopCoroutine (FinishCliffDialog());
	}

	public void ClickLion () {
		StopDodoTalks ();

		StartCoroutine (PlayLionClickClip ());
	}

	public IEnumerator PlayLionClickClip () {

		audSrc.PlayOneShot (lionClickAudio);

		StartDodoTalking ();

		yield return new WaitForSeconds (lionClickAudio.length);

		StopDodoTalking ();
	}
}
