using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float playerSpeed = 10;

	private Rigidbody2D rb;

	private float horInput;
	private float verInput;
	private float fireInput;


	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
	}


	void Update () {
		checkInput ();

		Move ();
	}


	//Genral Control for Scene Management________________________________________________________________________________________________________________________________________________________________
	void checkInput () {
		horInput = Input.GetAxisRaw ("Horizontal");
		verInput = Input.GetAxisRaw ("Vertical");
		fireInput = Input.GetAxisRaw ("Fire1");
	}

	void resetInput () {
		horInput = 0;
		verInput = 0;
		fireInput = 0;
	}

	public void playerDisable () {
		this.enabled = false;
		resetInput ();
	}

	public void playerEnable () {
		this.enabled = true;
	}

	//Movement_____________________________________________________________________________________________________________________________________________________________________________________________
	void Move () {
		Vector2 move = Vector2.zero;
		float hor = 0;
		float ver = 0;
		if (horInput < 0) {
			hor = horInput - 1;
		} if (horInput > 0) {
			hor = horInput + 1;
		}

		if (verInput < 0) {
			ver = verInput - 1;
		}if (verInput > 0) {
			ver = verInput + 1;
		}

		move = new Vector2(hor/2, ver/2);
		if (move.magnitude > 1) {
			move.Normalize();
		}
		
		rb.velocity = playerSpeed * move;
	}

}
