using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleChild : MonoBehaviour {

	[SerializeField] float wiggleAmp = 1;
	[SerializeField] float wiggleTime;
	[SerializeField] float wiggleOffSet;
	float wiggleTimeFactor;
	float wiggleVeloAmp;

	[SerializeField] private Transform parent;
	private Rigidbody2D parentRb;
	[SerializeField] private WiggleChild child;
	[SerializeField] private bool isFinal = true;
	private Rigidbody2D rb;

	private float time;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
		time = -1 * wiggleOffSet * Mathf.PI;
		wiggleTimeFactor = 2 * Mathf.PI / wiggleTime;
		wiggleVeloAmp = wiggleAmp * wiggleTimeFactor;
		parentRb = parent.GetComponent<Rigidbody2D> ();
		//parent = this.transform.parent;

		//PositionInitialize ();
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
		float wiggleSpeed = parentRb.velocity.y + wiggleVeloAmp * Mathf.Cos (-1 * time * wiggleTimeFactor);

		rb.velocity = new Vector2 (0, wiggleSpeed);
		if (!isFinal) {
			child.MoveWiggle (deltaTime);
		}
	}

	public void PositionInitialize () {
		float pos = -1 * wiggleAmp * Mathf.Sin (wiggleTimeFactor * Mathf.PI * wiggleOffSet);
		pos += parent.position.y;

		float x = this.transform.position.x;
		float z = this.transform.position.z;

		this.transform.position = new Vector3 (x, pos, z);
		if (!isFinal) {
			child.PositionInitialize ();
		}
	}

}
