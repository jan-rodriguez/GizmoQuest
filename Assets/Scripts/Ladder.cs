using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {
	
	public void CollectLadder () {
		gameObject.GetComponent<Animation>().Play();
	}
}
