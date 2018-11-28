using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragunSceneManager : MonoBehaviour {

	public static DragunSceneManager Instance { get; private set; }

	[SerializeField] private GameObject player;
	private PlayerController playerController;
	[SerializeField] private GameObject dragun;
	private Dragun dragunController;



	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy (gameObject);
		}

		playerController = player.GetComponent<PlayerController> ();
		dragunController = dragun.GetComponent<Dragun> ();
	}

	public void GameOver () {
		playerController.playerDisable ();
		//GameOver screen
		Debug.Log("game over");
	}

	public GameObject GetPlayer () {
		return this.player;
	}


}
