using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	Vector2 startPos;
	Vector2 clickOffset;

	void Start(){

	}

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData){
		startPos = transform.position;
		clickOffset = startPos - eventData.position;
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData){
		transform.position = eventData.position + clickOffset;
	}

	#endregion


	#region IEndDropHandler implementation

	public void OnEndDrag (PointerEventData eventData) {

		//If they drop it in the build area, set the new parent
		if (IsPointInRectTransform(eventData.position, WorkBenchManager.BuildArea)) {
			transform.SetParent(WorkBenchManager.BuildArea.transform);
		}else if (IsPointInRectTransform(eventData.position, WorkBenchManager.Inventory)){
			//Set parent to null then actual inventory to re-position the element
			transform.SetParent(null);
			transform.SetParent(WorkBenchManager.Inventory.transform);
		}

	}

	#endregion

	static bool IsPointInRectTransform(Vector2 point, RectTransform rectTrans){
		Vector3[] fourCornersArray = new Vector3[4];
		rectTrans.GetWorldCorners (fourCornersArray);

		Vector3 topLeft = fourCornersArray[0];
		Vector3 bottomRight = fourCornersArray[2];

		return point.x > topLeft.x && point.x < bottomRight.x && point.y > topLeft.y && point.y < bottomRight.y;
	}
	
}