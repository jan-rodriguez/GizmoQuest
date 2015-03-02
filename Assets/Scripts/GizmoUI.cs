using UnityEngine;
using System.Collections;

public class GizmoUI : MonoBehaviour {

	RuntimePlatform platform = Application.platform;
	Vector2 originalTouch0Pos;
	Vector2 originalTouch1Pos;
	float orgRotAngleDeg;
	bool isMobile = false;
	bool draggingObject = false;
	bool rotatingObject = false;

	// Use this for initialization
	void Start () {
		isMobile = platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer;
	}
	
	// Update is called once per frame
	void Update () {

		if (isMobile) {
			HandleMobileInput();
		}else{
			HandleRegularInput();
		}
	
	}

	bool TouchingObject(Vector2 touchPos) {

		RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touchPos), Vector2.zero);

		if (hitInfo) {
			return hitInfo.collider.gameObject == gameObject;
		}
		return false;
	}

	void HandleMobileInput(){
		if (!draggingObject) {
			if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
					
				Vector2 touchPos = Input.GetTouch(0).position;

				//Clicked object
				if(TouchingObject(touchPos)){
					draggingObject = true;

					//Set original touch position
					originalTouch0Pos = touchPos;
				}

			}
		}else{
			if(Input.GetTouch(0).phase == TouchPhase.Ended) {
				draggingObject = false;
				return;
			}

			//Two finger rotation
			if(Input.touchCount == 2 && Input.GetTouch(1).phase == TouchPhase.Began){
				rotatingObject = true;
				//Set second touch original position
				originalTouch1Pos = Input.GetTouch(1).position;

				float deltaX = originalTouch0Pos.x - originalTouch1Pos.x;
				float deltaY = originalTouch0Pos.y - originalTouch1Pos.y;

				//Set original rotation from original positions
				orgRotAngleDeg = (Mathf.Rad2Deg * Mathf.Atan2(deltaY, deltaX)) - transform.eulerAngles.z;
			}
			//Lifted second finger up, no longer rotating object
			else if(rotatingObject && Input.touchCount == 2 && Input.GetTouch(1).phase == TouchPhase.Ended){
				rotatingObject = false;
			}

			//Only drag when not rotating
			if(!rotatingObject){
				DragObject();
			}
			//Rotate the object
			else{
				RotateObject();
			}

		}

	}

	void HandleRegularInput(){
		if (!draggingObject) {
			if(Input.GetMouseButtonDown(0)){

				Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

				//Clicked on object
				draggingObject = TouchingObject(mousePos);

			}
		}else{
			//No longer dragging object
			if(Input.GetMouseButtonUp(0)){
				draggingObject = false;
				return;
			}

			//Handle dragging object
			Vector2 mousePos = Input.mousePosition;
			Vector3 mouseScreenPos = Camera.main.ScreenToWorldPoint(mousePos);
			mouseScreenPos.z = 0;

			transform.position = mouseScreenPos;
		}
	}

	void RotateObject(){

		//Make sure to have atleast two touches
		if (Input.touchCount == 2) {
			Vector2 curTouch0Pos = Input.GetTouch(0).position;
			Vector2 curTouch1Pos = Input.GetTouch(1).position;

			float deltaXPrime = curTouch0Pos.x - curTouch1Pos.x;
			float deltaYPrime = curTouch0Pos.y - curTouch1Pos.y;

			float curAngle = Mathf.Rad2Deg * Mathf.Atan2(deltaYPrime, deltaXPrime);

			float deltaAngle = curAngle - orgRotAngleDeg;

			//Rotate the object
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, deltaAngle));
		} 
		//Not touching with two fingers, stop rotation and return
		else {
			rotatingObject = false;
			return;
		}


	}

	void DragObject(){
		Vector2 touchPos = Input.GetTouch(0).position;
		Vector3 mouseScreenPos = Camera.main.ScreenToWorldPoint(touchPos);
		mouseScreenPos.z = 0;
		
		transform.position = mouseScreenPos;
	}
}
