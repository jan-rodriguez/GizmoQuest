using UnityEngine;
using System.Collections;

public class GizmoUI : MonoBehaviour {

	RuntimePlatform platform = Application.platform;
	bool isMobile = false;
	bool draggingObject = false;

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
				draggingObject = TouchingObject(touchPos);
			}
		}else{
			if(Input.GetTouch(0).phase == TouchPhase.Ended) {
				draggingObject = false;
				return;
			}

			Vector2 touchPos = Input.GetTouch(0).position;
			Vector3 mouseScreenPos = Camera.main.ScreenToWorldPoint(touchPos);
			mouseScreenPos.z = 0;
			
			transform.position = mouseScreenPos;
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
}
