using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BanjoBuilder : MonoBehaviour {
//	
//	public GameObject finishedGizmo;
//	
//	private Dictionary<string, bool> partsDict = new Dictionary<string,bool> ();
//	
//	public const string BOX = "box";
//	public const string POLE = "pole";
//	public const string RUBBERBAND = "rubberband";
//	public static string[] PARTS_LIST = {BOX, POLE, RUBBERBAND};
//	private const int BOX_LAYER = 0;
//	private const int POLE_LAYER = 1;
//	private const int RUBBERBAND_LAYER = 2;
//	private const float DISTANCE_THRESHOLD = .1f;
//	private const float ANGLE_THRESHOLD = 30f;
//	
//	private Vector2 boxPos = Vector2.zero;
//	private const float boxAngle = 0f;
//	private Vector2 polePos = Vector2.zero;
//	private const float poleAngle = 0f;
//	private const float shortRodAngle = 90f;
//	private Vector2 shortRodPos = Vector2.zero;
//	private Vector2 stringPos = new Vector2(0.131f, -1.286f);
//	private const float stringAngle = 0f;
//	
//	private Animation hideAnimation;
//	private AudioSource correctDropSource;
//	
//	// Use this for initialization
//	void Start () {
//		hideAnimation = gameObject.GetComponent<Animation>();
//		correctDropSource = gameObject.GetComponent<AudioSource>();
//		
//		partsDict.Add (BOX, false);
//		partsDict.Add (POLE, false);
//		partsDict.Add (RUBBERBAND, false);
//	}
//	
//	public void TryAttachGizmo (GameObject gizmo) {
//		
//		switch(gizmo.tag) {
//			
//			//TODO: MAKE SYMMETRIC CHANGES
//		case BOX:
//			float clothDist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - clothPos).magnitude;
//			float gizmoAngle = gizmo.transform.eulerAngles.z > 180 ? gizmo.transform.eulerAngles.z - 360 : gizmo.transform.eulerAngles.z;
//			float clothAngleDiff = Mathf.Abs(clothAngle - gizmoAngle);
//			
//			bool hasCloth = false;
//			
//			if( clothDist < DISTANCE_THRESHOLD && clothAngleDiff < ANGLE_THRESHOLD 
//			   && partsDict.TryGetValue(BOX, out hasCloth) && !hasCloth) {
//				//Set the kite as the parent
//				ConnectGizmo(gizmo, clothPos, clothAngle, BOX_LAYER);
//			}
//			break;
//		case POLE:
//			float shortRodDist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - shortRodPos).magnitude;
//			float shortRodPrefabAng = gizmo.transform.eulerAngles.z > 180 ? gizmo.transform.eulerAngles.z - 360 : gizmo.transform.eulerAngles.z;
//			float shortRodAngDiff = Mathf.Abs(shortRodAngle - shortRodPrefabAng);
//			
//			bool hasShortRod = false;
//			
//			//TODO: ADD THIS BACK TO MAKE SHORT ROD
//			//			if( shortRodDist < DISTANCE_THRESHOLD && shortRodAngDiff < ANGLE_THRESHOLD 
//			//			   && partsDict.TryGetValue(SHORT_ROD, out hasShortRod) && !hasShortRod) {
//			//Set the kite as the parent
//			ConnectGizmo(gizmo, shortRodPos, shortRodAngle, POLE_LAYER);
//			//			}
//			break;
//		case RUBBERBAND:
//			float longRodDist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - longRodPos).magnitude;
//			float rodAngle = gizmo.transform.eulerAngles.z > 180 ? gizmo.transform.eulerAngles.z - 360 : gizmo.transform.eulerAngles.z;
//			float longRodAngleDiff = Mathf.Abs(longRodAngle - rodAngle);
//			
//			bool hasLongRod = false;
//			
//			
//			if( longRodDist < DISTANCE_THRESHOLD && longRodAngleDiff < ANGLE_THRESHOLD 
//			   && partsDict.TryGetValue(RUBBERBAND, out hasLongRod) && !hasLongRod) {
//				//Set the kite as the parent
//				ConnectGizmo(gizmo, longRodPos, longRodAngle, RUBBERBAND_LAYER);
//			}
//			break;
//		}
//		
//		if(CompletedKite()) {
//			AlertUserCompleted();
//		}
//		
//	}
//	
//	void ConnectGizmo (GameObject gizmo, Vector3 pos, float angle, int sortingOrder) {
//		//Set the kite as the parent
//		gizmo.transform.parent = this.transform;
//		gizmo.transform.localPosition = pos;
//		gizmo.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
//		gizmo.GetComponent<GizmoWorldDrag>().enabled = false;
//		gizmo.GetComponent<Collider2D>().enabled = false;
//		gizmo.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
//		partsDict[gizmo.tag] = true;
//		if(correctDropSource != null) {
//			correctDropSource.Play();
//		}
//		
//	}
//	
//	void AlertUserCompleted () {
//		hideAnimation.Play();
//		foreach(Animation anim in gameObject.GetComponentsInChildren<Animation>()){
//			anim.Play ();
//		}
//		GameManagerManager.manager.GetComponent<ForestProgression>().makeKite();
//		
//	}
//	
//	public void hideBuilderAndShowFinished() {
//		finishedGizmo.SetActive(true);
//		gameObject.SetActive(false);
//	}
//	
//	bool CompletedKite () {
//		foreach(bool val in partsDict.Values){
//			if(val == false) {
//				return false;
//			}
//		}
//		
//		return true;
//	}
}
