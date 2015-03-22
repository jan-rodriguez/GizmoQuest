using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GizmoSlot : MonoBehaviour {

	private int remainingGizmos = 10;
	private GizmoUIItem childGizmoItem;
	private Text gizmoRemainingText;


	// Use this for initialization
	void Start () {
		childGizmoItem = GetComponentInChildren<GizmoUIItem> ();
		gizmoRemainingText = GetComponentInChildren<Text> ();	
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CreateNewChildGizmo () {
		if (remainingGizmos > 0) {
			Transform.Instantiate(childGizmoItem.gizmoPrefab);
			remainingGizmos--;
			UpdateGizmoCount();
		}
	}

	void UpdateGizmoCount() {
		gizmoRemainingText.text = remainingGizmos.ToString ();
	}
}
