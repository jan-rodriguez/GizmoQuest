using UnityEngine;
using System.Collections;

public class WallBreaker : MonoBehaviour {

	public DamWall wall;

	public void BreakWall() {
		wall.BreakWall ();
		Destroy (gameObject);
	}
}
