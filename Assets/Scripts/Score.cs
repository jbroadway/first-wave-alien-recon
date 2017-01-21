using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public static Score Instance;

	public Text scoreText;

	private int score = 0;

	private int abducted = 0;

	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else {
			Debug.LogWarning ("There should be only one Score component!");
		}
	}

	public void AddPoints () {
		abducted++;

		// Only one point during the day because it's heatscore
		score += DayCycle.Instance.IsDay () ? 1 : 2;

		scoreText.text = score.ToString ();

		if (abducted >= AbducteePool.Instance.abductees.Length) {
			AbducteePool.Instance.Reset ();
			Intro.Instance.RunIntro ();
			abducted = 0;
		}
	}
}
