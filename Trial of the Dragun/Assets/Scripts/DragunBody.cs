using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragunBody : MonoBehaviour {

	[SerializeField] float wiggleAmp = 1;
	[SerializeField] float wiggleTime;
	float wiggleTimeFactor;
	[SerializeField] float wiggleOffSet;

	[SerializeField] private Transform parent;

	private float time;


	void Start () {
		time = wiggleOffSet * Mathf.PI;
		wiggleTimeFactor = 2 * Mathf.PI / wiggleTime;
	}
	

	void Update () {
		MoveWiggle ();
		time += Time.deltaTime;
	}

	void MoveWiggle ()
	{
		float newY = parent.position.y + wiggleAmp * Mathf.Sin (time * wiggleTimeFactor);
		float x = this.transform.position.x;
		float z = this.transform.position.z;
		Vector3 pos = new Vector3 (x, newY, z);

		this.transform.position = pos; 
	}

	public void Stop () {
		this.enabled = false;
	}
	public void Restart () {
		this.enabled = true;
	}

}
