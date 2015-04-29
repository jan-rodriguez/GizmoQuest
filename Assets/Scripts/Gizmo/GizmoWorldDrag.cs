using UnityEngine;
using System.Collections;

public class GizmoWorldDrag : MonoBehaviour {

	public const string BUILD_AREA_TAG = "BuildArea";
	private const int SELECTED_SORTING_ORDER = 100;

	KiteBuilder buildArea;
	bool inBuildArea = false;

	Vector2 originalTouch0Pos;
	Vector2 originalTouch1Pos;
	Vector3 clickOffset;
	float orgRotAngleDeg;
	bool draggingObject = false;
	bool rotatingObject = false;

	AudioSource audioSource;	
	SpriteRenderer spriteRenderer;

//	LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {

		buildArea = GameObject.FindGameObjectWithTag(BUILD_AREA_TAG).GetComponent<KiteBuilder>();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		audioSource = gameObject.GetComponent<AudioSource>();
//		lineRenderer = GetComponent<LineRenderer> ();
//
//		lineRenderer.SetVertexCount(3);
	}
	
	// Update is called once per frame
	void Update () {

		if (Utils.isMobile) {
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
			if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began){
					
				Vector2 touchPos = Input.GetTouch(0).position;

				//Clicked object
				if(TouchingObject(touchPos)){
					BeginDragObject(Camera.main.ScreenToWorldPoint(touchPos));
				}

			}
		}else{
			if(Input.GetTouch(0).phase == TouchPhase.Ended) {
				Vector2 touchPos = Input.GetTouch(0).position;
				EndDragObject();
				return;
			}

			//Two finger rotation
			if(Input.touchCount == 2 && Input.GetTouch(1).phase == TouchPhase.Began && draggingObject){

				rotatingObject = true;


				//Set original rotation from original positions
//				orgRotAngleDeg =  transform.eulerAngles.z;
			}
			//Lifted second finger up, no longer rotating object
			else if(rotatingObject && Input.touchCount == 2 && Input.GetTouch(1).phase == TouchPhase.Ended){
				rotatingObject = false;
			}

		}

		if(draggingObject) {
			DragObject(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));
		}

		if(rotatingObject) {
			RotateObject();
		}

	}

	void HandleRegularInput(){
		if (!draggingObject) {
			if(Input.GetMouseButtonDown(0) && TouchingObject(Input.mousePosition)){

				BeginDragObject(Camera.main.ScreenToWorldPoint(Input.mousePosition));

			}
		}else{
			//No longer dragging object
			if(Input.GetMouseButtonUp(0)){
				EndDragObject();
				return;
			}
		}

		if(draggingObject) {
			DragObject(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
	}

	void RotateObject(){
		draggingObject = false;
		EndDragObject();
		//Make sure to have atleast two touches
		if (Input.touchCount == 2) {

			DetectTouchMovement.Calculate();
			//Rotate the object
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + DetectTouchMovement.turnAngleDelta * 2));
		} 
		//Not touching with two fingers, stop rotation and return
		else {
			rotatingObject = false;
			return;
		}
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if(collider.tag == BUILD_AREA_TAG) {
			inBuildArea = true;
		}
	}
	
	void OnTriggerExit2D (Collider2D collider) {
		if(collider.tag == BUILD_AREA_TAG) {
			inBuildArea = false;
		}
	}

	void BeginDragObject(Vector3 position) {
		draggingObject = true;
		position.z = 0;
		clickOffset = transform.position - position;
		transform.position = position + clickOffset;
		spriteRenderer.sortingOrder = SELECTED_SORTING_ORDER;

		if(audioSource != null) {
			audioSource.Play();
		}
	}

	void DragObject(Vector3 position){
		position.z = 0;	
		transform.position = position + clickOffset;
	}

	void EndDragObject(){
		draggingObject = false;
		spriteRenderer.sortingOrder = 10;

		if(inBuildArea) {
			buildArea.TryAttachGizmo(gameObject);
		}

	}
}
