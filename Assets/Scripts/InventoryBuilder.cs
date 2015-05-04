using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryBuilder : MonoBehaviour {

	public GameObject slotPrefab;

	private GameObject gameManager;
	private ForestProgression progression;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("_GameManager");
		if (gameManager == null) {
			Debug.LogError ("Starting workbench without game manager. Can't populate inventory");
		} else {
			progression = gameManager.GetComponent<ForestProgression>();
			foreach(string type in KiteBuilder.PARTS_LIST){
				//Get all parts of a type in your inventory
				List<string> partsList = progression.inventory.GetParts(type);
				if(partsList != null) { //Found parts
					foreach(string name in progression.inventory.GetParts(type)) {
						//Create the part
						BuildSlot(name);
					}
				}else{//Didn't find parts
//					BuildSlot (null);
				}
			}
		}
	}

	void BuildSlot (string itemName) {

		GameObject actualSlot =  Instantiate(slotPrefab);

		switch(itemName){
		case GizmoPrefabs.DiamonClothName:
			actualSlot.GetComponent<GizmoSlot>().SetGizmoPrefab(GizmoPrefabs.DiamondClothPrefab);
			break;
		case GizmoPrefabs.LongStickName:
			actualSlot.GetComponent<GizmoSlot>().SetGizmoPrefab(GizmoPrefabs.LongStickPrefab);
			break;
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
		default:
			break;
		}

		if(actualSlot != null) {
			actualSlot.GetComponent<RectTransform>().SetParent(gameObject.GetComponent<RectTransform>());
		}

	}
}
