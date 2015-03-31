using UnityEngine;
using System.Collections;

public class WorkBenchManager : MonoBehaviour {

	public static RectTransform Inventory;
	public static RectTransform WorkBenchUI;

	public static GizmoMovementHandler selectedGizmoHandler;

	void Start()
	{
		Inventory = GameObject.Find ("Inventory").GetComponent<RectTransform>();
		WorkBenchUI = gameObject.GetComponent<RectTransform> ();
	}

	public static void SetSelectedGizmo (GizmoMovementHandler newHandler) 
	{
		if(selectedGizmoHandler != null && selectedGizmoHandler != newHandler)
		{
			selectedGizmoHandler.DeselectGizmo();
		}
		selectedGizmoHandler = newHandler;
	}
	
}
