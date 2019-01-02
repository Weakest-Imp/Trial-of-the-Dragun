using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosions : MonoBehaviour {

	[SerializeField] private List<GameObject> explos;
	[SerializeField] private float offset = 0.33f;


	void OnEnable () {
		StartCoroutine (Boom ());	
	}

	IEnumerator Boom () {
		for (int i = 0; i < explos.Count; i++) {
			explos [i].SetActive (false);
		}

		for (int i = 0; i < explos.Count; i++) {
			explos [i].SetActive (true);
			yield return new WaitForSeconds (offset);
		}
	}

}
