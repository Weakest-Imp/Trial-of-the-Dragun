using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragunClaw : MonoBehaviour {

	[SerializeField] private Dragun dragun;
	[SerializeField] private int damage = 2;

	void Start () {
		
	}
	

	void Update () {
		
	}


	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "PlayerBullet") {
			dragun.TakeDamage (damage);
			Debug.Log (damage);
		}
	}

}
