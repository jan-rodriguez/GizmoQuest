using UnityEngine;
using System.Collections;

public class DodoCliffController : MonoBehaviour {

	Animator animator;
	SpriteRenderer tissueBoxRenderer;
	CliffProgression tissueBoxProgression;
	ForestProgression progression;
	bool clicked = false;
	AudioSource audSrc;

	public AudioClip[] noBanjoClips;
	public AudioClip[] banjoDialogClips;
	public Lion lionScript;
	public Ladder ladderScript;
	public AudioClip banjoClip;

	void Start () {
		audSrc = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();
		GameObject tissueBox = transform.GetChild(0).gameObject;
		tissueBoxRenderer = tissueBox.GetComponent<SpriteRenderer>();
		tissueBoxProgression = tissueBox.GetComponent<CliffProgression> ();
		progression = GameManagerManager.forestProgression;
		if(progression.haveTissueBox()) {
			animator.SetBool("hasBox", false);
		}

		if(progression.haveBanjo()) {
			animator.SetBool("hasBanjo", true);
		}
	}

	void OnMouseDown() {
		if(CliffProgression.itemsCollectible && !clicked && progression.haveBanjoPrint()
		   && !progression.haveTissueBox()){
			clicked = true;
			animator.SetBool("hasBox", false);
			tissueBoxRenderer.enabled = true;
			tissueBoxProgression.CollectTissueBox();
		}

		if (progression.haveBanjo () && !clicked) {
			clicked = true;
			PlayBanjo();
		}
	}

	void PlayBanjo() {
		//TODO: MAKE DODO PLAY BANJO HERE
		StartCoroutine(BanjoCoroutine());
	}

	IEnumerator BanjoCoroutine () {
		animator.SetBool("playBanjo", true);

		yield return new WaitForSeconds(1.5f);

		animator.SetBool("playBanjo", false);

		audSrc.PlayOneShot(banjoClip);
		lionScript.WakeUp();
		ladderScript.CollectLadder();

		yield return null;
	}

	void StartTalking () {
		animator.SetBool("talking", true);
	}
}
