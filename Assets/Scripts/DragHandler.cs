using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject dragObject;
	Vector3 startPos;
	CanvasGroup canvGroup;

	void Start(){
		canvGroup = GetComponent<CanvasGroup> ();
	}

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData){
		print ("On Begin Drag called");
		dragObject = gameObject;
		startPos = transform.position;
		canvGroup.blocksRaycasts = false;
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData){
		print ("On Drag Called");
		transform.position = Input.mousePosition;
	}

	#endregion


	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData) {
		print ("On End Drag Called");
		dragObject = null;
		transform.position = startPos;
		canvGroup.blocksRaycasts = true;
	}

	#endregion

	
}