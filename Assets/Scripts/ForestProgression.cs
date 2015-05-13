using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForestProgression : MonoBehaviour {

	public static int START_SCREEN_LVL = 0;
	public static int WORKBENCH_LVL = 1;
	public static int AIRFIELD_LVL = 2;
	public static int SAVANNAH_LVL = 3;
	public static int CLIFF_LVL = 4;

	public static int previousLevel;

	public class GizmoBuilder {
		private Dictionary<string, List<string>> partsHeld;

		public GizmoBuilder() {
			partsHeld = new Dictionary<string, List<string>>();
		}
		
		public void AddPart(string partType, string partName) {
			List<string> partList;
			if (!partsHeld.TryGetValue (partType, out partList)) {
				partsHeld.Add (partType, partList = new List<string>());
			}
			partList.Add (partName);
		}
		
		public bool HavePart(string partType) {
			return partsHeld.ContainsKey (partType);
		}
		
		public bool HaveAllKiteParts() {
			foreach (string part in KiteBuilder.PARTS_LIST) {
				if (!partsHeld.ContainsKey (part)) {
					return false;
				}
			}
			return true;
		}

		public bool HaveAllBanjoPars() {
			foreach (string part in BanjoBuilder.PARTS_LIST) {
				if (!partsHeld.ContainsKey (part)) {
					return false;
				}else if(part == BanjoBuilder.RUBBERBAND) {
					//Make sure we have 3 rubberbands
					if(partsHeld[part].Count != 3){
						return false;
					}
				}
			}
			return true;
		}
		
		public List<string> GetParts(string partType) {
			List<string> returnList = new List<string>();
			List<string> partList;
			if (partsHeld.TryGetValue (partType, out partList)) {
				foreach (string elementName in partList) {
					returnList.Add (elementName);
				}
				return returnList;
			}
			return null;
		}

		public Dictionary<string, List<string>> GetPartsHeld() {
			return partsHeld;
		}
	}

	public GizmoBuilder inventory;

	public string gizmoToBuild;

	//Scene 1: Dodo
	private bool metDodo = false;
	private bool kitePrint = false;
	private bool kite = false;

	//Scene 2: Lake
	private bool seenLake = false;

	//Scene 4: Passing the Lion
	private bool banjoPrint = false;
	private bool ladderPrint = false;
	private bool ladder = false;
	private bool banjo = false;
	private bool collectedTissueBox = false;
	private bool collectedPaperTowelRoll = false;

	//Scene 5: Dropping the Ball
	private bool slingshotPrint = false;
	private bool slingshot = false;
	private bool boulderCleared = false;

	// Use this for initialization
	void Start () {
		inventory = new GizmoBuilder ();
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void meetDodo() {
		metDodo = true;
	}

	public bool haveMetDodo() {
		return metDodo;
	}

	// Kite-related getters/setters/accessers
	public bool getKitePrint() {
		if (kitePrint) {
			return false;
		}
		kitePrint = true;
		return true;
	}

	public bool haveKitePrint() {
		return kitePrint;
	}
	
	public void makeKite() {
		kite = true;
	}

	public bool haveKite() {
		return kite;
	}
	
	public void seeLake() {
		seenLake = true;
	}

	//Ladder-related getters/setters/accessors
	public bool getLadderPrint() {
		if (ladderPrint) {
			return false;
		}
		ladderPrint = true;
		return true;
	}
	
	public bool haveLadderPrint() {
		return ladderPrint;
	}

	public void makeLadder() {
		ladder = true;
	}
	
	public bool haveLadder() {
		return ladder;
	}

	//Banjo-related getters/setters/accessors
	public bool getBanjoPrint() {
		if (banjoPrint) {
			return false;
		}
		banjoPrint = true;
		return true;
	}
	
	public bool haveBanjoPrint() {
		return banjoPrint;
	}

	public void makeBanjo() {
		banjo = true;
	}
	
	public bool haveBanjo() {
		return banjo;
	}

	public void collectTissueBox () {
		collectedTissueBox = true;
	}

	public bool haveTissueBox () {
		return collectedTissueBox;
	}

	public void collectPaperTowelRoll () {
		collectedPaperTowelRoll = true;
	}

	public bool havePaperTowelRoll () {
		return collectedPaperTowelRoll;
	}

	//Slingshot-related getters/setters/accessors
	public bool getSlingshotPrint() {
		if (slingshotPrint) {
			return false;
		}
		slingshotPrint = true;
		return true;
	}
	
	public bool haveSlingshotPrint() {
		return slingshotPrint;
	}
	
	public void makeSlingshot() {
		slingshot = true;
	}
	
	public bool haveSlingshot() {
		return slingshot;
	}

	public void clearBoulder() {
		boulderCleared = true;
	}

	private void HideCollectedItems() {
		if(inventory.GetPartsHeld().Values != null){
			foreach(List<string> listOfParts in inventory.GetPartsHeld().Values){
				foreach(string part in listOfParts) {
					GameObject go = GameObject.Find(part);
					if(go != null) {
						DestroyObject(go);
					}
				}
			}
		}

	}

	private void FinishKiteLevel() {
		if (kite) {
			GameObject kiteObject = GameObject.Find ("Kite");
			if (kiteObject != null) {
				kiteObject.GetComponent<SpriteRenderer>().enabled = true;
			}

			GameObject dodo = GameObject.Find ("Dodo");
			if (dodo != null) {
				dodo.GetComponent<DodoController>().startDodoKite();
			}
		}
	}

	void OnLevelWasLoaded(int level) {
		if(level != 1) {
			previousLevel = level;
		}

		HideCollectedItems();
		if (level == AIRFIELD_LVL) {
			FinishKiteLevel();
		}

	}
}
