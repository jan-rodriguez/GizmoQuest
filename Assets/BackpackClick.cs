using UnityEngine;
using System.Collections;

public class BackpackClick : MonoBehaviour {

	public Sprite openSprite;
	public GameObject child;
	bool clicked = false;

	void OnMouseDown () {
		if (!clicked) {
			clicked = true;
			gameObject.GetComponent<SpriteRenderer>().sprite = openSprite;
			child.SetActive(true);
		}
	}
}
