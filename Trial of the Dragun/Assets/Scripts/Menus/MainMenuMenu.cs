using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMenu : MonoBehaviour {

	[SerializeField] List<GameObject> pointers;
	private int pointer = 0;
	private int pointerLimit;

	[SerializeField] GameObject controlCanvas;
	private bool isControlCanvas = false;

	[SerializeField] AudioClip MainMenuBGM;

	private float verInput;
	private bool verPressed = false;
	private float fireInput;
	private bool firePressed = false;

	void Start () {
		pointerLimit = pointers.Count - 1;
		pointer = pointerLimit;

		controlCanvas.SetActive (false);

		UpdateDisplay ();
		SoundManager.Instance.PlayBGM (MainMenuBGM);
	}


	void Update () {
		InputDetection ();

		if (!isControlCanvas) { 
			Confirm ();
			MovePointer ();
		} else {
			QuitControls ();
		}
	}

	//General functions of menus______________________________________________________________
	void InputDetection () {
		verInput = Input.GetAxisRaw ("Vertical");

		//To allow Enter as a confirmation key, as well as fire
		fireInput = Input.GetAxisRaw ("Fire1");
		if (Input.GetKeyDown (KeyCode.Return)) {
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
		if (fireInput == 0) {
			firePressed = false;
		}
		if (fireInput == 1) {
			if (!firePressed) {
				switch (pointer) {
				case 1:
					DragunScene ();
					break;
				case 0: 
					Controls ();
					break;
				default:
					Debug.Log ("Pointer out of bounds");
					break;
				}
			}
			firePressed = true;
		}
	}

	void DragunScene () {
		GameManager.Instance.DragunScene ();
	}

	void Controls () {
		controlCanvas.SetActive (true);
		isControlCanvas = true;
	}

	void QuitControls () {
		if (fireInput == 0) {
			firePressed = false;
		}
		if (fireInput == 1) {
			if (!firePressed) {
				controlCanvas.SetActive (false);
				isControlCanvas = false;
			}
			firePressed = true;
		}
	}

}
