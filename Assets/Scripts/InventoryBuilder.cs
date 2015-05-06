using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryBuilder : MonoBehaviour {

	public GameObject slotPrefab;

	private GameObject gameManager;
	private ForestProgression progression;

	// Use this for initialization
	void Start () {
		gameManager = GameManagerManager.manager;
		if (gameManager == null) {
			Debug.LogError ("Starting workbench without game manager. Can't populate inventory");
		} else {
			progression = GameManagerManager.forestProgression;

			string[] partsToBuildList = null;

			//TODO: GET CORRECT PART TO BUILD HERE
			string partToBuild = GizmoPrefabs.BANJO;

			switch (partToBuild) {
			case GizmoPrefabs.KITE:
				partsToBuildList = KiteBuilder.PARTS_LIST;
				break;
			case GizmoPrefabs.BANJO:
				partsToBuildList = BanjoBuilder.PARTS_LIST;
				break;
			case GizmoPrefabs.SHOVEL:
				break;
			}
			foreach(string type in partsToBuildList){
				//Get all parts of a type in your inventory
				List<string> partsList = progression.inventory.GetParts(type);
				if(partsList != null) { //Found parts
					foreach(string name in progression.inventory.GetParts(type)) {
						//Create the part
						BuildSlot(name);
					}
				}else{//Didn't find parts
					BuildSlot (null);
				}
			}
		}
	}

	void BuildSlot (string itemName) {

		GameObject actualSlot =  Instantiate(slotPrefab);

		switch(itemName){
		case GizmoPrefabs.ClothName:
			actualSlot.GetComponent<GizmoSlot>().SetGizmoPrefab(GizmoPrefabs.ClothPrefab);
			break;
		case GizmoPrefabs.PenName:
			actualSlot.GetComponent<GizmoSlot>().SetGizmoPrefab(GizmoPrefabs.PenPrefab);
			break;
		case GizmoPrefabs.StrawName:
			actualSlot.GetComponent<GizmoSlot>().SetGizmoPrefab(GizmoPrefabs.StrawPrefab);
			break;
		case GizmoPrefabs.StringName:
			actualSlot.GetComponent<GizmoSlot>().SetGizmoPrefab(GizmoPrefabs.StringPrefab);
			break;
		case GizmoPrefabs.PaperTowelRollName:
			actualSlot.GetComponent<GizmoSlot>().SetGizmoPrefab(GizmoPrefabs.PaperTowelRollPrefab);
			break;
		case GizmoPrefabs.Vine1Name:
			actualSlot.GetComponent<GizmoSlot>().SetGizmoPrefab(GizmoPrefabs.Vine1Prefab);
			break;
		case GizmoPrefabs.Vine2Name:
			actualSlot.GetComponent<GizmoSlot>().SetGizmoPrefab(GizmoPrefabs.Vine2Prefab);
			break;
		case GizmoPrefabs.Vine3Name:
			actualSlot.GetComponent<GizmoSlot>().SetGizmoPrefab(GizmoPrefabs.Vine3Prefab);
			break;
		case GizmoPrefabs.TissueBoxName:
			actualSlot.GetComponent<GizmoSlot>().SetGizmoPrefab(GizmoPrefabs.TissueBoxPrefab);
			break;
		default:
			break;
		}

		if(actualSlot != null) {
			actualSlot.GetComponent<RectTransform>().SetParent(gameObject.GetComponent<RectTransform>());
		}

	}
}
