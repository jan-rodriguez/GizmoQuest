using UnityEngine;
using System.Collections;

public class GameManagerManager : MonoBehaviour {

	public static GameObject manager;

	// Use this for initialization
	void Awake () {

		manager = GameObject.Find("_GameManager");
		if(manager == null) {
			manager = Instantiate(Resources.Load ("_GameManager")) as GameObject;
			manager.name = "_GameManager";
		}
	}
}
