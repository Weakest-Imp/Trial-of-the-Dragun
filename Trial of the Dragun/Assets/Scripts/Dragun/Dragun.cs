using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragun : MonoBehaviour {

	[SerializeField] private List<DragunBody> body;

	[SerializeField] private int maxHealth;
	private int health;

	void Start () {
		health = maxHealth;
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

}
