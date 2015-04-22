using UnityEngine;
using System.Collections;

public class GizmoWorldDrag : MonoBehaviour {

	public const string BUILD_AREA_TAG = "BuildArea";
	private const int SELECTED_Z_POS = -1;

	KiteBuilder buildArea;
	bool inBuildArea = false;

	Vector2 originalTouch0Pos;
	Vector2 originalTouch1Pos;
	Vector3 hitPoint;
	float orgRotAngleDeg;
	bool draggingObject = false;
	bool rotatingObject = false;
	HingeJoint2D hinge;
	Rigidbody2D rigidBody;

//	LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
		hinge = GetComponent<HingeJoint2D> ();
		rigidBody = GetComponent<Rigidbody2D> ();

		buildArea = GameObject.FindGameObjectWithTag(BUILD_AREA_TAG).GetComponent<KiteBuilder>();
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

	void FixedUpdate() {
		if (draggingObject) {
			DragObject(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
	}

	bool TouchingObject(Vector2 touchPos) {

		RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touchPos), Vector2.zero);

		if (hitInfo) {
			hitPoint = hitInfo.point;
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
					BeginDragObject(touchPos);
				}

			}
		}else{
			if(Input.GetTouch(0).phase == TouchPhase.Ended) {
				Vector2 touchPos = Input.GetTouch(0).position;
				EndDragObject(touchPos);
				return;
			}

			//Two finger rotation
			if(Input.touchCount == 2 && Input.GetTouch(1).phase == TouchPhase.Began){
				rotatingObject = true;

				//Set original rotation from original positions
				orgRotAngleDeg =  transform.eulerAngles.z;
			}
			//Lifted second finger up, no longer rotating object
			else if(rotatingObject && Input.touchCount == 2 && Input.GetTouch(1).phase == TouchPhase.Ended){
				rotatingObject = false;
			}

		}

	}

	void HandleRegularInput(){
		if (!draggingObject) {
			if(Input.GetMouseButtonDown(0) && TouchingObject(Input.mousePosition)){

				BeginDragObject(Input.mousePosition);

			}
		}else{
			//No longer dragging object
			if(Input.GetMouseButtonUp(0)){
				EndDragObject(Input.mousePosition);
				return;
			}
		}
	}

	void RotateObject(){

		//Make sure to have atleast two touches
		if (Input.touchCount == 2) {
			Vector2 curTouch0Pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 curTouch1Pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);
			Vector2 vertex = transform.position;

//			lineRenderer.SetPosition(0, curTouch0Pos);
//			lineRenderer.SetPosition(1, vertex);
//			lineRenderer.SetPosition(2, curTouch1Pos);

			Vector2 a = curTouch0Pos - vertex;
			Vector2 b = vertex - curTouch1Pos;

			float theta = Mathf.Acos(Vector2.Dot(a, b)/(a.magnitude * b.magnitude));

			float curAngle = Mathf.Rad2Deg * theta;

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
		rigidBody.isKinematic = false;
		transform.position = new Vector3(transform.position.x, transform.position.y, SELECTED_Z_POS);
		hinge.anchor = transform.InverseTransformPoint(hitPoint);
	}

	void DragObject(Vector3 position){
//		transform.position = position + dragOffset;
		hinge.connectedAnchor = position;
	}

	void EndDragObject(Vector3 position){
		draggingObject = false;
		rigidBody.isKinematic = true;
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);

		if(inBuildArea) {
			buildArea.TryAttachGizmo(gameObject);
		}
	}
}
