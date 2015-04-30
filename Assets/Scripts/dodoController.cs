using UnityEngine;
using System.Collections;

public class DodoController : MonoBehaviour {
	private Animator animator;
	public Sprite defaultSprite;
	private AudioSource source;
	public AudioClip[] sadClips;
	public AudioClip[] speechClips;
	public AudioClip[] yesClips;

	private int affirmationNumber;

	private IEnumerator sadDodo;
	private IEnumerator speechDodo;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		source = this.GetComponent<AudioSource> ();
		sadDodo = dodoSadRoutine ();
		speechDodo = dodoSpeechRoutine ();
		StartCoroutine(sadDodo);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startDodoSpeech() {
		StopCoroutine (sadDodo);
		StartCoroutine (speechDodo);
	}

	public void dodoApproval() {
		StartCoroutine (dodoYesRoutine ());
	}

	IEnumerator dodoYesRoutine() {
		source.clip = yesClips [affirmationNumber];
		dodoStartTalking ();
		source.Play ();
		yield return new WaitForSeconds (yesClips [affirmationNumber].length);
		dodoStopTalking ();
		affirmationNumber = (affirmationNumber + 1) % yesClips.Length;
	}

	IEnumerator dodoSadRoutine() {
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
			source.clip = speechClips[i];
			dodoStartTalking ();
			source.Play ();
			yield return new WaitForSeconds(speechClips[i].length);
			dodoStopTalking ();
			yield return new WaitForSeconds(1);
		}

		string[] wigglableParts = new string[]{"String", "Pen", "Straw", "Cloth"};

		AirfieldProgression.itemsCollectible = true;
		foreach (string part in wigglableParts) {
			GameObject.Find (part).GetComponent<AirfieldProgression>().StartWiggle();
		}
	}

	public void dodoStartTalking() {
		animator.SetBool ("dodoIsTalking", true);
	}

	public void dodoStopTalking() {
		animator.SetBool ("dodoIsTalking", false);
		this.GetComponent<SpriteRenderer> ().sprite = defaultSprite;
	}
}
