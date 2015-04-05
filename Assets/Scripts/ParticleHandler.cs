using UnityEngine;
using System.Collections;

public class ParticleHandler : MonoBehaviour {

	ParticleSystem emitter;

	void Start() {
		emitter = GetComponent<ParticleSystem> ();
	}

	public void StartEmitter () {
		emitter.Play ();
	}
}
