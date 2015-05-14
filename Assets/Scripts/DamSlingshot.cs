using UnityEngine;
using System.Collections;

public class DamSlingshot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (GameManagerManager.forestProgression.haveSlingshot ()) {
			GetComponent<SpriteRenderer>().enabled = true;
			GameManagerManager.forestProgression.clearBoulder();
		}
	}

	public void StartSlingshotAnim() {
		GetComponent<Animation>().Play ();
	}
	
	void ShootRock() {
		GameObject rock = transform.GetChild (0).gameObject;
		rock.GetComponent<Animation> ().Play ();
		rock.GetComponent<SpriteRenderer> ().enabled = true;
		rock.GetComponent<AudioSource>().Play();
	}
}
