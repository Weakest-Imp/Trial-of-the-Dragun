using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance { get; private set; }


	void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	public void QuitGame () {
		Application.Quit (); 
	}

	public void MainMenu () {
		SceneManager.LoadScene ("MainMenu");
	}
	public void DragunScene () {
		SceneManager.LoadScene ("DragunScene");
	}


}
