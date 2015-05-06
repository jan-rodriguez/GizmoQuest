using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	private static ParticleSystem cloudEmitter;
	private HingeJoint2D springJoint;
	private bool isDragging = false;
	private SwipeCamera camSwipe;
	private Rigidbody2D rigidBod;
	private float originalY;
	private const float leftXPos = -50f;

	// Use this for initialization
	void Start () {

		originalY = transform.position.y;

		camSwipe = Camera.main.GetComponent<SwipeCamera> ();

		if (cloudEmitter == null) {
			cloudEmitter = GameObject.Find ("CloudEmitter").GetComponent<ParticleSystem>();
		}
		springJoint = GetComponent<HingeJoint2D>();

		float randomVel = Random.Range (.2f, .6f);
		rigidBod = GetComponent<Rigidbody2D> ();
		rigidBod.velocity = new Vector2 (randomVel, 0);

		foreach (GameObject cloud in GameObject.FindGameObjectsWithTag("cloud")) {
			if(cloud != gameObject) {
				Physics2D.IgnoreCollision(cloud.GetComponent<Collider2D>(), GetComponent<Collider2D> ());
			}
		}
	}

	void Update() {
		if (Utils.isMobile) {
			HandleMobileInput ();	
		} else {
			HandleRegularInput ();
		}

	}

	void HandleMobileInput () {
		if(cloudEmitter.isPlaying){
			if(Input.GetMouseButtonUp(0)){
				camSwipe.cameraCanMove = true;
				springJoint.enabled = false;
				isDragging = false;
				float randomVel = Random.Range (.2f, .6f);
				rigidBod.velocity = new Vector2 (randomVel, 0);
				cloudEmitter.Stop();
				StartCoroutine(GoToOriginalY ());
			}
		}
		if (isDragging) {
			if(Input.touchCount > 0) {
				Vector3 mousePos = Input.GetTouch(0).position;
				mousePos.z = transform.position.z - Camera.main.transform.position.z;
				springJoint.connectedAnchor = Camera.main.ScreenToWorldPoint(mousePos);
			}

		}
	}

	void HandleRegularInput () {
		if(cloudEmitter.isPlaying){
			if(Input.GetMouseButtonUp(0)){
				camSwipe.cameraCanMove = true;
				springJoint.enabled = false;
				isDragging = false;
				float randomVel = Random.Range (.2f, .6f);
				rigidBod.velocity = new Vector2 (randomVel, 0);
				cloudEmitter.Stop();
				StartCoroutine(GoToOriginalY ());
			}
		}
		if (isDragging) {
			
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = transform.position.z - Camera.main.transform.position.z;
			springJoint.connectedAnchor = Camera.main.ScreenToWorldPoint(mousePos);
		}
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "bounds") {
			transform.localPosition = new Vector3(leftXPos, transform.localPosition.y, 0);
			float randomVel = Random.Range (.2f, .6f);
			rigidBod.velocity = new Vector2 (randomVel, 0);

		}
	}

	void OnMouseDown () {
		camSwipe.cameraCanMove = false;
		isDragging = true;
		cloudEmitter.transform.parent = transform;
		cloudEmitter.transform.localPosition = Vector2.zero;
		cloudEmitter.Play ();

		if(springJoint != null){
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = transform.position.z - Camera.main.transform.position.z;
			springJoint.enabled = true;
			springJoint.connectedAnchor = Camera.main.ScreenToWorldPoint(mousePos);

		}
	}

	private IEnumerator GoToOriginalY () {
		int direction = transform.position.y < originalY ? 1 : -1;
		while (transform.position.y < originalY - .5f || transform.position.y > originalY + .5f) {
			rigidBod.velocity = new Vector2(0, direction);
			yield return new WaitForSeconds(.25f);
		}
		float randomVel = Random.Range (.2f, .6f);
		rigidBod.velocity = new Vector2 (randomVel, 0);
		yield return null;
	}
}
