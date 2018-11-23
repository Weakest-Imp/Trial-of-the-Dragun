using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) {
		Debug.Log ("There's something...");
		if (other.gameObject.tag == "DraGun") {
			Debug.Log ("IT'S THE DRAGUN!!!!!!");
		}
	}
}
