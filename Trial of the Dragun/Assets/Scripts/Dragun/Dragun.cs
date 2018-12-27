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
	int previousAttack = -1;

	void Start () {
		health = maxHealth;

//		flag = 1.5f;
		StartCoroutine(DragunBattle ());
	}
	

	void Update () {
		
	}

	//Interactions__________________________________________________________________________________________________________________________________________________________________________________
	public void TakeDamage (int damage) {
		health -= damage;
		if (health < 1) {
			StopDragun ();
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
		yield return null;
		if (flag <= 0) {
			//Introduces Straight shots
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
				FirstHalf ();
				inAttack = true;
			}
			yield return null;
		}

		while (flag <= 1.5f) {
			//Introduces Down shots
			if (!inAttack) {
				StartCoroutine (SecondHalfAttack0 ());
				inAttack = true;
				previousAttack = 0;
				flag = 2;
			}
			yield return null;
		}

		while (flag <= 2) {
			//Second random phase until "death"
			if (health < 1) {
				flag = 2.5f;
			}
			if (!inAttack) {
				SecondHalf ();
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
		yield return new WaitForSeconds (2);
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
		yield return new WaitForSeconds (3);
		inAttack = false;
	}

	//Random patterns for first half_____________________________________________________________
	void FirstHalf () {
		int pattern = Random.Range (0, 4);
		while (pattern == previousAttack) {
			pattern = Random.Range (0, 4);
		}
		StartCoroutine ("FirstHalfAttack" + pattern);
		previousAttack = pattern;
	}

	IEnumerator FirstHalfAttack0 () {
		foreach (DragunClaw gun in guns) {
			gun.UpShot ();
		}
		yield return new WaitForSeconds(4);
		inAttack = false;
	}

	IEnumerator FirstHalfAttack1 () {
		guns [0].UpShot ();
		yield return new WaitForSeconds (1);
		guns [1].UpShot ();
		yield return new WaitForSeconds (1);
		guns [2].StraightShot ();
		yield return new WaitForSeconds (3);
		inAttack = false;
	}

	IEnumerator FirstHalfAttack2 () {
		guns [1].UpShot ();
		yield return new WaitForSeconds (1);
		guns [0].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [1].StraightShot ();
		yield return new WaitForSeconds (3);
		inAttack = false;
	}

	IEnumerator FirstHalfAttack3 () {
		guns [0].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [1].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [2].StraightShot ();
		yield return new WaitForSeconds (3);
		inAttack = false;
	}

	//Random patterns for second half____________________________________________________________
	void SecondHalf () {
		int pattern = Random.Range (0, 4);
		while (pattern == previousAttack) {
			pattern = Random.Range (0, 4);
		}
		StartCoroutine ("SecondHalfAttack" + pattern);
		previousAttack = pattern;
	}

	IEnumerator SecondHalfAttack0 () {
		foreach (DragunClaw gun in guns) {
			gun.UpShot ();
		}
		yield return new WaitForSeconds (1.5f);

		foreach (DragunClaw gun in guns) {
			gun.DownShot ();
		}
		yield return new WaitForSeconds(4);
		inAttack = false;
	}

	IEnumerator SecondHalfAttack1 () {
		guns [2].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [2].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [0].DownShot ();
		guns [1].UpShot ();
		yield return new WaitForSeconds (3);
		inAttack = false;
	}

	IEnumerator SecondHalfAttack2 () {
		guns [0].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [1].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [2].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [1].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [0].StraightShot ();
		yield return new WaitForSeconds (3);
		inAttack = false;
	}

	IEnumerator SecondHalfAttack3 () {
		guns [0].DownShot ();
		guns [2].UpShot ();
		yield return new WaitForSeconds (1);
		guns [2].DownShot ();
		guns [0].UpShot ();
		yield return new WaitForSeconds (1);
		guns [1].StraightShot ();
		yield return new WaitForSeconds(3);
		inAttack = false;
	}


}
