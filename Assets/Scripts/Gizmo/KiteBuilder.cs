using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KiteBuilder : MonoBehaviour {

	public GameObject finishedGizmo;

	private Dictionary<string, bool> partsDict = new Dictionary<string,bool> ();

	public const string STRING = "string";
	public const string CLOTH = "cloth";
	public const string LONG_ROD = "long_rod";
	public const string SHORT_ROD = "short_rod";
	public static string[] PARTS_LIST = {STRING, CLOTH, LONG_ROD, SHORT_ROD};
	private const int STRING_LAYER = 0;
	private const int CLOTH_LAYER = 1;
	private const int LONG_ROD_LAYER = 2;
	private const int SHORT_ROD_LAYER = 3;
	private const float DISTANCE_THRESHOLD = .1f;
	private const float ANGLE_THRESHOLD = 30f;

	private Vector2 longRodPos = Vector2.zero;
	private const float longRodAngle = 0f;
	private Vector2 clothPos = Vector2.zero;
	private const float clothAngle = 0f;
	private const float shortRodAngle = 90f;
	private Vector2 shortRodPos = Vector2.zero;
	private Vector2 stringPos = new Vector2(0.131f, -1.286f);
	private const float stringAngle = 0f;

	private Animation hideAnimation;
	private AudioSource correctDropSource;

	// Use this for initialization
	void Start () {
		hideAnimation = gameObject.GetComponent<Animation>();
		correctDropSource = gameObject.GetComponent<AudioSource>();

		partsDict.Add (CLOTH, false);
		partsDict.Add (LONG_ROD, false);
		partsDict.Add (SHORT_ROD, false);
		partsDict.Add (STRING, false);
	}

	public void TryAttachGizmo (GameObject gizmo) {

		switch(gizmo.tag) {

		//TODO: MAKE SYMMETRIC CHANGES
		case CLOTH:
			float clothDist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - clothPos).magnitude;
			float gizmoAngle = gizmo.transform.eulerAngles.z > 180 ? gizmo.transform.eulerAngles.z - 360 : gizmo.transform.eulerAngles.z;
			float clothAngleDiff = Mathf.Abs(clothAngle - gizmoAngle);

			bool hasCloth = false;

			if( clothDist < DISTANCE_THRESHOLD && clothAngleDiff < ANGLE_THRESHOLD 
			   && partsDict.TryGetValue(CLOTH, out hasCloth) && !hasCloth) {
				//Set the kite as the parent
				ConnectGizmo(gizmo, clothPos, clothAngle, CLOTH_LAYER);
			}
			break;
		case SHORT_ROD:
			float shortRodDist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - shortRodPos).magnitude;
			float shortRodPrefabAng = gizmo.transform.eulerAngles.z > 180 ? gizmo.transform.eulerAngles.z - 360 : gizmo.transform.eulerAngles.z;
			float shortRodAngDiff = Mathf.Abs(shortRodAngle - shortRodPrefabAng);
			
			bool hasShortRod = false;

			//TODO: ADD THIS BACK TO MAKE SHORT ROD
//			if( shortRodDist < DISTANCE_THRESHOLD && shortRodAngDiff < ANGLE_THRESHOLD 
//			   && partsDict.TryGetValue(SHORT_ROD, out hasShortRod) && !hasShortRod) {
				//Set the kite as the parent
				ConnectGizmo(gizmo, shortRodPos, shortRodAngle, SHORT_ROD_LAYER);
//			}
			break;
		case LONG_ROD:
			float longRodDist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - longRodPos).magnitude;
			float rodAngle = gizmo.transform.eulerAngles.z > 180 ? gizmo.transform.eulerAngles.z - 360 : gizmo.transform.eulerAngles.z;
			float longRodAngleDiff = Mathf.Abs(longRodAngle - rodAngle);

			bool hasLongRod = false;


			if( longRodDist < DISTANCE_THRESHOLD && longRodAngleDiff < ANGLE_THRESHOLD 
			   && partsDict.TryGetValue(LONG_ROD, out hasLongRod) && !hasLongRod) {
				//Set the kite as the parent
				ConnectGizmo(gizmo, longRodPos, longRodAngle, LONG_ROD_LAYER);
			}
			break;
		case STRING:
			float stringDist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - stringPos).magnitude;
			float strAngle = gizmo.transform.eulerAngles.z > 180 ? gizmo.transform.eulerAngles.z - 360 : gizmo.transform.eulerAngles.z;
			float stringAngleDiff = Mathf.Abs(stringAngle - strAngle);
			bool hasString = false;

			if( stringDist < DISTANCE_THRESHOLD && stringAngleDiff < ANGLE_THRESHOLD 
			   && partsDict.TryGetValue(STRING, out hasString) && !hasString) {
				//TODO: CAHNGE FOR STRING
				ConnectGizmo(gizmo, stringPos, stringAngle, STRING_LAYER);
				
			}
			break;
		}

		if(CompletedKite()) {
			AlertUserCompleted();
		}

	}

	void ConnectGizmo (GameObject gizmo, Vector3 pos, float angle, int sortingOrder) {
		//Set the kite as the parent
		gizmo.transform.parent = this.transform;
		gizmo.transform.localPosition = pos;
		gizmo.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
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
		GameManagerManager.manager.GetComponent<ForestProgression>().makeKite();

	}

	public void hideBuilderAndShowFinished() {
		finishedGizmo.SetActive(true);
		gameObject.SetActive(false);
	}

	bool CompletedKite () {
		foreach(bool val in partsDict.Values){
			if(val == false) {
				return false;
			}
		}

		return true;
	}
}
