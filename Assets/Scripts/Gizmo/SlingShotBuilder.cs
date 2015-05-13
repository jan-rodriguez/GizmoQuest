using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SlingShotBuilder : MonoBehaviour {

	private GameObject finishedGizmo;
	
	private Dictionary<string, bool> partsDict = new Dictionary<string,bool> ();

	public const string V_STICK = "v_stick";
	public const string RULER = "ruler";
	public const string ROPE = "rope";
	public const string ELASTIC = "elastic";
	public static string[] PARTS_LIST = {V_STICK, RULER, ROPE, ELASTIC};

	private GameObject backToPrevBtn;
	private Animation hideAnimation;
	private AudioSource correctDropSource;

	private const int V_STICK_LAYER = 1;
	private const int RULER_LAYER = 2;
	private const int ROPE_LAYER = 3;
	private const int ELASTIC_LAYER = 4;
	private const float DISTANCE_THRESHOLD = .3f;
	private const float ANGLE_THRESHOLD = 30f;

	private Vector2 vStickPos = new Vector2(-.05f, .6f);
	private Vector2 rulerPos = new Vector2(.16f, -.88f);
	private Vector2 ropePos = new Vector2 (.16f, -.5f);
	private Vector2 elasticPos = new Vector2(0, 1.71f);

	void Start () {
		backToPrevBtn = GameObject.Find ("LoadPreviousLevel");
		finishedGizmo = Resources.Load ("FinishedSlingshot") as GameObject;
		
		hideAnimation = gameObject.GetComponent<Animation>();
		correctDropSource = gameObject.GetComponent<AudioSource>();

		partsDict.Add (V_STICK, false);
		partsDict.Add (RULER, false);
		partsDict.Add (ROPE, false);
		partsDict.Add (ELASTIC, false);
	}

	public void TryAttachGizmo (GameObject gizmo) {
		switch (gizmo.tag) {
		case V_STICK:
			float vStickDist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - vStickPos).magnitude;

			bool hasVStick = false;
			
			if( vStickDist < DISTANCE_THRESHOLD 
			   && partsDict.TryGetValue(V_STICK, out hasVStick) && !hasVStick) {
				//Set the kite as the parent
				ConnectGizmo(gizmo, vStickPos, V_STICK_LAYER);
				partsDict[V_STICK] = true;
			}
			break;
		case RULER:
			float spoonDist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - rulerPos).magnitude;

			bool hasSpoon = false;
			
			if( spoonDist < DISTANCE_THRESHOLD 
			   && partsDict.TryGetValue(RULER, out hasSpoon) && !hasSpoon) {
				//Set the kite as the parent
				ConnectGizmo(gizmo, rulerPos, RULER_LAYER);
				partsDict[RULER] = true;
			}
			break;
		case ROPE:
			float ropeDist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - ropePos).magnitude;
			
			bool hasRope = false;
			
			if( ropeDist < DISTANCE_THRESHOLD 
			   && partsDict.TryGetValue(ROPE, out hasRope) && !hasRope) {
				//Set the kite as the parent
				ConnectGizmo(gizmo, ropePos, ROPE_LAYER);
				partsDict[ROPE] = true;
			}
			break;
		case ELASTIC:
			float elasticDist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - elasticPos).magnitude;
			
			bool hasElastic = false;
			
			if( elasticDist < DISTANCE_THRESHOLD 
			   && partsDict.TryGetValue(ELASTIC, out hasElastic) && !hasElastic) {
				//Set the kite as the parent
				ConnectGizmo(gizmo, elasticPos, ELASTIC_LAYER);
				partsDict[ELASTIC] = true;
			}
			break;
		}

		if(CompletedSlingshot()) {
			AlertUserCompleted();
		}
	}

	void ConnectGizmo (GameObject gizmo, Vector3 pos, int sortingOrder) {
		//Set the kite as the parent
		gizmo.transform.parent = this.transform;
		gizmo.transform.localPosition = pos;
		gizmo.GetComponent<GizmoWorldDrag>().enabled = false;
		gizmo.GetComponent<Collider2D>().enabled = false;
		gizmo.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
		partsDict[gizmo.tag] = true;
		if(correctDropSource != null) {
			correctDropSource.Play();
		}
		
	}

	void AlertUserCompleted () {
		hideAnimation.Play();
		foreach(Animation anim in gameObject.GetComponentsInChildren<Animation>()){
			anim.Play ();
		}
		//TODO: MAKE THIS FOR THE SLINGSHOT
		GameManagerManager.forestProgression.makeSlingshot();
	}
	
	public void hideBuilderAndShowFinished() {
		backToPrevBtn.GetComponent<Button>().interactable = true;
		backToPrevBtn.GetComponent<Image>().enabled = true;
		Instantiate (finishedGizmo);
		gameObject.SetActive(false);
	}

	bool CompletedSlingshot () {
		foreach(bool val in partsDict.Values){
			if(val == false) {
				return false;
			}
		}
		
		return true;
	}
}
