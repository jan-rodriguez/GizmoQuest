using UnityEngine;
using System.Collections;

public class SlingshotRock : MonoBehaviour {

	public GameObject otherRock;

	void StartOtherRockAnim () {
		otherRock.GetComponent<Animation> ().Play ();
		otherRock.GetComponent<AudioSource>().Play();
		Destroy (gameObject);
	}
}
