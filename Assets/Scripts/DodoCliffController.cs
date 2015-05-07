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

	public AudioClip[] noBanjoClips;
	public AudioClip[] banjoDialogClips;
	public AudioClip lionClickAudio;
	public AudioClip myTissuesClip;
	public Lion lionScript;
	public Ladder ladderScript;
	public AudioClip banjoClip;
	public AudioClip greatJobClip;


	void Start () {
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
			animator.SetBool("hasBanjo", true);
			CliffProgression.canBeginLevel = true;
			StartCoroutine(FinishCliffDialog());
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
		if (CliffProgression.canBeginLevel) {
			if(CliffProgression.itemsCollectible && !clicked && progression.haveBanjoPrint()
			   && !progression.haveTissueBox()){
				clicked = true;
				animator.SetBool("hasBox", false);
				tissueBoxRenderer.enabled = true;
				tissueBoxProgression.CollectTissueBox();
				StopDodoTalks();
				StartCoroutine(PlayMyTissuesClip());
			}
			
			if (progression.haveBanjo () && !clicked) {
				clicked = true;
				PlayBanjo();
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
			audSrc.clip = banjoDialogClips[i];
			audSrc.Play ();
			
			yield return new WaitForSeconds(banjoDialogClips[i].length);
		}
		StopDodoTalking();
	}

	IEnumerator PlayMyTissuesClip () {
		audSrc.Pause ();
		audSrc.PlayOneShot (myTissuesClip);

		yield return new WaitForSeconds (myTissuesClip.length);

		audSrc.UnPause ();

		yield return null;
	}

	void PlayBanjo() {
		//TODO: MAKE DODO PLAY BANJO HERE
		StopDodoTalks ();
		StartCoroutine(BanjoCoroutine());
	}

	void StartDodoTalking () {
		animator.SetBool ("talking", true);
	}

	void StopDodoTalking () {
		animator.SetBool ("talking", false);
	}

	IEnumerator BanjoCoroutine () {
		animator.SetBool("playBanjo", true);
		audSrc.PlayOneShot(banjoClip);

		yield return new WaitForSeconds(1.5f);

		animator.SetBool("playBanjo", false);

		audSrc.PlayOneShot (greatJobClip);
		lionScript.WakeUp();
		ladderScript.CollectLadder();

		yield return null;
	}

	void StartTalking () {
		animator.SetBool("talking", true);
	}

	public void StopDodoTalks () {
//		StopCoroutine (PlayLionClickClip());
		stopPlaying = true;
		audSrc.Stop ();
		StopDodoTalking ();
		StopCoroutine (BanjoCoroutine ());
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
