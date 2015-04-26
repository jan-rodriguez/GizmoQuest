using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GizmoSlot : MonoBehaviour {

	private int remainingGizmos = 1;
	private Image gizmoImage;
	private GameObject gizmoPrefab;

	static Color outOfGizmoColor = Color.gray;
//	static Color remainingGizmoColor = new Color(158, 255, 128);


	// Use this for initialization
	void Awake () {
		gizmoImage = GetComponent<Image> ();
	}

	public void CreateNewChildGizmo () {
		if (remainingGizmos > 0) {
			Instantiate(gizmoPrefab);
			remainingGizmos--;
		}

		if (remainingGizmos == 0) {
			gizmoImage.color = outOfGizmoColor;
		}
	}

	public void SetGizmoPrefab (GameObject gzPrefab) {
		gizmoPrefab = gzPrefab;
		gizmoImage.sprite = gizmoPrefab.GetComponent<SpriteRenderer>().sprite;
	}
}
