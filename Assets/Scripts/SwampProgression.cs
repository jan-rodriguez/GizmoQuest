using UnityEngine;
using System.Collections;

public class SwampProgression : MonoBehaviour {
	private ForestProgression storyManager;
	private GameObject toCliff;
	
	// Use this for initialization
	void Start () {
		storyManager = GameObject.Find ("_GameManager").GetComponent<ForestProgression>();
		toCliff = GameObject.Find ("Swamp to Cliff");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void tempKiteMaker() {
		if (storyManager.inventory.HaveAllKiteParts ()) {
			storyManager.makeKite ();
			toCliff.GetComponent<SpriteRenderer>().enabled = true;
			toCliff.GetComponent<BoxCollider2D>().enabled = true;
		}
	}
	
	// Good lord.
	void OnMouseDown() {
		switch (this.name) {
			//TODO: this shit
		}
	}
}
