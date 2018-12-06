using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragunBody : MonoBehaviour {

	[SerializeField] float wiggleAmp = 0.5f; //head = 3
	//for phase 2
	[SerializeField] float wiggleAmp2 = 1; //head = 3
	private float amplitude;

	[SerializeField] float wiggleTime = 1; //head = 3
	//for phase 2, head only
	[SerializeField] float wiggleTime2 = 1; //head = 2
	float wiggleTimeFactor;
	[SerializeField] float wiggleOffSet;

	[SerializeField] private Dragun dragun;
	[SerializeField] private Transform parent;
	[SerializeField] private int damage = 1;

	private float time;


	void Start () {
		amplitude = wiggleAmp;
		time = wiggleOffSet * Mathf.PI;
		wiggleTimeFactor = 2 * Mathf.PI / wiggleTime;
	}
	

	void Update () {
		MoveWiggle ();
		time += Time.deltaTime;
	}

	void MoveWiggle ()
	{
		float newY = parent.position.y + amplitude * Mathf.Sin (time * wiggleTimeFactor);
		float x = this.transform.position.x;
		float z = this.transform.position.z;
		Vector3 pos = new Vector3 (x, newY, z);

		this.transform.position = pos; 
	}

	public void Phase2 () {
		amplitude = wiggleAmp2;
//		wiggleTimeFactor = 2 * Mathf.PI / wiggleTime2;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "PlayerBullet") {
			Debug.Log ("Hurt animation");
			dragun.TakeDamage (damage);
		}
	}

	public void Stop () {
		this.enabled = false;
	}
	public void Restart () {
		this.enabled = true;
	}

}
