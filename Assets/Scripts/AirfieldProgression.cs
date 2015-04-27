using UnityEngine;
using System.Collections;

public class AirfieldProgression : MonoBehaviour {
	private ForestProgression storyManager;
	private GameObject toSavannah;
	
	// Use this for initialization
	void Start () {
		storyManager = GameObject.Find ("_GameManager").GetComponent<ForestProgression>();
		toSavannah = GameObject.Find ("Airfield to Savannah");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void tempKiteMaker() {
		if (storyManager.inventory.HaveAllKiteParts ()) {
			storyManager.makeKite ();
			toSavannah.GetComponent<SpriteRenderer>().enabled = true;
			toSavannah.GetComponent<BoxCollider2D>().enabled = true;
		}
	}
	
	// Good lord.
	void OnMouseDown() {
		switch (this.name) {
		case "Dodo":
			if (storyManager.getKitePrint ()) {
				print ("Acquired kite print.");
			} else {
				print ("Built kite.");
				tempKiteMaker ();
			}
			break;
		case "Wire":
			print ("Acquired wire.");
			storyManager.inventory.AddPart (KiteBuilder.STRING, "String");
			this.GetComponent<SpriteRenderer>().enabled = false;
			this.GetComponent<SpriteRenderer>().enabled = false;
			break;
		case "Cloth":
			print ("Acquired cloth.");
			storyManager.inventory.AddPart (KiteBuilder.CLOTH, "Cloth");
			this.GetComponent<SpriteRenderer>().enabled = false;
			this.GetComponent<SpriteRenderer>().enabled = false;
			break;
		case "Straw":
			print ("Acquired straw.");
			storyManager.inventory.AddPart (KiteBuilder.LONG_ROD, "Straw");
			this.GetComponent<SpriteRenderer>().enabled = false;
			this.GetComponent<SpriteRenderer>().enabled = false;
			break;
		case "Pen":
			print ("Acquired pen.");
			storyManager.inventory.AddPart (KiteBuilder.SHORT_ROD, "Pen");
			this.GetComponent<SpriteRenderer>().enabled = false;
			this.GetComponent<SpriteRenderer>().enabled = false;
			break;
		}
	}
}
