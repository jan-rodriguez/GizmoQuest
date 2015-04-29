using UnityEngine;
using System.Collections;

public class AllowCameraToMove : MonoBehaviour {

	public void LetCameraMove () {
		Camera.main.GetComponent<SwipeCamera>().cameraCanMove = true;
	}
}
