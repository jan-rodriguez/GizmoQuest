using UnityEngine;
using System.Collections;

public class GizmoPrefabs : MonoBehaviour{

	public const string KITE = "kite";
	public const string BANJO = "banjo";
	public const string SHOVEL = "shovel";

	//KITE
	public const string ClothName = "Cloth";
	public const string PenName = "Pen";
	public const string StrawName = "Straw";
	public const string StringName = "String";

	public static GameObject ClothPrefab;
	public static GameObject PenPrefab;
	public static GameObject StrawPrefab;
	public static GameObject StringPrefab;

	//BANJO
	public const string TissueBoxName = "tissuebox";
	public const string Vine1Name = "vine1";
	public const string Vine2Name = "vine2";
	public const string Vine3Name = "vine3";
	public const string PaperTowelRollName = "papertowelroll";

	public static GameObject TissueBoxPrefab;
	public static GameObject Vine1Prefab;
	public static GameObject Vine2Prefab;
	public static GameObject Vine3Prefab;
	public static GameObject PaperTowelRollPrefab;



	void Awake () {
		string itemToBuild = BANJO;

		switch (itemToBuild) {
		case KITE:
			ClothPrefab = Resources.Load(ClothName) as GameObject;
			PenPrefab = Resources.Load (PenName) as GameObject;
			StrawPrefab = Resources.Load (StrawName) as GameObject;
			StringPrefab = Resources.Load(StringName) as GameObject;
			break;
		case BANJO:
			TissueBoxPrefab = Resources.Load (TissueBoxName) as GameObject;
			Vine1Prefab = Resources.Load (Vine1Name) as GameObject;
			Vine2Prefab = Resources.Load (Vine2Name) as GameObject;
			Vine3Prefab = Resources.Load (Vine3Name) as GameObject;
			PaperTowelRollPrefab = Resources.Load (PaperTowelRollName) as GameObject;
			break;
		case SHOVEL:
			break;
		}

	}

}
