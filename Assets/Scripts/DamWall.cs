using UnityEngine;
using System.Collections;

public class DamWall : MonoBehaviour {

	public Sprite brokenSprite;
	public GameObject[] waterSprites;

	public void BreakWall () {
		GetComponent<SpriteRenderer> ().sprite = brokenSprite;
		StartCoroutine (FlowWater ());
	}

	private IEnumerator FlowWater () {
		for (int i = 0; i < waterSprites.Length; i++) {
			waterSprites[i].SetActive(true);
			yield return new WaitForSeconds(1f);
		}
		GoToSavannah ();
		yield return null;
	}

	void GoToSavannah () {
		StartCoroutine(GameManagerManager.manager.GetComponent<SceneManager>().GoToCompletedSavannah());
	}
}
