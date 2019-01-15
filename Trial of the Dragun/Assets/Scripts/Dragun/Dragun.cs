using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragun : MonoBehaviour {

	[SerializeField] private DragunHead head;
	[SerializeField] private List<DragunBody> body;
	[SerializeField] private List<DragunClaw> guns;

	[SerializeField] private int maxHealth1;
	[SerializeField] private int maxHealth2;
	private int health;
	[SerializeField] private DragunHealthBar dragunBar;

	[SerializeField] private AudioClip hurtSound1;
	[SerializeField] private AudioClip hurtSound2;
	[SerializeField] private AudioClip ROAR;
	[SerializeField] private AudioClip littleBoomSound;
	[SerializeField] private AudioClip finalBoomSound;

	[SerializeField] private float initialX = 0;
	[SerializeField] private float moveSpeed = 3;
	[SerializeField] private float fallSpeed = 1;

	private float flag = 0;
	private bool inAttack = false;
	int previousAttack = -1;

	void Start () {
		health = maxHealth1;

		dragunBar.InitiateBar (maxHealth1);
		StartCoroutine (Intro ());
	}


	//Interactions__________________________________________________________________________________________________________________________________________________________________________________
	public void TakeDamage (int damage) {
		health -= damage;
		dragunBar.UpdateBar (health);
		if (health > 0) {
			SoundManager.Instance.RandomizeSFX (hurtSound1, hurtSound2);
		}
		if (health < 1) {
			if (flag < 2.5f) {
				FakeDeath ();
			} else if (flag > 2.5f) {
				RealDeath ();
			}
		}
	}


	//Body Management________________________________________________________________________________________________________________________________________________________________________________
	//Cutscenes_________________________________________________________________________________
	public void StopDragun () {
		this.StopDragunAttacks ();
		foreach (DragunBody part in body) {
			part.Stop ();
		}
		head.Stop ();
	}
	public void StopDragunAttacks () {
		this.StopAllCoroutines ();
		foreach (DragunClaw part in guns) {
			part.Stop ();
		}
	}

	public void RestartDragun () {
		foreach (DragunBody part in body) {
			part.Restart ();
		}
		StartCoroutine (DragunBattle ());
	}

	public void StopDragunHead () {
		body [0].Stop ();
	}
	public void RestartDragunHead () {
		body [0].Restart ();
	}

	IEnumerator Intro () {
		StopDragunHead ();
		DragunSceneManager.Instance.Intro ();
		yield return new WaitForSeconds (1);

		float X = this.transform.position.x;
		float Y = this.transform.position.y;
		float Z = this.transform.position.z;

		while (X > initialX) {
			X -= moveSpeed * Time.deltaTime;
			this.transform.position = new Vector3 (X, Y, Z);
			yield return null;
		}
		this.transform.position = new Vector3 (initialX, Y, Z);

		yield return new WaitForSeconds (1);

		DragunSceneManager.Instance.VS ();
		yield return new WaitForSeconds (1.5f);

		DragunSceneManager.Instance.IntroEnd ();
		RestartDragunHead ();
		StartCoroutine (DragunBattle ());
	}

	//Deaths cutscenes__________________________________________________________
	void FakeDeath () {
		flag = 2.5f;
		StopDragun ();
		DragunSceneManager.Instance.FakeDeath ();
	}
	public void VibrateDragun () {
		StartCoroutine (VibrateDragunCoroutine ());
//		SoundManager.Instance.PlaySFXLoop (finalBoomSound);
	}
	IEnumerator VibrateDragunCoroutine () {
		for (int i = 0; i < body.Count; i++) {
			yield return new WaitForSeconds (0.5f);
			body [i].Vibrate ();
		}
		yield return new WaitForSeconds (2);

		for (int i = 0; i < body.Count; i++) {
			body [i].VibrateStop ();
		}
		yield return new WaitForSeconds (0.5f);

		head.BigGunOut ();
		yield return new WaitForSeconds (1);
		SoundManager.Instance.PlaySFX (ROAR);
		yield return new WaitForSeconds (2);

		DragunSceneManager.Instance.ResumeBattle ();
		RestartDragun ();
		Phase2 ();
	}

	public void Phase2 (){
		foreach (DragunBody part in body) {
			part.Phase2 ();
		}
		dragunBar.Phase2 (maxHealth2);
		health = maxHealth2;

		flag = 3;
		inAttack = false;
	}

	void RealDeath (){
		if (flag < 4) {
			flag = 4;
			StopDragun ();
			DragunSceneManager.Instance.RealDeath ();
		}
	}
	public void FallDown () {
		StartCoroutine (FallDownCoroutine ());
	}
	IEnumerator FallDownCoroutine () {
		SoundManager.Instance.PlaySFXLoopCrescendo (littleBoomSound);
		body [1].Explode ();
		body [3].Explode ();
		body [5].Explode ();
		yield return new WaitForSeconds (1);

		body [2].Explode ();
		body [4].Explode ();
		body [6].Explode ();
		yield return new WaitForSeconds (1);

		body [0].Explode ();
		foreach (DragunBody part in body) {
			part.Vibrate ();
		}
		yield return new WaitForSeconds (1);

		float X = this.transform.position.x;
		float Y = this.transform.position.y;
		float Z = this.transform.position.z;

		float fall;
		while (Y > -11) {
			fall = fallSpeed * Time.deltaTime;
			Y -= fall;

			foreach (DragunBody part in body) {
				part.VibrateFall (fall);
			}

			this.transform.position = new Vector3 (X, Y, Z);
			yield return null;
		}
		foreach (DragunBody part in body) {
			part.VibrateStop ();
		}

		SoundManager.Instance.PlaySFX (finalBoomSound);
		yield return new WaitForSeconds (2);

		DragunSceneManager.Instance.Victory ();

	}

	//Attacks_____________________________________________________________________________________
	IEnumerator DragunBattle () {
		yield return null;
		if (flag <= 0) {
			//Introduces Straight shots
			StartCoroutine (FirstAttack ());
			inAttack = true;
			flag = 0.5f;
			yield return null;
		}
		while (flag <= 0.5f) {
			//Introduces Up shots
			if (!inAttack) {
				StartCoroutine (SecondAttack ());
				inAttack = true;
				flag = 1;
			}
			yield return null;
		}

		//Starts acting randomly
		int halfLife = 1 + maxHealth1 / 2;

		while (flag <= 1) {
			if (health < halfLife) {
				flag = 1.5f;
			}
			if (!inAttack) {
				FirstHalf ();
				inAttack = true;
			}
			yield return null;
		}

		while (flag <= 1.5f) {
			//Introduces Down shots
			if (!inAttack) {
				StartCoroutine (SecondHalfAttack0 ());
				inAttack = true;
				previousAttack = 0;
				flag = 2;
			}
			yield return null;
		}

		while (flag <= 2) {
			//Second random phase until "death"
			if (!inAttack) {
				SecondHalf ();
				inAttack = true;
			}
			yield return null;
		}


		//Only gets here after first death
		inAttack = false;
		previousAttack = -1;
		int attack = 2;
		while (flag <= 3) {
			//Final phase
			if (!inAttack) {
				if (attack < 3) {
					FinalPart ();
					attack++;
					inAttack = true;
				} else {
					BigShot ();
					attack = 1;
					inAttack = true;
				}
			}
			yield return null;
		}

	}

	//One time attacks___________________________________________________________________________
	IEnumerator FirstAttack () {
		guns [1].StraightShot ();
		yield return new WaitForSeconds (2f);

		for (int i = 0; i < guns.Count; i++) {
			guns [i].StraightShot ();
			yield return new WaitForSeconds (1);
		}
		yield return new WaitForSeconds (2);
		inAttack = false;
	}

	IEnumerator SecondAttack () {
		guns [0].UpShot ();
		yield return new WaitForSeconds (2f);

		guns [2].UpShot ();
		yield return new WaitForSeconds (1);
		guns [1].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [0].UpShot ();
		yield return new WaitForSeconds (3);
		inAttack = false;
	}

	//Random patterns for first half_____________________________________________________________
	void FirstHalf () {
		int pattern = Random.Range (0, 4);
		while (pattern == previousAttack) {
			pattern = Random.Range (0, 4);
		}
		StartCoroutine ("FirstHalfAttack" + pattern);
		previousAttack = pattern;
	}

	IEnumerator FirstHalfAttack0 () {
		foreach (DragunClaw gun in guns) {
			gun.UpShot ();
		}
		yield return new WaitForSeconds(4);
		inAttack = false;
	}

	IEnumerator FirstHalfAttack1 () {
		guns [0].UpShot ();
		yield return new WaitForSeconds (1);
		guns [1].UpShot ();
		yield return new WaitForSeconds (1);
		guns [2].StraightShot ();
		yield return new WaitForSeconds (3);
		inAttack = false;
	}

	IEnumerator FirstHalfAttack2 () {
		guns [1].UpShot ();
		yield return new WaitForSeconds (1);
		guns [0].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [1].StraightShot ();
		yield return new WaitForSeconds (3);
		inAttack = false;
	}

	IEnumerator FirstHalfAttack3 () {
		guns [0].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [1].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [2].StraightShot ();
		yield return new WaitForSeconds (3);
		inAttack = false;
	}

	//Random patterns for second half____________________________________________________________
	void SecondHalf () {
		int pattern = Random.Range (0, 4);
		while (pattern == previousAttack) {
			pattern = Random.Range (0, 4);
		}
		StartCoroutine ("SecondHalfAttack" + pattern);
		previousAttack = pattern;
	}

	IEnumerator SecondHalfAttack0 () {
		foreach (DragunClaw gun in guns) {
			gun.UpShot ();
		}
		yield return new WaitForSeconds (1.5f);

		foreach (DragunClaw gun in guns) {
			gun.DownShot ();
		}
		yield return new WaitForSeconds(4);
		inAttack = false;
	}

	IEnumerator SecondHalfAttack1 () {
		guns [2].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [2].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [0].DownShot ();
		guns [1].UpShot ();
		yield return new WaitForSeconds (3);
		inAttack = false;
	}

	IEnumerator SecondHalfAttack2 () {
		guns [0].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [1].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [2].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [1].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [0].StraightShot ();
		yield return new WaitForSeconds (3);
		inAttack = false;
	}

	IEnumerator SecondHalfAttack3 () {
		guns [0].DownShot ();
		guns [2].UpShot ();
		yield return new WaitForSeconds (1);
		guns [2].DownShot ();
		guns [0].UpShot ();
		yield return new WaitForSeconds (1);
		guns [1].StraightShot ();
		yield return new WaitForSeconds(3);
		inAttack = false;
	}

	//After the Big Gun is out____________________________________________________________________
	void FinalPart () {
		int pattern = Random.Range (0, 3);
		while (pattern == previousAttack) {
			pattern = Random.Range (0, 3);
		}
		StartCoroutine ("FinalAttack" + pattern);
		previousAttack = pattern;
	}

	IEnumerator FinalAttack0 () {
		guns [0].StraightShot ();
		guns [2].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [1].StraightShot ();
		yield return new WaitForSeconds (1);

		guns [0].StraightShot ();
		guns [2].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [1].StraightShot ();
		yield return new WaitForSeconds (1);

		guns [0].StraightShot ();
		guns [1].StraightShot ();
		guns [2].StraightShot ();
		yield return new WaitForSeconds (3);
		inAttack = false;
	}

	IEnumerator FinalAttack1 () {
		guns [0].UpShot ();
		yield return new WaitForSeconds (0.5f);
		guns [1].UpShot ();
		yield return new WaitForSeconds (0.5f);
		guns [0].UpShot ();
		yield return new WaitForSeconds (1);

		guns [1].DownShot ();
		yield return new WaitForSeconds (0.5f);
		guns [0].DownShot ();
		yield return new WaitForSeconds (0.5f);
		guns [1].DownShot ();
		yield return new WaitForSeconds (1);

		guns [2].StraightShot ();
		yield return new WaitForSeconds (3);
		inAttack = false;
	}

	IEnumerator FinalAttack2 () {
		guns [0].StraightShot ();
		yield return new WaitForSeconds (0.5f);
		guns [1].StraightShot ();
		yield return new WaitForSeconds (0.5f);
		guns [2].StraightShot ();
		yield return new WaitForSeconds (1);

		guns [0].UpShot ();
		guns [1].DownShot ();
		yield return new WaitForSeconds (1);

		guns [0].DownShot ();
		guns [1].UpShot ();
		yield return new WaitForSeconds (1);

		guns [2].StraightShot ();
		yield return new WaitForSeconds (0.5f);
		guns [1].StraightShot ();
		yield return new WaitForSeconds (0.5f);
		guns [0].StraightShot ();
		yield return new WaitForSeconds (3);

		inAttack = false;
	}

	//Patterns with a big shot at the end__________________________________________________________
	void BigShot () {
		int pattern = Random.Range (0, 2);
		StartCoroutine ("BigShot" + pattern);
	}

	IEnumerator BigShot0 () {
		//Gets called before the rest to get time for charging
		head.BigShot ();

		guns [2].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [1].StraightShot ();
		yield return new WaitForSeconds (1);

		guns [0].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [1].StraightShot ();
		yield return new WaitForSeconds (1);

		guns [2].StraightShot ();
		yield return new WaitForSeconds (1);
		guns [1].StraightShot ();
		yield return new WaitForSeconds (1);

		guns [0].StraightShot ();
		yield return new WaitForSeconds (4);//1 before the shot + 3 for cooldown

		inAttack = false;
	}

	IEnumerator BigShot1 () {
		//Gets called before the rest to get time for charging
		head.BigShot ();

		guns [0].DownShot ();
		guns [1].UpShot ();
		yield return new WaitForSeconds (1);
		guns [1].DownShot ();
		guns [0].UpShot ();
		yield return new WaitForSeconds (1);
		guns [2].StraightShot ();
		yield return new WaitForSeconds (2);

		guns [0].DownShot ();
		guns [1].UpShot ();
		yield return new WaitForSeconds (1);
		guns [1].DownShot ();
		guns [0].UpShot ();
		yield return new WaitForSeconds (1);
		guns [2].StraightShot ();
		yield return new WaitForSeconds (4);//1 before the shot + 3 for cooldown

		inAttack = false;
	}
}
