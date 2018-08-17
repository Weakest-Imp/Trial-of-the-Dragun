using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleChild : MonoBehaviour {

	[SerializeField] float wiggleAmp;
	[SerializeField] float wiggleTime;
	float wiggleTimeFactor;

	Transform parent;

	float time;

	// Use this for initialization
	void Start () {
		time = 0;
		wiggleTimeFactor = 2 * Mathf.PI / wiggleTime;
		parent = this.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		MoveWiggle (time);
		time += Time.deltaTime;
	}

	void MoveWiggle (float time)
	{
		float newY = parent.position.y + wiggleAmp * Mathf.Sin (time * wiggleTimeFactor);
		float x = this.transform.position.x;
		float z = this.transform.position.z;
		Vector3 pos = new Vector3 (x, newY, z);

		this.transform.position = pos; 
	}

}
