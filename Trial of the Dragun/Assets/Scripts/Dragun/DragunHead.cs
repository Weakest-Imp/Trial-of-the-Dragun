using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragunHead : MonoBehaviour {

	[SerializeField] private Dragun dragun;
	[SerializeField] private GameObject bigBullet;
	private DragunBody bodyPart;

	[SerializeField] private float aimTime = 5;
	[SerializeField] private float buildUpTime = 2;
	[SerializeField] private float cooldownTime = 2;
	private Vector2 headDirection;
	[SerializeField] private Vector3 positionDeviation;

	private Animator anim;
	private GameObject player;

	void Start () {
		bodyPart = this.GetComponent<DragunBody> ();
		anim = this.GetComponent<Animator> ();
		player = DragunSceneManager.Instance.GetPlayer ();

		headDirection = Vector2.zero;
	}
	
	public void BigShot () {
		StartCoroutine (BigShotCoroutine ());
		bodyPart.Stop ();
	}

	IEnumerator BigShotCoroutine () {
		float time = 0;
		RotateHead ();
		while (time < aimTime) {
			yield return null;
			time += Time.deltaTime;
			RotateHead ();
		}
		Vibrate ();
		yield return new WaitForSeconds (buildUpTime);

		ResetVibrate ();
		GameObject bigBul = Instantiate (bigBullet, this.transform.position + positionDeviation, this.transform.rotation);
		bigBul.GetComponent<DragunBigBullet> ().DefineDirections (headDirection);

		yield return new WaitForSeconds (cooldownTime);
		ResetRotate ();
		bodyPart.Restart ();
	}

	void RotateHead () {
		float directionX = player.transform.position.x - this.transform.position.x;
		float directionY = player.transform.position.y - this.transform.position.y;
		headDirection = new Vector2 (directionX, directionY);

		float angle = Mathf.Atan2 (-1 * directionY, directionX);
		this.transform.rotation = Quaternion.Euler (0, 0,-180 * angle / Mathf.PI -180);
	}
	void ResetRotate () {
		this.transform.rotation = Quaternion.Euler (0, 0, 0);
	}

	void Vibrate () {
		bodyPart.Vibrate ();
	}
	void ResetVibrate () {
		bodyPart.VibrateStop ();
	}

	public void BigGunOut () {
		anim.SetTrigger ("Phase 2");
	}

	public void Stop () {
		StopAllCoroutines ();
	}

}
