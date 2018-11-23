using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

	[SerializeField] float slideSpeed;
	[SerializeField] float speedChangeTime;
	float scale;

	void Start () 
	{
		scale = this.transform.parent.localScale.x;
	}

	void Update () {
		Slide ();
	}
	
	// Slides the background at requested speed
	void Slide () {
		float x = this.transform.position.x + slideSpeed * Time.deltaTime;
		float y = this.transform.position.y;
		float z = this.transform.position.z;
		Overflow (x, y, z);
	}

	// Teleports the background back once it goes out of screen
	void Overflow (float x, float y, float z)
	{
		if (x > 5.25F * scale) {
			this.transform.position = new Vector3 (x - 10.24F * scale, y, z);
		} else {
			if (x < -5.25F * scale) {
				this.transform.position = new Vector3 (x + 10.24F * scale, y, z);
			} else {
				this.transform.position = new Vector3 (x, y, z);
			}
		}
	}

	public void speedChange (float newSpeed) {
		StartCoroutine (speedChangeCoroutine(newSpeed));
	}

	IEnumerator speedChangeCoroutine (float newSpeed)
	//Changes the speed smoothly
	{
		float change = newSpeed - slideSpeed;
		float time = 0;
		change = change / speedChangeTime;
		while (time < speedChangeTime) {
			yield return null;
			slideSpeed += Time.deltaTime * change;
			time += Time.deltaTime;
		}
		slideSpeed = newSpeed;
	}
}
