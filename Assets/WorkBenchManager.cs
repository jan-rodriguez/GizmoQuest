using UnityEngine;
using System.Collections;

public class WorkBenchManager : MonoBehaviour {

	public static RectTransform BuildArea;
	public static RectTransform Inventory;

	void Start(){
		BuildArea = GameObject.Find ("BuildArea").GetComponent<RectTransform>();
		Inventory = GameObject.Find ("Inventory").GetComponent<RectTransform>();
	}
	
}
