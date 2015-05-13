using UnityEngine;
using System.Collections;

public class CollectChild : MonoBehaviour {

	public Sprite collectedSprite;

	void Start() {
		if (GameManagerManager.forestProgression.haveSlingshot ()) {
			gameObject.GetComponent<SpriteRenderer>().sprite = collectedSprite;
		}
	}

	void OnMouseDown () {
		if(transform.childCount > 0 && DamProgression.itemsCollectible 
		   && GameManagerManager.forestProgression.haveSlingshotPrint() 
		   && !GameManagerManager.forestProgression.haveSlingshot()) {
			GameObject child = transform.GetChild(0).gameObject;
			child.GetComponent<DamProgression>().CollectPiece();
			child.GetComponent<SpriteRenderer>().enabled = true;
			gameObject.GetComponent<SpriteRenderer>().sprite = collectedSprite;
		}
	}
}
