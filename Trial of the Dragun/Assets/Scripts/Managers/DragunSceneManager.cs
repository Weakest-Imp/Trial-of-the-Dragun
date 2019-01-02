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
	[SerializeField] private Background background1;
	[SerializeField] private Background background2;
	[SerializeField] private Image flash;


	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy (gameObject);
		}

		playerController = player.GetComponent<PlayerController> ();
		dragunController = dragun.GetComponent<Dragun> ();
		gameOverCanvas.SetActive (false);
		flash.gameObject.SetActive (false);
	}

	public GameObject GetPlayer () {
		return this.player;
	}

	public void GameOver () {
		playerController.playerDisable ();
		dragunController.StopDragunAttacks ();
		gameOverCanvas.SetActive (true);
	}

	public void FakeDeath () {
		playerController.playerDisable ();

		StartCoroutine (FakeDeathCoroutine ());
	}
	IEnumerator FakeDeathCoroutine () {
		for (int i = 0; i < 3; i++) {
			Debug.Log ("needs flash sound");
			flash.gameObject.SetActive (true);
			yield return new WaitForSeconds (0.15f);
			flash.gameObject.SetActive (false);
			yield return new WaitForSeconds (0.15f);
		}

		background1.speedChange (0);
		background2.speedChange (0);

		dragunController.VibrateDragun ();
	}
	public void ResumeBattle () {
		playerController.playerEnable ();
		background1.Phase2 ();
		background2.Phase2 ();
	}

	public void RealDeath () {
		playerController.playerDisable ();

		StartCoroutine (RealDeathCoroutine ());
	}
	IEnumerator RealDeathCoroutine () {
		for (int i = 0; i < 3; i++) {
			Debug.Log ("needs flash sound");
			flash.gameObject.SetActive (true);
			yield return new WaitForSeconds (0.15f);
			flash.gameObject.SetActive (false);
			yield return new WaitForSeconds (0.15f);
		}

		background1.speedChange (0);
		background2.speedChange (0);

		dragunController.FallDown ();
	}
	public void Victory () {
		//Victory picture
		Debug.Log ("Congrats");
	}

}
