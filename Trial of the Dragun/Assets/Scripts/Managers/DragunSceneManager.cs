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
	[SerializeField] private GameObject victoryCanvas;
	[SerializeField] private Background background1;
	[SerializeField] private Background background2;
	[SerializeField] private Image flash;

	[SerializeField] private AudioClip battleBGM1;
	[SerializeField] private AudioClip battleBGM2;

	[SerializeField] private AudioClip flashSound;
	[SerializeField] private AudioClip gameOverJingle;

	[System.Serializable]
	public class Introduction {
		public GameObject introBox1;
		public GameObject introBox2;
		public float height = 5;
		public float heightOut = 7;
		public float speed = 3;
		public GameObject versus;
	}
	[SerializeField] private Introduction intro;


	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy (gameObject);
		}

		playerController = player.GetComponent<PlayerController> ();
		dragunController = dragun.GetComponent<Dragun> ();
		gameOverCanvas.SetActive (false);
		victoryCanvas.SetActive (false);
		flash.gameObject.SetActive (false);
	}

	void Start () {
		SoundManager.Instance.StopBGM ();
	}

	//Player_____________________________________________________________________________________
	public GameObject GetPlayer () {
		return this.player;
	}

	public void GameOver () {
		playerController.playerDisable ();
		dragunController.StopDragunAttacks ();
		gameOverCanvas.SetActive (true);
		SoundManager.Instance.PlaySFX (gameOverJingle);
	}


	//Dragun______________________________________________________________________________________
	public void Intro () {
		playerController.playerDisable ();
		playerController.Intro ();
		StartCoroutine (BoxIn ());
	}
	IEnumerator BoxIn () {
		intro.introBox1.SetActive (true);
		intro.introBox2.SetActive (true);

		float height = intro.introBox1.transform.position.y;

		float X1 = intro.introBox1.transform.position.x;
		float Z1 = intro.introBox1.transform.position.z;
		float X2 = intro.introBox2.transform.position.x;
		float Z2 = intro.introBox2.transform.position.z;

		while (height > intro.height) {
			yield return null;
			height -= intro.speed * Time.deltaTime;
			intro.introBox1.transform.position = new Vector3 (X1, height, Z1);
			intro.introBox2.transform.position = new Vector3 (X2, -1 * height, Z2);
		}
		intro.introBox1.transform.position = new Vector3 (X1, intro.height, Z1);
		intro.introBox2.transform.position = new Vector3 (X2, -1 * intro.height, Z2);
	}
	public void VS () {
		intro.versus.SetActive (true);
	}
	public void IntroEnd () {
		playerController.playerEnable ();
		StartCoroutine (BoxOut ());
		intro.versus.SetActive (false);
		SoundManager.Instance.PlayBGM (battleBGM1);
	}
	IEnumerator BoxOut (){
		float height = intro.introBox1.transform.position.y;

		float X1 = intro.introBox1.transform.position.x;
		float Z1 = intro.introBox1.transform.position.z;
		float X2 = intro.introBox2.transform.position.x;
		float Z2 = intro.introBox2.transform.position.z;

		while (height < intro.heightOut) {
			yield return null;
			height += intro.speed * 2 * Time.deltaTime;
			intro.introBox1.transform.position = new Vector3 (X1, height, Z1);
			intro.introBox2.transform.position = new Vector3 (X2, -1 * height, Z2);
		}

		intro.introBox1.SetActive (false);
		intro.introBox2.SetActive (false);
	}


	public void FakeDeath () {
		playerController.playerDisable ();
		SoundManager.Instance.StopBGM ();

		StartCoroutine (FakeDeathCoroutine ());
	}
	IEnumerator FakeDeathCoroutine () {
		for (int i = 0; i < 3; i++) {
			SoundManager.Instance.PlaySFX (flashSound);
			flash.gameObject.SetActive (true);
			yield return new WaitForSeconds (0.15f);
			flash.gameObject.SetActive (false);
			yield return new WaitForSeconds (0.15f);
		}

		background1.speedChange (0);
		background2.speedChange (0);

		yield return new WaitForSeconds (0.5f);
		dragunController.VibrateDragun ();
	}
	public void ResumeBattle () {
		playerController.playerEnable ();
		background1.Phase2 ();
		background2.Phase2 ();
		SoundManager.Instance.PlayBGM (battleBGM2);
	}

	public void RealDeath () {
		playerController.playerDisable ();
		SoundManager.Instance.StopBGM ();

		StartCoroutine (RealDeathCoroutine ());
	}
	IEnumerator RealDeathCoroutine () {
		for (int i = 0; i < 3; i++) {
			SoundManager.Instance.PlaySFX (flashSound);
			flash.gameObject.SetActive (true);
			yield return new WaitForSeconds (0.15f);
			flash.gameObject.SetActive (false);
			yield return new WaitForSeconds (0.15f);
		}

		background1.speedChange (0);
		background2.speedChange (0);

		yield return new WaitForSeconds (0.5f);
		dragunController.FallDown ();
	}
	public void Victory () {
		player.SetActive (false);
		victoryCanvas.SetActive (true);
		Debug.Log ("Victory jingle");
		StartCoroutine (BackToMainMenu ());
	}
	IEnumerator BackToMainMenu () {
		//To not let it disappear instantely
		yield return new WaitForSeconds (1);

		float fireInput = Input.GetAxisRaw ("Fire1");
		while (fireInput != 1) {
			yield return null;
			fireInput = Input.GetAxisRaw ("Fire1");
		}
		GameManager.Instance.MainMenu ();

	}

}
