using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GizmoSlot : MonoBehaviour {

	private int remainingGizmos = 1;
	private GizmoUIItem childGizmoItem;
	private Text gizmoRemainingText;
	private Image slotImage;

	static Color outOfGizmoColor = Color.gray;
	static Color remainingGizmoColor = new Color(158, 255, 128);


	// Use this for initialization
	void Start () {
		childGizmoItem = GetComponentInChildren<GizmoUIItem> ();
		gizmoRemainingText = GetComponentInChildren<Text> ();
		slotImage = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CreateNewChildGizmo () {
		if (remainingGizmos > 0) {
			Transform newGizmo = Transform.Instantiate(childGizmoItem.gizmoPrefab);
			remainingGizmos--;
			UpdateGizmoCount();
		}

		if (remainingGizmos == 0) {
			slotImage.color = outOfGizmoColor;
		}
	}

	void UpdateGizmoCount() {
		gizmoRemainingText.text = remainingGizmos.ToString ();
	}
}
