using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GizmoMovementHandler : MonoBehaviour {

	private RectTransform m_thisRectTrans;
	private Image m_thisImage;
	private bool isDragging;

	static Color selectedColor = Color.red;
	static Color deselectedColor = Color.white;
	static Vector2 startPos;
	static Vector3 dragOffest;

	void Start()
	{
		m_thisRectTrans = gameObject.GetComponent<RectTransform>();
		m_thisImage = gameObject.GetComponent<Image> ();
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

				if(IsPointInRectTransform(Input.GetTouch(0).position, m_thisRectTrans))
				{
					SelectGizmo();
					isDragging = true;
					dragOffest = transform.position - (Vector3)(Input.GetTouch(0).position);
				}
			}
		}
		//***Dragging***
		else
		{
			if(Input.GetTouch(0).phase == TouchPhase.Ended) 
			{
				DropGameObject(Input.GetTouch(0).position);
				return;
			}
		}
		//***/Dragging***

		if(isDragging)
		{
			DragGameObject(Input.GetTouch(0).position);
		}
	}

	void HandleRegularInput()
	{
		Vector3 mousePosition = Input.mousePosition;
		if (!isDragging) {
			if(Input.GetMouseButtonDown(0) && IsPointInRectTransform(mousePosition, m_thisRectTrans)){
				BeginGameObjectDrag();
				dragOffest = transform.position - mousePosition;
			}
		}
		else
		{
			//No longer dragging object
			if(Input.GetMouseButtonUp(0))
			{
				DropGameObject(mousePosition);
				return;
			}
		}

		if(isDragging)
		{
			DragGameObject(Input.mousePosition);
		}
	}

	void BeginGameObjectDrag() {
		SelectGizmo ();
		//Set the canvas as the parent so the dragged object is always on top of everything
		transform.SetParent (WorkBenchManager.WorkBenchUI);
		isDragging = true;
	}

	//Move game object to the position
	void DragGameObject(Vector3 position) 
	{
		gameObject.transform.position = position + dragOffest;
	}

	void DropGameObject(Vector3 position)
	{
		isDragging = false;

		//Dropped within the game area

		transform.SetParent(null);
		transform.SetParent(WorkBenchManager.Inventory.transform);
		//Only selected in the build area
		DeselectGizmo();
	}

	//Set the gizmo to the highlighted state
	void SelectGizmo () 
	{
		WorkBenchManager.SetSelectedGizmo(this);
		m_thisImage.color = selectedColor;
		transform.position = new Vector3 (transform.position.x, transform.position.y, 10);
	}

	//Set gizmo color back to regular state
	public void DeselectGizmo() 
	{
		WorkBenchManager.selectedGizmoHandler = null;
		m_thisImage.color = deselectedColor;
		transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
	}


	//Check if a position on screen is within a rect transfrom
	static bool IsPointInRectTransform(Vector2 point, RectTransform rectTrans)
	{
		Vector3[] fourCornersArray = new Vector3[4];
		rectTrans.GetWorldCorners (fourCornersArray);

		Vector3 topLeft = fourCornersArray[0];
		Vector3 bottomRight = fourCornersArray[2];

		return point.x > topLeft.x && point.x < bottomRight.x && point.y > topLeft.y && point.y < bottomRight.y;
	}
	
}