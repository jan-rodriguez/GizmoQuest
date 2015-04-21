using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KiteBuilder : MonoBehaviour {

	private Dictionary<string, bool> partsDict = new Dictionary<string,bool> ();

	// Use this for initialization
	void Start () {
		partsDict.Add ("long_rod", false);
		partsDict.Add ("cloth", false);
		partsDict.Add ("string", false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		print (other.tag);
		//Check if the other is a compatible part
		if(partsDict.ContainsKey(other.tag)) {
			print ("Got " + other.tag);
		}
	}
}
