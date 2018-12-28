using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragunSceneManager : MonoBehaviour {

	public static DragunSceneManager Instance { get; private set; }

	[SerializeField] private GameObject player;
	private PlayerController playerController;

	[SerializeField] private GameObject dragun;
	private Dragun dragunController;

	[SerializeField] private GameObject gameOverCanvas;


	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy (gameObject);
		}

		playerController = player.GetComponent<PlayerController> ();
		dragunController = dragun.GetComponent<Dragun> ();
		gameOverCanvas.SetActive (false);
	}

	public GameObject GetPlayer () {
		return this.player;
	}

	public void GameOver () {
		playerController.playerDisable ();
		dragunController.StopDragunAttacks ();
		gameOverCanvas.SetActive (true);
		Debug.Log("game over");
	}



}
