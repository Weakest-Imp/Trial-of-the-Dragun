using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float playerSpeed;

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
		horInput = Input.GetAxis ("Horizontal");
		verInput = Input.GetAxis ("Vertical");
		fireInput = Input.GetAxis ("Fire1");
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
		Vector2 move = new Vector2 (horInput, verInput);
		if (move.magnitude > 1) {
			move = move / move.magnitude;
		}
		//move.Normalize ();
		rb.velocity = playerSpeed * move;
	}

}
