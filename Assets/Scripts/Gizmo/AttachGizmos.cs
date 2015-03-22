using UnityEngine;
using System.Collections;

public class AttachGizmos : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D collider) {
		print(collider.bounds.center);
	}

}
