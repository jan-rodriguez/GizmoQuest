using UnityEngine;
using System.Collections;

public class DamSlingshot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (GameManagerManager.forestProgression.haveSlingshot ()) {
			GetComponent<SpriteRenderer>().enabled = true;
			GetComponent<Animation>().Play ();
			GameManagerManager.forestProgression.clearBoulder();
		}
	}
	
	void ShootRock() {
		GameObject rock = transform.GetChild (0).gameObject;
		rock.GetComponent<Animation> ().Play ();
		rock.GetComponent<SpriteRenderer> ().enabled = true;
	}
}
