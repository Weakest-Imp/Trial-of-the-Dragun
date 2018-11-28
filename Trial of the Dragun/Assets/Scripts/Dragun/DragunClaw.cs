﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragunClaw : MonoBehaviour {

	[SerializeField] private Dragun dragun;
	[SerializeField] private GameObject SmallGun;
	[SerializeField] private GameObject bullet;
	[SerializeField] private int damage = 2;


	[SerializeField] private float buildUpTimeStraight = 1;
	[SerializeField] private Vector3 positionDeviationStraight;

	[SerializeField] private float buildUpTimeCurved = 0.5f;
	[SerializeField] private Vector3 positionDeviationUp;
	[SerializeField] private Vector3 positionDeviationDown;

	void Start () {
		
	}
	

	void Update () {
		
	}

	//Interactions________________________________________________________________________________
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "PlayerBullet") {
			Debug.Log ("Hurt claw animation");
			dragun.TakeDamage (damage);
		}
	}



	//Shots___________________________________________________________________________________
	public void StraightShot () {
		StartCoroutine (StraightShotCoroutine ());
	}
	IEnumerator StraightShotCoroutine () {
		Debug.Log ("The gun spins 2 times");
		//Animation: spinning Gun
		yield return new WaitForSeconds (buildUpTimeStraight);

		GameObject bul = Instantiate (bullet, this.transform.position + positionDeviationStraight, this.transform.rotation);
		bul.GetComponent<DragunBullet> ().DefineIncline (0);
	}

	public void UpShot () {
		StartCoroutine (UpShotCoroutine ());
	}
	IEnumerator UpShotCoroutine () {
		Debug.Log ("The gun spins 1 time");
		//Animation: spinning Gun
		yield return new WaitForSeconds (buildUpTimeCurved);

		GameObject bul = Instantiate (bullet, this.transform.position + positionDeviationUp, this.transform.rotation);
		bul.GetComponent<DragunBullet> ().DefineIncline (1);
	}

	public void DownShot () {
		StartCoroutine (DownShotCoroutine ());
	}
	IEnumerator DownShotCoroutine () {
		Debug.Log ("The gun spins 1 time in the other way");
		//Animation: spinning Gun
		yield return new WaitForSeconds (buildUpTimeCurved);

		GameObject bul = Instantiate (bullet, this.transform.position + positionDeviationDown, this.transform.rotation);
		bul.GetComponent<DragunBullet> ().DefineIncline (-1);
	}

}