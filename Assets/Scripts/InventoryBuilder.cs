using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryBuilder : MonoBehaviour {

	public GameObject slotPrefab;

	// Use this for initialization
	void Start () {
		BuildSlot(GizmoPrefabs.LongStickName);
		BuildSlot(GizmoPrefabs.ClothName);
		BuildSlot(GizmoPrefabs.PenName);
		BuildSlot(GizmoPrefabs.StrawName);
		BuildSlot(GizmoPrefabs.StringName);
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
		}

		if(actualSlot != null) {
			actualSlot.GetComponent<RectTransform>().SetParent(gameObject.GetComponent<RectTransform>());
		}

	}
}
