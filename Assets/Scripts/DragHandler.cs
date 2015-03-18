using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour {


	RectTransform thisRectTrans;

	Vector2 startPos;
	Vector3 dragOffest;
	bool isDragging;



	void Start()
	{
		thisRectTrans = gameObject.GetComponent<RectTransform>();
	}

	void Update()
	{
		if(Utils.isMobile)
		{
			HandleMobileInput();
		}
		else
		{
			HandleRegularInput();
		}
	}

	void HandleMobileInput()
	{
		if(!isDragging)
		{
			if(Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)
			{

				if(IsPointInRectTransform(Input.GetTouch(0).position, thisRectTrans))
				{
					print ("Starting drag");
					isDragging = true;
					dragOffest = transform.position - (Vector3)(Input.GetTouch(0).position);
				}
			}
		}
		else
		{
			if(Input.GetTouch(0).phase == TouchPhase.Ended) 
			{
				isDragging = false;
				DropGameObject(Input.GetTouch(0).position);
				return;
			}
		}

		if(isDragging)
		{
			DragGameObject(Input.GetTouch(0).position);
		}
	}

	void HandleRegularInput()
	{
		Vector3 mousePosition = Input.mousePosition;
		if (!isDragging) {
			if(Input.GetMouseButtonDown(0) && IsPointInRectTransform(mousePosition, thisRectTrans)){
				isDragging = true;
				dragOffest = transform.position - mousePosition;
			}
		}
		else
		{
			//No longer dragging object
			if(Input.GetMouseButtonUp(0))
			{
				isDragging = false;
				DropGameObject(mousePosition);
				return;
			}
		}

		if(isDragging)
		{
			DragGameObject(Input.mousePosition);
		}
	}

	void DragGameObject(Vector3 position) 
	{
		gameObject.transform.position = position + dragOffest;
	}

	void DropGameObject(Vector3 position)
	{
		if(IsPointInRectTransform(position, WorkBenchManager.BuildArea))
		{
			transform.SetParent(WorkBenchManager.BuildArea);
		}
		else if(IsPointInRectTransform(position, WorkBenchManager.Inventory))
		{
			transform.SetParent(null);
			transform.SetParent(WorkBenchManager.Inventory.transform);
		}
	}


	//Check if a position on screen is within a rect transfrom
	static bool IsPointInRectTransform(Vector2 point, RectTransform rectTrans){
		Vector3[] fourCornersArray = new Vector3[4];
		rectTrans.GetWorldCorners (fourCornersArray);

		Vector3 topLeft = fourCornersArray[0];
		Vector3 bottomRight = fourCornersArray[2];

		return point.x > topLeft.x && point.x < bottomRight.x && point.y > topLeft.y && point.y < bottomRight.y;
	}
	
}