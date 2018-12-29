using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragunBigBullet : MonoBehaviour {

	[SerializeField] private DragunBullet bigBullet;
	[SerializeField] private List<DragunBullet> bullets;

	
	public void DefineDirections (Vector2 direction) {
		direction.Normalize ();

		bigBullet.BigStraight (direction);
		bullets [0].BigStraight (GeneralRotation(direction, 45));
		bullets [1].BigStraight (GeneralRotation(direction, -45));
		bullets [2].BigStraight (GeneralRotation(direction, 90));
		bullets [3].BigStraight (GeneralRotation(direction, -90));

	}

	//Vector2 rotation functions_________________________________________________________________
	Vector2 GeneralRotation (Vector2 direction, float angle) {
		//rotates the direction by the given angle
		float radiantAngle = angle * 2 * Mathf.PI / 360;

		float cos = Mathf.Cos (radiantAngle);
		float sin = Mathf.Sin (radiantAngle);

		float newX = cos * direction.x - sin * direction.y;
		float newY = sin * direction.x + cos * direction.y;
		return new Vector2 (newX, newY);
	}


}
