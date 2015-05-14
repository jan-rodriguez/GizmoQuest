using UnityEngine;
using System.Collections;

public class DamWall : MonoBehaviour {

	public Sprite brokenSprite;
	public GameObject[] waterSprites;
	public AudioClip waterSounds;

	public void BreakWall () {
		GetComponent<SpriteRenderer> ().sprite = brokenSprite;
		GetComponent<AudioSource>().Play();
		GetComponent<AudioSource>().PlayOneShot(waterSounds);
		StartCoroutine (FlowWater ());
	}

	private IEnumerator FlowWater () {
		for (int i = 0; i < waterSprites.Length; i++) {
			waterSprites[i].SetActive(true);
			yield return new WaitForSeconds(1f);
		}
		yield return null;
	}

}
