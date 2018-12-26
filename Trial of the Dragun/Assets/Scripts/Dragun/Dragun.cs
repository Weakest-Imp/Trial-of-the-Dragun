using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragun : MonoBehaviour {

	[SerializeField] private List<DragunBody> body;
	[SerializeField] private List<DragunClaw> guns;

	[SerializeField] private int maxHealth;
	private int health;

	private float flag = 0;
	private bool inAttack = false;

	void Start () {
		health = maxHealth;

		StartCoroutine(DragunBattle ());
	}
	

	void Update () {
		
	}

	//Interactions__________________________________________________________________________________________________________________________________________________________________________________
	public void TakeDamage (int damage) {
		health -= damage;
		if (health < 1) {
			Debug.Log ("Dead DraGun");
		}
	}


	//Body Management________________________________________________________________________________________________________________________________________________________________________________
	//Cutscenes_________________________________________________________________________________
	public void StopDragun () {
		this.StopDragunAttacks ();
		foreach (DragunBody part in body) {
			part.Stop ();
		}
	}
	public void StopDragunAttacks () {
		this.StopAllCoroutines ();
		foreach (DragunClaw part in guns) {
			part.Stop ();
		}
	}

	public void RestartDragun () {
		foreach (DragunBody part in body) {
			part.Restart ();
		}
	}

	public void StopDragunHead () {
		body [0].Stop ();
	}
	public void RestartDragunHead () {
		body [0].Restart ();
	}

	public void Phase2 (){
		foreach (DragunBody part in body) {
			part.Phase2 ();
		}
	}

	//Attacks_____________________________________________________________________________________
	IEnumerator DragunBattle () {
		if (flag <= 0) {
			//Introduces Straight shots
			yield return null;
			StartCoroutine (FirstAttack ());
			inAttack = true;
			flag = 0.5f;
			yield return null;
		}
		while (flag <= 0.5f) {
			//Introduces Up shots
			if (!inAttack) {
				StartCoroutine (SecondAttack ());
				inAttack = true;
				flag = 1;
			}
			yield return null;
		}

		//Starts acting randomly
		int halfLife = 1 + maxHealth / 2;
		while (flag <= 1) {
			if (health < halfLife) {
				flag = 1.5f;
			}
			if (!inAttack) {
				Debug.Log ("Afini");
//				StartCoroutine (FirstHalf ());
				inAttack = true;
			}
			yield return null;
		}
	}

	IEnumerator FirstAttack () {
		guns [1].StraightShot ();
		yield return new WaitForSeconds (2f);

		for (int i = 0; i < guns.Count; i++) {
			guns [i].StraightShot ();
			yield return new WaitForSeconds (1);
		}
		yield return new WaitForSeconds (1);
		inAttack = false;
	}

	IEnumerator SecondAttack () {
		guns [0].UpShot ();
		yield return new WaitForSeconds (2f);

		guns [2].UpShot ();
		yield return new WaitForSeconds (1);
		guns [1].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [0].UpShot ();
		yield return new WaitForSeconds (1);
		inAttack = false;
	}

//	IEnumerator FirstHalf () {
//
//	}

	IEnumerator AllOutBarrage () {
		for (int i = 0; i < guns.Count; i++) {
			StartCoroutine (RandomBarrage (i));
			yield return new WaitForSeconds (1);
		}
	}

	IEnumerator RandomBarrage (int claw) {
		while (true) {
			yield return new WaitForSeconds (4);
			int incline = Random.Range (-1, 2);
			switch (incline) {
			case -1:
				guns [claw].DownShot ();
				break;
			case 0:
				guns [claw].StraightShot ();
				break;
			case 1:
				guns [claw].UpShot ();
				break;
			default:
				Debug.Log ("Gros GROS blème dans la fonction random");
				break;
			}
		}
	}

}
