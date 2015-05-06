using UnityEngine;
using System.Collections;

public class BackpackClick : MonoBehaviour {

	public Sprite openSprite;
	private SpriteRenderer paperRollRenderer;
	private Animation paperRollAnim;
	private Collider2D paperTowelCollider;
	private AudioSource audSrc;
	bool clicked = false;

	void Start() {
		audSrc = gameObject.GetComponent<AudioSource>();
		GameObject paperRoll = transform.GetChild(0).gameObject;
		if(paperRoll != null) {
			paperRollRenderer = paperRoll.GetComponent<SpriteRenderer>();
			paperRollAnim = paperRoll.GetComponent<Animation>();
			paperTowelCollider = paperRoll.GetComponent<Collider2D> ();
		}

		if(GameManagerManager.forestProgression.havePaperTowelRoll()) {
			clicked = true;
			gameObject.GetComponent<SpriteRenderer>().sprite = openSprite;
		}
	}

	void OnMouseDown () {
		if (!clicked && paperRollRenderer != null) {
			clicked = true;
			gameObject.GetComponent<SpriteRenderer>().sprite = openSprite;
			paperRollRenderer.enabled = true;
			paperTowelCollider.enabled = true;
			paperRollAnim.Play();
			audSrc.Play ();
		}
	}
}
