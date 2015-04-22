using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KiteBuilder : MonoBehaviour {

	private Dictionary<string, bool> partsDict = new Dictionary<string,bool> ();

	private const string STRING = "string";
	private const string CLOTH = "cloth";
	private const string TAIL = "tail";
	private const string LONG_ROD = "long_rod";

	public enum LAYER {
		STRING = 6,
		CLOTH = 10,
		TAIL = 9,
		LONG_ROD = 8,
		SHORT_ROD = 7
	}

	private const float DISTANCE_THRESHOLD = .5f;
	private const float ANGLE_THRESHOLD = 10f;

	private Vector3 longRodPos = Vector3.zero;
	private const float longRodAngle = 90f;
	private Vector3 clothPos = Vector3.zero;
	private const float clothAngle = 0f;
	private const float shortRodAngle = 0f;
	private  Vector3 shortRodPos = Vector3.zero;


	// Use this for initialization
	void Start () {
		partsDict.Add (CLOTH, false);
		partsDict.Add (LONG_ROD, false);
		partsDict.Add (STRING, false);
		partsDict.Add (TAIL, false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TryAttachGizmo (GameObject gizmo) {

		switch(gizmo.tag) {

		case CLOTH:
			
			float clothDist = (gizmo.transform.position - clothPos).magnitude;
			float clothAngleDiff = Mathf.Abs(gizmo.transform.eulerAngles.z - clothAngle);
			
			print (clothDist);
			print (clothAngleDiff);

			if( clothDist < DISTANCE_THRESHOLD && clothAngleDiff < ANGLE_THRESHOLD) {
				//Set the kite as the parent
				gizmo.transform.parent = this.transform;
				gizmo.transform.localPosition = clothPos;
				gizmo.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, clothAngle));

				print ("Attached correctly");

				gizmo.GetComponent<GizmoWorldDrag>().enabled = false;
				
			}
			break;
			
		case LONG_ROD:
			float longRodDist = (gizmo.transform.position - longRodPos).magnitude;
			float longRodAngleDiff = Mathf.Abs(gizmo.transform.eulerAngles.z - longRodAngle);

			print (longRodDist);
			print (longRodAngleDiff);

			if( longRodDist < DISTANCE_THRESHOLD && longRodAngleDiff < ANGLE_THRESHOLD) {
				//Set the kite as the parent
				gizmo.transform.parent = this.transform;
				gizmo.transform.localPosition = clothPos;
				gizmo.transform.rotation = Quaternion.Euler(new Vector3(0, 0, longRodAngle));
				
				print ("Attached correctly");

				gizmo.GetComponent<GizmoWorldDrag>().enabled = false;
				
			}
			break;
		case STRING:
			float stringDist = (gizmo.transform.position - longRodPos).magnitude;
			float stringAngleDiff = Mathf.Abs(gizmo.transform.eulerAngles.z - longRodAngle);
			if( stringDist < DISTANCE_THRESHOLD && stringAngleDiff < ANGLE_THRESHOLD) {
				//Set the kite as the parent
				gizmo.transform.parent = this.transform;
				gizmo.transform.localPosition = clothPos;
				gizmo.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, clothAngle));
				
				print ("Attached correctly");
				
				gizmo.GetComponent<GizmoWorldDrag>().enabled = false;
				
			}
			break;
		case TAIL:
			//TODO: MAKE STUFF FOR TAIL HERE
			float tailDist = (gizmo.transform.position - longRodPos).magnitude;
			float tailAngleDiff = Mathf.Abs(gizmo.transform.eulerAngles.z - longRodAngle);
			if( tailDist < DISTANCE_THRESHOLD && tailAngleDiff < ANGLE_THRESHOLD) {
				//Set the kite as the parent
				gizmo.transform.parent = this.transform;
				gizmo.transform.localPosition = clothPos;
				gizmo.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, clothAngle));
				
				print ("Attached correctly");
				
				gizmo.GetComponent<GizmoWorldDrag>().enabled = false;
				
			}
			break;
		}

	}
}
