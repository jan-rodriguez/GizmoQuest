using UnityEngine;
using System.Collections;

public class PlayAudioOnStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<AudioSource>().Play();
	}
}
