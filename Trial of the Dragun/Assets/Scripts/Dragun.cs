using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragun : MonoBehaviour {

	[SerializeField] private List<DragunBody> body;

	void Start () {
		
	}
	

	void Update () {
		
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
