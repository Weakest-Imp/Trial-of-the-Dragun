using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour {

	[SerializeField] private Sprite fullHeart;
	[SerializeField] private Sprite halfHeart;
	[SerializeField] private Sprite emptyHeart;
	private Image im;

	[SerializeField] private int middleHealth;

	void Start () {
		im = this.GetComponent<Image> ();
	}

	public void HeartChange (int health) {
		if (health > middleHealth) {
			im.sprite = fullHeart;
		} else if (health < middleHealth) {
			im.sprite = emptyHeart;
		} else {
			im.sprite = halfHeart;
		}
	}

}
