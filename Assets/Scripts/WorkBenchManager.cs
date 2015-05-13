using UnityEngine;
using System.Collections;

public class WorkBenchManager : MonoBehaviour {

	private GameObject kiteBuilderPrefab;
	private GameObject banjoBuilderPrefab;
	private GameObject slingshotBuilderPrefab;

	void Awake()
	{
		kiteBuilderPrefab = Resources.Load ("KiteBuilder") as GameObject;
		banjoBuilderPrefab = Resources.Load ("BanjoBuilder") as GameObject;
		slingshotBuilderPrefab = Resources.Load ("SlingshotBuilder") as GameObject;

		SetupBuildArea (GameManagerManager.forestProgression.gizmoToBuild);
	}

	void SetupBuildArea (string partToBuild) {
		switch (partToBuild) {
		case GizmoPrefabs.KITE:
			Instantiate(kiteBuilderPrefab);
			break;
		case GizmoPrefabs.BANJO:
			Instantiate(banjoBuilderPrefab);
			break;
		case GizmoPrefabs.SLINGSHOT:
			Instantiate(slingshotBuilderPrefab);
			break;
		}
	}
	
}
