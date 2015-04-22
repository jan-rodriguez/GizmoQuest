using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KiteBuilder : MonoBehaviour {

	private Dictionary<string, bool> partsDict = new Dictionary<string,bool> ();

	private const string STRING = "string";
	private const string CLOTH = "cloth";
	private const string TAIL = "tail";
	private const string LONG_ROD = "long rod";
	private const string SHORT_ROD = "short rod";

	public enum LAYER {
		STRING = 6,
		CLOTH = 10,
		TAIL = 9,
		LONG_ROD = 8,
		SHORT_ROD = 7
	}

	private const float DISTANCE_THRESHOLD = .01f;
	private const float ANGLE_THRESHOLD = 10f;

	private Vector2 longRodPos = Vector2.zero;
	private const float longRodAngle = 90f;
	private Vector2 clothPos = Vector2.zero;
	private const float clothAngle = 0f;
	private const float shortRodAngle = 0f;
	private  Vector2 shortRodPos = Vector3.zero;


	// Use this for initialization
	void Start () {
		partsDict.Add (CLOTH, false);
		partsDict.Add (LONG_ROD, false);
		partsDict.Add (SHORT_ROD, false);
		partsDict.Add (STRING, false);
		partsDict.Add (TAIL, false);
	}
	
	// Update is called once per frame
	void Update () {
	
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
				gizmo.transform.parent = this.transform;
				gizmo.transform.localPosition = clothPos;
				gizmo.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, clothAngle));
				gizmo.GetComponent<GizmoWorldDrag>().enabled = false;
				partsDict.Add(CLOTH, true);
				
			}
			break;
		
		//TODO: ADD SHORT ROD CASE
		case LONG_ROD:
			float longRodDist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - longRodPos).magnitude;
			float rodAngle = gizmo.transform.eulerAngles.z > 180 ? gizmo.transform.eulerAngles.z - 360 : gizmo.transform.eulerAngles.z;
			float longRodAngleDiff = Mathf.Abs(longRodAngle - rodAngle);

			bool hasLongRod = false;

			if( longRodDist < DISTANCE_THRESHOLD && longRodAngleDiff < ANGLE_THRESHOLD 
			   && partsDict.TryGetValue(LONG_ROD, out hasLongRod) && !hasLongRod) {
				//Set the kite as the parent
				gizmo.transform.parent = this.transform;
				gizmo.transform.localPosition = clothPos;
				gizmo.transform.rotation = Quaternion.Euler(new Vector3(0, 0, longRodAngle));
				gizmo.GetComponent<GizmoWorldDrag>().enabled = false;
				partsDict.Add(LONG_ROD, true);
			}
			break;
		case STRING:
			float stringDist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - longRodPos).magnitude;
			float strAngle = gizmo.transform.eulerAngles.z > 180 ? gizmo.transform.eulerAngles.z - 360 : gizmo.transform.eulerAngles.z;
			float stringAngleDiff = Mathf.Abs(longRodAngle - strAngle);
			bool hasString = false;
			if( stringDist < DISTANCE_THRESHOLD && stringAngleDiff < ANGLE_THRESHOLD 
			   && partsDict.TryGetValue(STRING, out hasString) && !hasString) {
				//Set the kite as the parent
				gizmo.transform.parent = this.transform;
				gizmo.transform.localPosition = clothPos;
				gizmo.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, clothAngle));
				gizmo.GetComponent<GizmoWorldDrag>().enabled = false;
				partsDict.Add(STRING, true);
				
			}
			break;
		case TAIL:
			float tailDist = ((Vector2)transform.InverseTransformPoint(gizmo.transform.position) - longRodPos).magnitude;
			float tailAngle = gizmo.transform.eulerAngles.z > 180 ? gizmo.transform.eulerAngles.z - 360 : gizmo.transform.eulerAngles.z;
			float tailAngleDiff = Mathf.Abs(longRodAngle - tailAngle);
			bool hasTail = false;
			if( tailDist < DISTANCE_THRESHOLD && tailAngleDiff < ANGLE_THRESHOLD
			   && partsDict.TryGetValue(TAIL, out hasTail) && !hasTail) {
				//Set the kite as the parent
				gizmo.transform.parent = this.transform;
				gizmo.transform.localPosition = clothPos;
				gizmo.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, clothAngle));
				gizmo.GetComponent<GizmoWorldDrag>().enabled = false;
				partsDict.Add(TAIL, true);
			}
			break;
		}

	}
}
