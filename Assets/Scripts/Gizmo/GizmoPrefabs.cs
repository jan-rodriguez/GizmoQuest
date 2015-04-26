using UnityEngine;
using System.Collections;

public class GizmoPrefabs : MonoBehaviour{

	public const string DiamonClothName = "diamond-cloth";
	public const string LongStickName = "Colored-Stick";
	public const string ClothName = "Cloth";
	public const string PenName = "Pen";
	public const string StrawName = "Straw";

	public static GameObject DiamondClothPrefab;
	public static GameObject LongStickPrefab;
	public static GameObject ClothPrefab;
	public static GameObject PenPrefab;
	public static GameObject StrawPrefab;

	void Awake () {
		DiamondClothPrefab = Resources.Load(DiamonClothName) as GameObject;
		LongStickPrefab = Resources.Load(LongStickName) as GameObject;
		ClothPrefab = Resources.Load(ClothName) as GameObject;
		PenPrefab = Resources.Load (PenName) as GameObject;
		StrawPrefab = Resources.Load (StrawName) as GameObject;
	}

}
