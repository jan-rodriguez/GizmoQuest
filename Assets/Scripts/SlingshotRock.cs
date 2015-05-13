using UnityEngine;
using System.Collections;

public class SlingshotRock : MonoBehaviour {

	public GameObject otherRock;

	void StartOtherRockAnim () {
		otherRock.GetComponent<Animation> ().Play ();
		Destroy (gameObject);
	}
}
