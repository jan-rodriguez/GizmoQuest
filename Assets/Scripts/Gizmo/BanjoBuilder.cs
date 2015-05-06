using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BanjoBuilder : MonoBehaviour {

	//TODO: SET IMAGE HERE
	public GameObject finishedGizmo;
	
	private Dictionary<string, bool> partsDict = new Dictionary<string,bool> ();
	
	public const string BOX = "box";
	public const string POLE = "pole";
	public const string RUBBERBAND = "rubberband";
	public const string RUBBERBAND1 = RUBBERBAND + "1";
	public const string RUBBERBAND2 = RUBBERBAND + "2";
	public const string RUBBERBAND3 = RUBBERBAND + "3";
	public static string[] PARTS_LIST = {BOX, POLE, RUBBERBAND};
	private const int BOX_LAYER = 0;
	private const int POLE_LAYER = 1;
	private const int RUBBERBAND_LAYER = 2;
	private const float DISTANCE_THRESHOLD = .3f;
	private const float ANGLE_THRESHOLD = 30f;
	
	private Vector2 boxPos = Vector2.zero;
	private Vector2 polePos = new Vector2(0, 3.85f);
	private Vector2 rubberband1Pos = new Vector2(-.63f, 0);
	private Vector2 rubberband2Pos = Vector2.zero;
	private Vector2 rubberband3Pos = new Vector2(.57f, 0);
	
	private Animation hideAnimation;
	private AudioSource correctDropSource;
	private GameObject backToPrevBtn;
	
	// Use this for initialization
	void Start () {
		hideAnimation = gameObject.GetComponent<Animation>();
		correctDropSource = gameObject.GetComponent<AudioSource>();
		finishedGizmo = Resources.Load ("FinishedBanjo") as GameObject;
		backToPrevBtn = GameObject.Find ("LoadPreviousLevel");
		partsDict.Add (BOX, false);
		partsDict.Add (POLE, false);
		partsDict.Add (RUBBERBAND1, false);
		partsDict.Add (RUBBERBAND2, false);
		partsDict.Add (RUBBERBAND3, false);
	}
	
	public void TryAttachGizmo (GameObject gizmo) {
		
		switch(gizmo.tag) {

		case BOX:
			float boxDist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - boxPos).magnitude;

			bool hasBox = false;
			
			if( boxDist < DISTANCE_THRESHOLD 
			   && partsDict.TryGetValue(BOX, out hasBox) && !hasBox) {
				//Set the kite as the parent
				ConnectGizmo(gizmo, boxPos, BOX_LAYER);
				partsDict[BOX] = true;
			}
			break;
		case POLE:
			float poleDist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - polePos).magnitude;
			bool hasPole = false;
			
			//TODO: ADD THIS BACK TO MAKE SHORT ROD
			if( poleDist < DISTANCE_THRESHOLD 
			   && partsDict.TryGetValue(POLE, out hasPole) && !hasPole) {
				//Set the kite as the parent
				ConnectGizmo(gizmo, polePos, POLE_LAYER);
				partsDict[POLE] = true;
			}
			break;
		case RUBBERBAND:
			float rubberband1Dist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - rubberband1Pos).magnitude;
			float rubberband2Dist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - rubberband2Pos).magnitude;
			float rubberband3Dist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - rubberband3Pos).magnitude;

			bool hasRubberband1 = false;
			bool hasRubberband2 = false;
			bool hasRubberband3 = false;

			partsDict.TryGetValue(RUBBERBAND1, out hasRubberband1);
			partsDict.TryGetValue(RUBBERBAND2, out hasRubberband2);
			partsDict.TryGetValue(RUBBERBAND3, out hasRubberband3);
			
			
			if( rubberband1Dist < DISTANCE_THRESHOLD && !hasRubberband1) {
				//Set the kite as the parent
				ConnectGizmo(gizmo, rubberband1Pos, RUBBERBAND_LAYER);
				partsDict[RUBBERBAND1] = true;
			}else if(rubberband2Dist < DISTANCE_THRESHOLD && !hasRubberband2){
				//Set the kite as the parent
				ConnectGizmo(gizmo, rubberband2Pos, RUBBERBAND_LAYER);
				partsDict[RUBBERBAND2] = true;
			}else if(rubberband3Dist < DISTANCE_THRESHOLD && !hasRubberband3){
				//Set the kite as the parent
				ConnectGizmo(gizmo, rubberband3Pos, RUBBERBAND_LAYER);
				partsDict[RUBBERBAND3] = true;
			}
			break;
		}
		
		if(CompletedBanjo()) {
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
		GameManagerManager.forestProgression.makeBanjo();
		
	}
	
	public void hideBuilderAndShowFinished() {
		
		backToPrevBtn.GetComponent<Button>().interactable = true;
		backToPrevBtn.GetComponent<Image>().enabled = true;
		Instantiate (finishedGizmo);
		gameObject.SetActive(false);
	}
	
	bool CompletedBanjo () {
		foreach(bool val in partsDict.Values){
			if(val == false) {
				return false;
			}
		}
		
		return true;
	}
}
