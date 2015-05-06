using UnityEngine;
using System.Collections;

public class SavannahController : MonoBehaviour {
	private Animator animator;
	public Sprite defaultSprite;
	private AudioSource source;
	public AudioClip[] clips;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		source = this.GetComponent<AudioSource> ();
		StartCoroutine (dodoSpeechRoutine ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator dodoSpeechRoutine() {
		yield return new WaitForSeconds (0.25f);
		for (int i = 0; i < clips.Length; i++) {
			source.clip = clips[i];
			dodoStartTalking ();
			source.Play ();
			yield return new WaitForSeconds(clips[i].length);
			dodoStopTalking ();
			yield return new WaitForSeconds(1);
		}

		yield return new WaitForSeconds (2);
		GameObject nextArrow = GameObject.Find ("Savannah to Cliff");
		if (nextArrow != null) {
			nextArrow.GetComponent<SpriteRenderer>().enabled = true;
			nextArrow.GetComponent<Collider2D>().enabled = true;
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
