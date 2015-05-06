using UnityEngine;
using System.Collections;

public class DodoCliffController : MonoBehaviour {

	Animator animator;
	SpriteRenderer tissueBoxRenderer;
	CliffProgression tissueBoxProgression;
	ForestProgression progression;
	bool clicked = false;

	void Start () {
		animator = GetComponent<Animator>();
		GameObject tissueBox = transform.GetChild(0).gameObject;
		tissueBoxRenderer = tissueBox.GetComponent<SpriteRenderer>();
		tissueBoxProgression = tissueBox.GetComponent<CliffProgression> ();
		progression = GameManagerManager.forestProgression;
		if(progression.haveTissueBox()) {
			clicked = true;
			animator.SetBool("hasBox", false);
		}
	}

	void OnMouseDown() {
		if(CliffProgression.itemsCollectible && !clicked){
			clicked = true;
			animator.SetBool("hasBox", false);
			tissueBoxRenderer.enabled = true;
			tissueBoxProgression.CollectTissueBox();
		}

		if (progression.haveBanjo ()) {
			PlayBanjo();
		}
	}

	void PlayBanjo() {
		//TODO: MAKE DODO PLAY BANJO HERE
	}
}
