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
	[SerializeField] private List<HeartUI> healthBar;

	[SerializeField] private float invincibilityTime = 0.8f;
	private bool invincible = false;
	[SerializeField] private float flickerTime = 0.1f;
	private SpriteRenderer sr;

	private Rigidbody2D rb;
	private Animator anim;

	private float horInput;
	private float verInput;
	private float fireInput;



	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
		sr = this.GetComponent<SpriteRenderer> ();
		anim = this.GetComponent<Animator> ();
		health = maxHealth;
		HealthBar ();
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
		invincible = true;
	}

	public void playerEnable () {
		this.enabled = true;
		invincible = false;
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
		if (!invincible && other.gameObject.tag == "DraGunBigBullet") {
			TakeDamage (2);
		}
	}

	void TakeDamage (int damage) {
		health -= damage;
		HealthBar ();
		if (health < 1) {
			DragunSceneManager.Instance.GameOver ();
			anim.SetTrigger ("Dead");
			return;
		} 
		StartCoroutine (Invincibility ());
	}

	IEnumerator Invincibility () {
		invincible = true;
		anim.SetTrigger ("Hurt");
		yield return new WaitForSeconds (invincibilityTime);
		invincible = false;
	}

	void HealthBar () {
		foreach (HeartUI heart in healthBar) {
			heart.HeartChange (health);
		}
	}

}
