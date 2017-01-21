using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public static Score Instance;

	public Text scoreText;

	private int score = 0;

	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else {
			Debug.LogWarning ("There should be only one Score component!");
		}
	}

	public void AddPoint () {
		score++;

		scoreText.text = score.ToString ();
	}
}
