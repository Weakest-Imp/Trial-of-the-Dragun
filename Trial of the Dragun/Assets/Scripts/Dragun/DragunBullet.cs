using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragunBullet : MonoBehaviour {

//	[SerializeField] private float gravity = 1;
	[SerializeField] private float straightSpeed = 10;
	[SerializeField] private float curvedSpeed = 5;
	[SerializeField] private float maxHeight = 4.28f;
	private int incline = 0;
	//incline = -1, 0 or 1;
	//-1 = from down, 0 = straight, 1 = from above

	private GameObject player;
	private Rigidbody2D rb;


	void Awake () {
		rb = this.GetComponent<Rigidbody2D> ();
		player = DragunSceneManager.Instance.GetPlayer ();
	}

	void Update () {
		Rotate ();
	}

	//Initialisation__________________________________________________________________________
	public void DefineIncline (int newIncline) {
		incline = newIncline;
		switch (incline) {
		case -1: 
			Down ();
			break;
		case 0:
			Straight ();
			break;
		case 1: 
			Up ();
			break;
		default:
			Debug.Log ("mauvaise inclinaison");
			Destroy (this.gameObject);
			break;
		}

	}
	public void SetGravity (float gravity) {
		rb.gravityScale = gravity;
	}

	//Trajectories____________________________________________________________________________
	void Straight () {
		float directionX = player.transform.position.x - this.transform.position.x;
		float directionY = player.transform.position.y - this.transform.position.y;
		Vector2 direction = new Vector2 (directionX, directionY);
		direction.Normalize ();
		rb.velocity = direction * straightSpeed;
		SetGravity (0);
	}

	void Up () {
		float g = -1 * Physics2D.gravity.y;
		float deltaY0 = maxHeight - this.transform.position.y;
		if (deltaY0 < 0) {
			//In case the value is negative, to avoid problems
			Straight ();
			return;
		}
		float deltaY1 = maxHeight - player.transform.position.y;
		float T = -1 * (player.transform.position.x - this.transform.position.x) / curvedSpeed;

		float speedY = 2*(deltaY0 + Mathf.Sqrt(deltaY0*deltaY1)) / T;
		float gravity = speedY*speedY/ (2 * deltaY0 * g);

		rb.velocity = new Vector2 ( -1 * curvedSpeed, speedY);
		SetGravity (gravity);
	}

	void Down () {
		float g = Physics2D.gravity.y;
		float deltaY0 = this.transform.position.y + maxHeight;
		if (deltaY0 < 0) {
			//In case the value is negative, to avoid problems
			Straight ();
			return;
		}
		float deltaY1 = player.transform.position.y + maxHeight;
		float T = -1 * (player.transform.position.x - this.transform.position.x) / curvedSpeed;

		float speedY = -2*(deltaY0 + Mathf.Sqrt(deltaY0*deltaY1)) / T;
		float gravity = speedY*speedY/ (2 * deltaY0 * g);

		rb.velocity = new Vector2 ( -1 * curvedSpeed, speedY);
		SetGravity (gravity);
	}

	void Rotate () {
		float angle = Mathf.Atan2 (-1 * rb.velocity.y, rb.velocity.x);
		this.transform.rotation = Quaternion.Euler (0, 0,-180 * angle / Mathf.PI -180);
	}



	//Interactions____________________________________________________________________________
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Destroy (this.gameObject);
		}
		if (other.gameObject.tag == "Wall") {
			Destroy (this.gameObject);
		}
	}

}
