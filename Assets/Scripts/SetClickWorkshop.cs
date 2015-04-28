using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetClickWorkshop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Button b = gameObject.GetComponent<Button>();
		SceneManager scenes = GameManagerManager.manager.GetComponent<SceneManager>();
		b.onClick.AddListener(() => scenes.GoToWorkshop());
	}
}
