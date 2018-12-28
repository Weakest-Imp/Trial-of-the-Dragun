using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragunHealthBar : MonoBehaviour {

	private Slider dragunBar;
	[SerializeField] Image fill;
	[SerializeField] private Color phase1Color = Color.green;
	[SerializeField] private Color phase2Color = Color.red;

	[SerializeField] private float refillTime = 3;


	void Start () {
		dragunBar = this.GetComponent<Slider> ();
		dragunBar.gameObject.SetActive (false);
	}

	//Dragun Health Bar Management______________________________________________________________
	public void InitiateBar (int maxHealth) {
		dragunBar.gameObject.SetActive (true);
		dragunBar.maxValue = maxHealth;
		dragunBar.value = dragunBar.maxValue;
		fill.color = phase1Color;
	}
	public void Phase2 (int newMaxHealth) {
		fill.gameObject.SetActive (true);
		dragunBar.maxValue = newMaxHealth;
		
		dragunBar.value = 0;
		StartCoroutine (RefillBar ());
		fill.color = phase2Color;
	}

	public void UpdateBar (int health) {
		if (health < 1) {
			fill.gameObject.SetActive (false);
		}
		dragunBar.value = health;
	}

	//Refills the bar continiously
	IEnumerator RefillBar () {
		float maxValue = dragunBar.maxValue;
		int value = 0;
		float refillFactor = 1 / refillTime;
		while (value < maxValue) {
			yield return null;
			value += Mathf.CeilToInt (refillFactor * maxValue * Time.deltaTime);
			dragunBar.value = value;
		}
		dragunBar.value = maxValue;

	}
}
