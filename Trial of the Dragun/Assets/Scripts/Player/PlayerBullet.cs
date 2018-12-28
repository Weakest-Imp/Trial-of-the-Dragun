using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

	[SerializeField] private float speed;

	private Rigidbody2D rb;


	void Awake () {
		rb = this.GetComponent<Rigidbody2D> ();
		rb.velocity = new Vector2 (speed, 0);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "DraGun") {
			Explosion ();
		}
		if (other.gameObject.tag == "DraGunClaw") {
			Explosion ();
		}
	}

	void Explosion () {
		Destroy (this.gameObject);
	}

}
