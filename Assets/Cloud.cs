using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	private static ParticleSystem cloudEmitter;

	// Use this for initialization
	void Start () {

		if (cloudEmitter == null) {
			print ("NULL EMITTER");
			cloudEmitter = GameObject.Find ("CloudEmitter").GetComponent<ParticleSystem>();
		}

		float randomVel = Random.Range (0, .3f);
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (randomVel, 0);
	}

	void OnMouseDown () {
		cloudEmitter.transform.parent = transform;
		cloudEmitter.transform.localPosition = Vector2.zero;
		cloudEmitter.Play ();
	}
}
