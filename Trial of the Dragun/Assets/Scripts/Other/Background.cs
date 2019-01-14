using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

	[SerializeField] float slideSpeed1 = -5;
	[SerializeField] float slideSpeed2 = 10;
	float slideSpeed;

	[SerializeField] float speedChangeTime = 1;
	float scale;

	[SerializeField] private Color dark;
	[SerializeField] private float colorChangeTime = 3;

	private Rigidbody2D rb;
	private SpriteRenderer sr;

	void Start () 
	{
		scale = this.transform.parent.localScale.x;
		rb = this.GetComponent<Rigidbody2D> ();
		sr = this.GetComponent<SpriteRenderer> ();
		rb.velocity = new Vector2 (slideSpeed1, 0);
	}

	void Update () {
		Overflow ();
	}


	// Teleports the background back once it goes out of screen
	void Overflow ()
	{
		float x = this.transform.position.x;
		float y = this.transform.position.y;
		float z = this.transform.position.z;
		if (x > 5.25F * scale) {
			this.transform.position = new Vector3 (x - 10.22F * scale, y, z);
		} else {
			if (x < -5.25F * scale) {
				this.transform.position = new Vector3 (x + 10.22F * scale, y, z);
			} else {
				this.transform.position = new Vector3 (x, y, z);
			}
		}
	}

	public void speedChange (float newSpeed) {
		StartCoroutine (speedChangeCoroutine(newSpeed));
	}
	public void Phase2 () {
		StartCoroutine (speedChangeCoroutine(slideSpeed2));
		StartCoroutine (Darken ());
	}

	IEnumerator speedChangeCoroutine (float newSpeed)
	//Changes the speed smoothly
	{
		Vector2 change = new Vector2(newSpeed - rb.velocity.x, 0);
		float time = 0;
		float delta = 0;
		change = change / speedChangeTime;
		while (time < speedChangeTime) {
			yield return null;
			delta = Time.deltaTime;
			rb.velocity += delta * change;
			time += delta;
		}
		rb.velocity = new Vector2(newSpeed, 0);
	}

	IEnumerator Darken () {
		Color original = sr.color;

		float time = 0;
		float timeFactor = 1 / colorChangeTime;
		yield return null;
		float advancement;

		while (time < colorChangeTime) {
			time += Time.deltaTime;
			advancement = time * timeFactor;

			sr.color = Color.Lerp(original, dark, advancement);
			yield return null;
		}

		sr.color = dark;
	}

}
