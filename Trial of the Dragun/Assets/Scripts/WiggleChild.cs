using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleChild : MonoBehaviour {

	[SerializeField] float wiggleAmp = 1;
	[SerializeField] float wiggleTime;
	float wiggleTimeFactor;
	[SerializeField] float wiggleOffSet;

	[SerializeField] private Transform parent;
	private Rigidbody2D parentRb;
	[SerializeField] private WiggleChild child;
	[SerializeField] private bool isFinal = true;
	private Rigidbody2D rb;

	private float time;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
		time = wiggleOffSet * Mathf.PI;
		wiggleTimeFactor = 2 * Mathf.PI / wiggleTime;
		parentRb = parent.GetComponent<Rigidbody2D> ();
		//parent = this.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
	}

//	void MoveWiggle ()
//	{
//		float newY = parent.position.y + wiggleAmp * Mathf.Sin (time * wiggleTimeFactor);
//		float x = this.transform.position.x;
//		float z = this.transform.position.z;
//		Vector3 pos = new Vector3 (x, newY, z);
//
//		this.transform.position = pos; 
//	}

	public void MoveWiggle (float deltaTime) {
		time += deltaTime;
		float wiggleSpeed = parentRb.velocity.y + wiggleAmp * Mathf.Cos (-1 * time * wiggleTimeFactor);

		rb.velocity = new Vector2 (0, wiggleSpeed);
		if (!isFinal) {
			child.MoveWiggle (deltaTime);
		}
	}

}
