using UnityEngine;
using System.Collections;

public class GameManagerManager : MonoBehaviour {

	public static GameObject manager;
	public static ForestProgression forestProgression;

	// Use this for initialization
	void Awake () {


		if(manager == null) {
			manager = GameObject.Find("_GameManager");
			manager = Instantiate(Resources.Load ("_GameManager")) as GameObject;
			manager.name = "_GameManager";
		}
		if(forestProgression == null) {
			forestProgression = manager.GetComponent<ForestProgression>();
		}
	}
}
