using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (GameManagerManager.forestProgression.haveBanjo ()) {
			gameObject.GetComponent<Animation>().Play();
		}
	}
}
