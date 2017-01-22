using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Score : MonoBehaviour {

	public static Score Instance;

	[Serializable]
	public class ScoreEvent : UnityEvent<Score> { }

	public ScoreEvent OnScore = new ScoreEvent ();

	public Text scoreText;

	public Text highestScoreText;

	public Text finalScoreText;

	public GameObject sun;

	public GameObject moon;

	private int score = 0;

	private int abducted = 0;

	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else {
			Debug.LogWarning ("There should be only one Score component!");
		}

		UpdateHighestScore ();
	}

	IEnumerator Start () {
		yield return new WaitUntil (() => DayCycle.Instance != null);
		Debug.Log ("Score getting a late start");
	}

	public int Abducted () {
		return abducted;
	}

	public void AddPoints () {
		Debug.Log ("AddPoints()");
		abducted++;

		// Only one point during the day because it's heatscore
		score += DayCycle.Instance.IsDay () ? 1 : 2;

		UpdateHighestScore ();

		scoreText.text = score.ToString ();

		if (OnScore != null) {
			OnScore.Invoke (this);
		}

		if (abducted >= AbducteePool.Instance.abductees.Length) {
			StartCoroutine (ResetGame ());
		}
	}

	public void Penalize () {
		Debug.Log ("Penalize()");

		// Lose a point for getting the wrong person
		score -= (score > 0) ? -1 : 0;

		UpdateHighestScore ();

		scoreText.text = score.ToString ();
	}

	IEnumerator ResetGame () {
		yield return new WaitForSeconds (0.5f);

		finalScoreText.text = "Final Score: " + score;
		finalScoreText.gameObject.SetActive (true);

		yield return new WaitForSeconds (1.5f);

		finalScoreText.gameObject.SetActive (false);

		AbducteePool.Instance.Reset ();
		Intro.Instance.RunIntro ();
		Inputs.Instance.ResetUFO ();
		DayCycle.Instance.ResetCycle ();
		sun.SetActive (true);
		moon.SetActive (false);
		abducted = 0;
		score = 0;
		scoreText.text = score.ToString ();
	}

	void UpdateHighestScore () {
		int highest = PlayerPrefs.GetInt ("highest_score", 0);

		if (score > highest) {
			PlayerPrefs.SetInt ("highest_score", score);

			highestScoreText.text = score.ToString ();
		} else {
			highestScoreText.text = highest.ToString ();
		}
	}

	public void HandleDayCycleChange (DayCycle dc) {
		if (dc.IsDay ()) {
			sun.SetActive (true);
			moon.SetActive (false);
		} else {
			sun.SetActive (false);
			moon.SetActive (true);
		}
	}
}
