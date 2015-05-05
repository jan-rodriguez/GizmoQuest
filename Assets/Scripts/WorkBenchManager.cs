using UnityEngine;
using System.Collections;

public class WorkBenchManager : MonoBehaviour {

	private GameObject kiteBuilderPrefab;
	private GameObject banjoBuilderPrefab;

	void Awake()
	{
		kiteBuilderPrefab = Resources.Load ("KiteBuilder") as GameObject;
		banjoBuilderPrefab = Resources.Load ("BanjoBuilder") as GameObject;

		SetupBuildArea (GizmoPrefabs.BANJO);
	}

	void SetupBuildArea (string partToBuild) {
		switch (partToBuild) {
		case GizmoPrefabs.KITE:
			Instantiate(kiteBuilderPrefab);
			break;
		case GizmoPrefabs.BANJO:
			Instantiate(banjoBuilderPrefab);
			break;
		}
	}
	
}
