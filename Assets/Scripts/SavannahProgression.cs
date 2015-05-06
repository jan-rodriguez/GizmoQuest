using UnityEngine;
using System.Collections;

public class SavannahProgression : MonoBehaviour {
	private ForestProgression storyManager;
	private SwipeCamera cameraMover;
	private GameObject toCliff;

	// Use this for initialization
	void Start () {
		storyManager = GameManagerManager.forestProgression;
		cameraMover = Camera.main.GetComponent<SwipeCamera> ();
		toCliff = GameObject.Find ("Savannah to Cliff");
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
		case "Dodo":
			if (storyManager.getKitePrint ()) {
				print ("Acquired kite print.");
			} else {
				print ("Already have kite print.");
			}
			break;
		case "Sticks":
			storyManager.inventory.AddPart ("long rod", "stick");
			storyManager.inventory.AddPart ("short rod", "stick");
			print ("Acquired kite sticks.");
			this.gameObject.SetActive(false);
			tempKiteMaker();
			break;
		case "Paper":
			storyManager.inventory.AddPart ("cloth", "paper");
			print ("Acquired kite cloth.");
			this.gameObject.SetActive(false);
			tempKiteMaker();
			break;
		case "Yarn":
			storyManager.inventory.AddPart ("string", "yarn");
			storyManager.inventory.AddPart ("tail", "yarn");
			print ("Acquired kite yarn.");
			this.gameObject.SetActive(false);
			tempKiteMaker();
			break;
		}
	}
}
