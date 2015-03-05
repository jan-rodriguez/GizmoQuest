using UnityEngine;
using System.Collections;

public class RightClickMoveCamera : MonoBehaviour {
	public GameObject MainCamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		MainCamera.transform.position += Vector3.right * 50;
	}
}
