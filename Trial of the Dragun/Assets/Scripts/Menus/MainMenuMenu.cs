using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMenu : MonoBehaviour {

	[SerializeField] List<GameObject> pointers;
	private int pointer = 0;
	private int pointerLimit;

	private float verInput;
	private bool verPressed = false;
	private float fireInput;

	void Start () {
		pointerLimit = pointers.Count - 1;
		pointer = pointerLimit;

		UpdateDisplay ();
	}


	void Update () {
		InputDetection ();

		Confirm ();
		MovePointer ();
	}

	//General functions of menus______________________________________________________________
	void InputDetection () {
		verInput = Input.GetAxisRaw ("Vertical");

		//To allow Enter as a confirmation key, as well as fire
		fireInput = Input.GetAxisRaw ("Fire1");
		if (Input.GetKeyDown (KeyCode.KeypadEnter)) {
			fireInput = 1;
		}
	}

	void MovePointer () {
		if (verInput == 0) {
			verPressed = false;
		}
		if (verInput == 1) {
			if (!verPressed) {
				PointerUp ();
				UpdateDisplay ();
			}
			verPressed = true;
		}
		if (verInput == -1) {
			if (!verPressed) {
				PointerDown ();
				UpdateDisplay ();
			}
			verPressed = true;
		}		
	}

	void PointerUp () {
		pointer++;
		if (pointer > pointerLimit) {
			pointer = 0;
		}
	}
	void PointerDown () {
		pointer--;
		if (pointer < 0) {
			pointer = pointerLimit;
		}
	}

	void UpdateDisplay() {
		for (int i = 0; i <= pointerLimit; i++) {
			if (pointer == i) {
				pointers [i].SetActive (true);
			} else {
				pointers [i].SetActive (false);
			}
		}
	}

	//Depends on menu__________________________________________________________________________
	void Confirm () {
		if (fireInput == 1) {
			switch (pointer) {
			case 2:
				DragunScene ();
				break;
			case 1: 
				Controls ();
				break;
			case 0: 
				QuitGame ();
				break;
			default:
				Debug.Log ("Pointer out of bounds");
				break;
			}
		}
	}

	void DragunScene () {
		GameManager.Instance.DragunScene ();
	}

	void Controls () {
		Debug.Log ("Make controls");
	}

	void QuitGame () {
		GameManager.Instance.QuitGame (); 
	}

}
