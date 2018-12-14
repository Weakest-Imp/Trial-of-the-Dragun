using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float playerSpeed = 10;
	[SerializeField] private GameObject bullet;
	[SerializeField] private float shootCooldown;
	private float cooldown = 0;

	[SerializeField] private int maxHealth = 6;
	private int health;
	[SerializeField] private float invicibilityTime = 0.8f;
	private bool invincible = false;
	[SerializeField] private float flickerTime = 0.1f;
	private SpriteRenderer sr;

	private Rigidbody2D rb;

	private float horInput;
	private float verInput;
	private float fireInput;


	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
		sr = this.GetComponent<SpriteRenderer> ();
		health = maxHealth;
	}


	void Update () {
		checkInput ();

		Move ();
		Fire ();
	}


	//Genral Control for Scene Management________________________________________________________________________________________________________________________________________________________________
	void checkInput () {
		horInput = Input.GetAxisRaw ("Horizontal");
		verInput = Input.GetAxisRaw ("Vertical");
		fireInput = Input.GetAxisRaw ("Fire1");
	}

	void resetInput () {
		horInput = 0;
		verInput = 0;
		fireInput = 0;
	}

	public void playerDisable () {
		this.enabled = false;
		resetInput ();
		Move ();
	}

	public void playerEnable () {
		this.enabled = true;
	}

	//Movement_____________________________________________________________________________________________________________________________________________________________________________________________
	void Move () {
		Vector2 move = Vector2.zero;
		float hor = 0;
		float ver = 0;
		if (horInput < 0) {
			hor = horInput - 1;
		} if (horInput > 0) {
			hor = horInput + 1;
		}

		if (verInput < 0) {
			ver = verInput - 1;
		}if (verInput > 0) {
			ver = verInput + 1;
		}

		move = new Vector2(hor/2, ver/2);
		if (move.magnitude > 1) {
			move.Normalize();
		}
		
		rb.velocity = playerSpeed * move;
	}


	//Shooting_____________________________________________________________________________________________________________________________________________________________________________________
	void Fire () {
		if (cooldown <= 0 && fireInput > 0) {
			Instantiate (bullet, this.transform.position, this.transform.rotation);
			StartCoroutine (ShootCooldown ());
		}
	}

	IEnumerator ShootCooldown () {
		cooldown = shootCooldown;
		while (cooldown > 0) {
			yield return null;
			cooldown -= Time.deltaTime;
		}
	}


	//Interactions_____________________________________________________________________________
	void OnTriggerEnter2D (Collider2D other) {
		if (!invincible && other.gameObject.tag == "DraGunBullet") {
			TakeDamage (1);
		}
	}

	void TakeDamage (int damage) {
		Debug.Log (health);
		StartCoroutine (Invincibility ());
		health -= damage;
		if (health < 1) {
			DragunSceneManager.Instance.GameOver ();
		}
	}

	IEnumerator Invincibility () {
		invincible = true;
		float time = 0;
		while (time < invicibilityTime) {
			sr.enabled = !sr.enabled;
			yield return new WaitForSeconds (flickerTime);
			time += Time.deltaTime;
		}
		sr.enabled = true;
		invincible = false;
	}

}
