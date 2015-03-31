using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITransitions : MonoBehaviour {
	public GameObject mapPanel;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void GoToWorkshop () {
		Application.LoadLevel ("Workbench");
	}
	
	public void ToggleMap () {
		if (mapPanel.activeSelf) {
			mapPanel.SetActive (false);
		} else {
			mapPanel.SetActive (true);
		}
	}
	
	public void MoveScenes(string levelName) {
		Application.LoadLevel (levelName);
	}
}