using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragunClaw : MonoBehaviour {

	[SerializeField] private Dragun dragun;
	[SerializeField] private GameObject SmallGun;
	[SerializeField] private GameObject bullet;
	[SerializeField] private int damage = 2;


	[SerializeField] private float buildUpTimeStraight = 1.2f;
	[SerializeField] private Vector3 positionDeviationStraight;

	[SerializeField] private float buildUpTimeCurved = 0.7f;
	[SerializeField] private Vector3 positionDeviationUp;
	[SerializeField] private Vector3 positionDeviationDown;

	[SerializeField] private float straightSpeed = 10;
	[SerializeField] private float curvedSpeed = 5;


	private Animator anim;
	[SerializeField] private float blinkTime = 0.1f;
	private SpriteRenderer sr;
	[SerializeField] private DragunBody parent;
	[SerializeField] private SpriteRenderer gun;
	private Shader shaderGUItext;
	private Shader shaderSpritesDefault;

	void Start () {
		anim = this.GetComponent<Animator> ();
		sr = this.GetComponent<SpriteRenderer> ();
		shaderGUItext = Shader.Find("GUI/Text Shader");
		shaderSpritesDefault = Shader.Find("Sprites/Default");
	}



	//Interactions________________________________________________________________________________
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "PlayerBullet") {
			dragun.TakeDamage (damage);
			StartCoroutine (Blink ());
		}
	}

	IEnumerator Blink () {
		parent.ClawBlink ();
		//Turns the sprite white
		sr.material.shader = shaderGUItext;
		sr.color = Color.white;
		gun.material.shader = shaderGUItext;
		gun.color = Color.white;

		yield return new WaitForSeconds (blinkTime);

		//returns the sprite to normal
		sr.material.shader = shaderSpritesDefault;
		sr.color = Color.white;
		gun.material.shader = shaderSpritesDefault;
		gun.color = Color.white;
	}


	//Shots___________________________________________________________________________________
	public void Stop() {
		StopAllCoroutines ();
	}


	public void StraightShot () {
		StartCoroutine (StraightShotCoroutine ());
	}
	IEnumerator StraightShotCoroutine () {
		anim.SetTrigger ("Straight");
		yield return new WaitForSeconds (buildUpTimeStraight);

		GameObject bul = Instantiate (bullet, this.transform.position + positionDeviationStraight, this.transform.rotation);
		DragunBullet bulControl = bul.GetComponent<DragunBullet> ();
		bulControl.SetSpeed (straightSpeed);
		bulControl.DefineIncline (0);
	}

	public void UpShot () {
		StartCoroutine (UpShotCoroutine ());
	}
	IEnumerator UpShotCoroutine () {
		anim.SetTrigger ("Up");
		yield return new WaitForSeconds (buildUpTimeCurved);

		GameObject bul = Instantiate (bullet, this.transform.position + positionDeviationUp, this.transform.rotation);
		DragunBullet bulControl = bul.GetComponent<DragunBullet> ();
		bulControl.SetSpeed (curvedSpeed);
		bulControl.DefineIncline (1);
	}

	public void DownShot () {
		StartCoroutine (DownShotCoroutine ());
	}
	IEnumerator DownShotCoroutine () {
		anim.SetTrigger ("Down");
		yield return new WaitForSeconds (buildUpTimeCurved);

		GameObject bul = Instantiate (bullet, this.transform.position + positionDeviationDown, this.transform.rotation);
		DragunBullet bulControl = bul.GetComponent<DragunBullet> ();
		bulControl.SetSpeed (curvedSpeed);
		bulControl.DefineIncline (-1);
	}

}
