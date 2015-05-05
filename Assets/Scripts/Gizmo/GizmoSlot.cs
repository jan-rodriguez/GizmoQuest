using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GizmoSlot : MonoBehaviour, IPointerDownHandler {

	private int remainingGizmos = 1;
	private Image gizmoImage;
	private GameObject gizmoPrefab;

	static Color outOfGizmoColor = Color.black;
//	static Color remainingGizmoColor = new Color(158, 255, 128);


	// Use this for initialization
	void Awake () {
		gizmoImage = GetComponent<Image> ();
	}

	void Start() {
		gameObject.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
	}

	public void OnPointerDown(PointerEventData eventData){
		CreateNewChildGizmo (eventData.position);
	}

	public void CreateNewChildGizmo (Vector3 pos) {
		if(gizmoPrefab != null){
			if (remainingGizmos > 0) {
				GameObject gizmo = Instantiate(gizmoPrefab) as GameObject;
				pos.z = 10;
				gizmo.transform.position = Camera.main.ScreenToWorldPoint(pos);
				gizmo.GetComponent<GizmoWorldDrag>().BeginDragObject(gizmo.transform.position);
				remainingGizmos--;
			}
			
			if (remainingGizmos == 0) {
				gizmoImage.color = outOfGizmoColor;
			}
		}
	}

	public void SetGizmoPrefab (GameObject gzPrefab) {
		gizmoPrefab = gzPrefab;
		gizmoImage.sprite = gzPrefab.GetComponent<SpriteRenderer> ().sprite;
		gizmoImage.color = Color.white;
	}
}
