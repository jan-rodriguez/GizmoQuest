using UnityEngine;
using System.Collections;

public class LandingProgression : MonoBehaviour {
	private ForestProgression storyManager;
	private GameObject toLake;

	// Use this for initialization
	void Start () {
		storyManager = GameObject.Find ("_GameManager").GetComponent<ForestProgression>();
		toLake = GameObject.Find ("Landing to Lake");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void tempKiteMaker() {
		if (storyManager.inventory.HaveAllKiteParts ()) {
			storyManager.makeKite ();
			toLake.GetComponent<SpriteRenderer>().enabled = true;
			toLake.GetComponent<BoxCollider2D>().enabled = true;
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
