using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveCamera : MonoBehaviour {
	public SpriteRenderer backgroundRenderer;
	private float rightBound;
	private float leftBound;
	private float conversionSlope;
	private float conversionIntercept;

	// Use this for initialization
	void Start () {
		SetBackground("");
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void MoveCameraToSlider () {
		Vector3 pos = Camera.main.transform.position;
		Camera.main.transform.position = pos;
	}

	public void SetBackground (string toBackground) {
		if (string.IsNullOrEmpty (toBackground)) {
			toBackground = backgroundRenderer.tag;
		} else {
			SpriteRenderer newBackgroundRenderer = GameObject.Find (toBackground + "Background").GetComponent<SpriteRenderer> ();
			backgroundRenderer = newBackgroundRenderer;
		}

		// Move the camera to the new background
		Vector3 newPos = GameObject.Find (toBackground + "Master").transform.position;
		Camera.main.transform.position = newPos;

		// Set the bounds of the camera's horizontal movement so it is limited to the BG sprite
		float vertExtent = Camera.main.orthographicSize;
		float horzExtent = vertExtent * Camera.main.aspect;
		leftBound = (float)(backgroundRenderer.bounds.min.x + horzExtent);
		rightBound = (float)(backgroundRenderer.bounds.max.x - horzExtent);

		// Do some math
		float slope = 1.0f / (rightBound - leftBound);
		float intercept = -(slope * leftBound);

		// Invert the equation we just found
		conversionSlope = 1.0f / slope;
		conversionIntercept = -(intercept / slope);
	}
}
