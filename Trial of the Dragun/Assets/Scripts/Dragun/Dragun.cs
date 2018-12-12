using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragun : MonoBehaviour {

	[SerializeField] private List<DragunBody> body;
	[SerializeField] private List<DragunClaw> guns;

	[SerializeField] private int maxHealth;
	private int health;

	void Start () {
		health = maxHealth;

		StartCoroutine(AllOutBarrage ());
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
		foreach (DragunBody part in body) {
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
