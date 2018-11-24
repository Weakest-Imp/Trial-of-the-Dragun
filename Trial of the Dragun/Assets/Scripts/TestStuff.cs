using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStuff : MonoBehaviour {

	private Rigidbody2D rb;

	// Use this for initialization
//	void Start () {
//		rb = this.GetComponent<Rigidbody2D> ();
//		rb.velocity = new Vector2 (1, 0);
//	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += new Vector3 (0.1f, 0, 0);
	}

	void OnTriggerEnter2D (Collider2D other) {
		Debug.Log ("There's something ELSE!");
		if (other.gameObject.tag == "DraGun") {
			Debug.Log ("IT'S THE DRAGUN!!!!!!");
		}
	}
}
