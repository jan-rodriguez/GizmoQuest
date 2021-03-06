﻿using UnityEngine;
using System.Collections;

public class SwipeCamera : MonoBehaviour {

	public SpriteRenderer backgroundRenderer;
	public bool cameraCanMove = true;
	public GameObject swipeNotifier;

	public static bool allowClicks = false;
	private const float minSwipeDist  = 10.0f;
	private const float maxSwipeTime = 0.5f;
	private const float SWIPE_Z_POS = -10.0f;
	private const float CAM_LINEAR_DRAG = 0.8f;
	private const float MIN_CAM_X_VEL = 0.5f;
	
	private float camMinXPos = -19.0f;
	private float camMaxXPos = 19.0f;
	private Vector3 fingerStartPos = Vector3.zero;
	private Vector3 prevFingerPos = Vector3.zero;
	private float camXVel = 0.0f;
	private bool isSwipe = false;
	private bool isFinishingSwipe = false;

	void Start () {
		float distance =  Mathf.Abs(transform.position.z - backgroundRenderer.transform.position.z);
		float frustumHeight = 2.0f * distance * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
		float frustumWidth = frustumHeight * Camera.main.aspect;

		camMinXPos = backgroundRenderer.bounds.min.x + .5f * frustumWidth;
		camMaxXPos = backgroundRenderer.bounds.max.x - .5f * frustumWidth;

		if(GameManagerManager.forestProgression.haveMetDodo() && swipeNotifier != null) {
			Destroy (swipeNotifier);
		}
	}

	// Update is called once per frame
	void Update () {
		if (Utils.isMobile) {
			HandleMobileInput ();
		} else {
			HandleRegularInput ();
		}

	}

	public void ChangeBackground(GameObject toArea) {
		// Disable the currently active area
		backgroundRenderer.transform.root.gameObject.SetActive (false);
		
		// Enable the new active area
		toArea.SetActive (true);
		
		// Use the tag (e.g. 'Landing') to get the background ('LandingBackground') sprite renderer
		backgroundRenderer = GameObject.Find (toArea.tag + "Background").GetComponent<SpriteRenderer> ();

		// Reset the camera's viewpoint
		Camera.main.transform.position = new Vector3 (0, 0, -10);

		Start ();
	}

	void HandleMobileInput () {
		if (cameraCanMove) {

			if (Input.touchCount == 1) {
				
				Touch touch = Input.GetTouch (0);
				{
					switch (touch.phase) {
					case TouchPhase.Began:
						/* this is a new touch */
						BeginSwipe (touch.position);
						break;
						
					case TouchPhase.Canceled:
						/* The touch is being canceled */
						EndSwipe (touch.position);
						break;

					case TouchPhase.Ended:
						EndSwipe (touch.position);
						break;

					case TouchPhase.Moved:					
						//Vector3 touchPosVec3 = touch.position;
						if (ShouldSwipe (touch.position)) {
							if(swipeNotifier != null) {
								allowClicks = true;
								Destroy(swipeNotifier);
								swipeNotifier = null;
							}
							SwipeToPosition (touch.position);
							
						}
						break;
					}
				}
			}
		}
	}

	void HandleRegularInput () {
		if (cameraCanMove) {
			Vector3 mousePos = Input.mousePosition;
			if (Input.GetMouseButtonDown (0)) {
				BeginSwipe (Input.mousePosition);
			} else if (Input.GetMouseButton (0)) {
			
				if (ShouldSwipe (mousePos)) {
					if(swipeNotifier != null) {
						allowClicks = true;
						Destroy(swipeNotifier);
						swipeNotifier = null;
					}
					SwipeToPosition (mousePos);	
				}
			} else if (Input.GetMouseButtonUp (0)) {
				EndSwipe (mousePos);
			}
		}
	}

	void BeginSwipe (Vector3 swipeBeginPos) {
		StopCoroutine ("FinishSwipe");
		isFinishingSwipe = false;
		camXVel = 0;
		swipeBeginPos.z = SWIPE_Z_POS;
		isSwipe = true;
		fingerStartPos = swipeBeginPos;
		prevFingerPos = swipeBeginPos;
	}

	void SwipeToPosition (Vector3 swipePos) {
		swipePos.z = SWIPE_Z_POS;

		Vector3 worldDeltaPos = Camera.main.ScreenToWorldPoint(swipePos) - Camera.main.ScreenToWorldPoint(prevFingerPos);
		Camera.main.transform.Translate(worldDeltaPos.x, 0 , 0);
		Vector3 camPos = Camera.main.transform.position;
		//Don't allow camera to go beyond bounds
		Camera.main.transform.position = new Vector3 (Mathf.Clamp (camPos.x, camMinXPos, camMaxXPos), camPos.y, camPos.z);

		prevFingerPos = swipePos;
	}

	void EndSwipe(Vector3 endPos) {
		isSwipe = false;
		camXVel = (prevFingerPos.x - endPos.x);
		isFinishingSwipe = true;
		StartCoroutine ("FinishSwipe");
	}

	IEnumerator FinishSwipe () {
		while (Mathf.Abs(camXVel) > MIN_CAM_X_VEL && isFinishingSwipe) {
			Camera.main.transform.Translate(camXVel * Time.deltaTime, 0, 0);
			Vector3 camPos = Camera.main.transform.position;
			//Don't allow camera to go beyond bounds
			Camera.main.transform.position = new Vector3 (Mathf.Clamp (camPos.x, camMinXPos, camMaxXPos), camPos.y, camPos.z);
			camXVel = camXVel * CAM_LINEAR_DRAG;
			yield return new WaitForEndOfFrame();
		}
	}

	bool ShouldSwipe (Vector3 curSwipePos) {
		return isSwipe && (curSwipePos - fingerStartPos).magnitude > minSwipeDist;
	}

}
