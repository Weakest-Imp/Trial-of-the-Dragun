using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraGun : MonoBehaviour {

	[SerializeField] private WiggleChild head;

	// Use this for initialization
	void Start () {
		head.PositionInitialize ();
	}
	
	// Update is called once per frame
	void Update () {
		head.MoveWiggle (Time.deltaTime);
	}
}
