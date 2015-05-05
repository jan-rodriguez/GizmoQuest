using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	private static ParticleSystem cloudEmitter;
	private HingeJoint2D springJoint;

	// Use this for initialization
	void Start () {

		if (cloudEmitter == null) {
			cloudEmitter = GameObject.Find ("CloudEmitter").GetComponent<ParticleSystem>();
		}

		springJoint = GetComponent<HingeJoint2D>();

		float randomVel = Random.Range (0, .3f);
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (randomVel, 0);
	}

	void Update() {
		if(cloudEmitter.isPlaying){
			if(Input.GetMouseButtonUp(0)){
				cloudEmitter.Stop();
			}
		}
	}

	void OnMouseDown () {
		cloudEmitter.transform.parent = transform;
		cloudEmitter.transform.localPosition = Vector2.zero;
		cloudEmitter.Play ();

		if(springJoint != null){
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = transform.position.z;
			springJoint.connectedAnchor = Camera.main.ScreenToWorldPoint(mousePos);
			springJoint.enabled = true;
		}
	}
}
