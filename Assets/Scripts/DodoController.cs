using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DodoController : MonoBehaviour {
	private Animator animator;
	public Sprite defaultSprite;
	private AudioSource source;
	public AudioClip[] sadClips;
	public AudioClip[] speechClips;
	public AudioClip[] yesClips;
	public AudioClip[] kiteClips;

	private int affirmationNumber;

	private IEnumerator sadDodo;
	private IEnumerator speechDodo;
	private IEnumerator kiteDodo;

	private ForestProgression storyManager;

	private GameObject skipButton;
	private bool skip = false;

	// Use this for initialization
	void Start () {
		storyManager = GameObject.Find ("_GameManager").GetComponent<ForestProgression>();
		skipButton = GameObject.Find ("SkipButton");
		animator = this.GetComponent<Animator> ();
		source = this.GetComponent<AudioSource> ();
		sadDodo = dodoSadRoutine ();
		speechDodo = dodoSpeechRoutine ();
		if (!storyManager.haveMetDodo ()) {
			StartCoroutine (sadDodo);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void skipTalking() {
		skip = true;
		source.Stop ();
		dodoStopTalking ();
	}

	void enableSkipButton() {
		skipButton.GetComponent<Button> ().enabled = true;
		skipButton.GetComponent<Image> ().enabled = true;
		skipButton.transform.GetChild (0).GetComponent<Text> ().enabled = true;
	}

	void disableSkipButton() {
		skipButton.GetComponent<Button> ().enabled = false;
		skipButton.GetComponent<Image> ().enabled = false;
		skipButton.transform.GetChild (0).GetComponent<Text> ().enabled = false;
	}

	public void startDodoSpeech() {
		StopCoroutine (sadDodo);
		enableSkipButton ();
		StartCoroutine (speechDodo);
	}

	public void dodoApproval() {
		StartCoroutine (dodoYesRoutine ());
	}

	public void startDodoKite(GameObject progressArrow) {
		StartCoroutine (dodoKiteRoutine (progressArrow));
		enableSkipButton ();
	}

	IEnumerator dodoYesRoutine() {
		source.clip = yesClips [affirmationNumber];
		dodoStartTalking ();
		source.Play ();
		yield return new WaitForSeconds (yesClips [affirmationNumber].length);
		dodoStopTalking ();
		affirmationNumber = (affirmationNumber + 1) % yesClips.Length;
	}

	IEnumerator dodoKiteRoutine(GameObject progressArrow) {
		yield return new WaitForSeconds (1);
		for (int i = 0; i < kiteClips.Length; i++) {
			if (skip) {
				skip = false;
				break;
			}
			source.clip = kiteClips[i];
			dodoStartTalking ();
			source.Play ();
			yield return new WaitForSeconds(kiteClips[i].length);
			dodoStopTalking ();
			yield return new WaitForSeconds(1);
		}
		disableSkipButton ();

		progressArrow.GetComponent<SpriteRenderer>().enabled = true;
		progressArrow.GetComponent<BoxCollider2D>().enabled = true;
		Animation arrowAnimation = progressArrow.GetComponent<Animation> ();
		if (arrowAnimation != null) {
			arrowAnimation.Play ();
		}
	}

	IEnumerator dodoSadRoutine() {
		yield return new WaitForSeconds (2);
		for (int i = 0; i < sadClips.Length; i++) {
			source.clip = sadClips[i];
			dodoStartTalking ();
			source.Play ();
			yield return new WaitForSeconds(sadClips[i].length);
			dodoStopTalking ();
			yield return new WaitForSeconds(3);
		}
	}

	IEnumerator dodoSpeechRoutine() {
		for (int i = 0; i < speechClips.Length; i++) {
			if (skip) {
				skip = false;
				break;
			}
			source.clip = speechClips[i];
			dodoStartTalking ();
			source.Play ();
			yield return new WaitForSeconds(speechClips[i].length);
			dodoStopTalking ();
			yield return new WaitForSeconds(1);
		}

		disableSkipButton ();

		GameObject bubble = GameObject.Find ("BigThoughtBubble");
		if (bubble != null) {
			StartCoroutine(bubble.GetComponent<ThoughtBubble>().MoveToCorner ());
		}

		string[] wigglableParts = new string[]{"String", "Pen", "Straw", "Cloth"};

		AirfieldProgression.itemsCollectible = true;
		foreach (string part in wigglableParts) {
			GameObject.Find (part).GetComponent<AirfieldProgression>().StartWiggle();
		}
		yield return new WaitForSeconds (1);
	}

	public void dodoStartTalking() {
		animator.SetBool ("dodoIsTalking", true);
	}

	public void dodoStopTalking() {
		animator.SetBool ("dodoIsTalking", false);
		this.GetComponent<SpriteRenderer> ().sprite = defaultSprite;
	}
}
