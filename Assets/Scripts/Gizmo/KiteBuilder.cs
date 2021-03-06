﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class KiteBuilder : MonoBehaviour {

	private GameObject finishedGizmo;

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
	private const float DISTANCE_THRESHOLD = .3f;
	private const float ANGLE_THRESHOLD = 30f;

	private GameObject backToPrevBtn;

	private Vector2 longRodPos = Vector2.zero;
	private Vector2 clothPos = Vector2.zero;
	private Vector2 shortRodPos = Vector2.zero;
	private Vector2 stringPos = new Vector2(0.131f, -1.286f);

	private Animation hideAnimation;
	private AudioSource correctDropSource;

	// Use this for initialization
	void Start () {
		backToPrevBtn = GameObject.Find ("LoadPreviousLevel");

		hideAnimation = gameObject.GetComponent<Animation>();
		correctDropSource = gameObject.GetComponent<AudioSource>();

		finishedGizmo = Resources.Load ("FinishedKite") as GameObject;

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

			bool hasCloth = false;

			if( clothDist < DISTANCE_THRESHOLD
			   && partsDict.TryGetValue(CLOTH, out hasCloth) && !hasCloth) {
				//Set the kite as the parent
				ConnectGizmo(gizmo, clothPos, CLOTH_LAYER);
			}
			break;
		case SHORT_ROD:
			float shortRodDist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - shortRodPos).magnitude;
			
			bool hasShortRod = false;

			//TODO: ADD THIS BACK TO MAKE SHORT ROD
			if( shortRodDist < DISTANCE_THRESHOLD
			   && partsDict.TryGetValue(SHORT_ROD, out hasShortRod) && !hasShortRod) {
				//Set the kite as the parent
				ConnectGizmo(gizmo, shortRodPos, SHORT_ROD_LAYER);
			}
			break;
		case LONG_ROD:
			float longRodDist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - longRodPos).magnitude;

			bool hasLongRod = false;


			if( longRodDist < DISTANCE_THRESHOLD 
			   && partsDict.TryGetValue(LONG_ROD, out hasLongRod) && !hasLongRod) {
				//Set the kite as the parent
				ConnectGizmo(gizmo, longRodPos, LONG_ROD_LAYER);
			}
			break;
		case STRING:
			float stringDist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - stringPos).magnitude;
			bool hasString = false;

			if( stringDist < DISTANCE_THRESHOLD 
			   && partsDict.TryGetValue(STRING, out hasString) && !hasString) {
				//TODO: CAHNGE FOR STRING
				ConnectGizmo(gizmo, stringPos, STRING_LAYER);
				
			}
			break;
		}

		if(CompletedKite()) {
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
		GameManagerManager.forestProgression.makeKite();
	}

	public void hideBuilderAndShowFinished() {
		backToPrevBtn.GetComponent<Button>().interactable = true;
		backToPrevBtn.GetComponent<Image>().enabled = true;
		Instantiate (finishedGizmo);
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
