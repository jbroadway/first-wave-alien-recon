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

	public int dayPoints = 2;

	public int nightPoints = 5;

	public int penaltyPoints = -1;

	public float timeLimit = 90f;

	public Text scoreText;

	public Text highestScoreText;

	public Text finalScoreText;

	public Text timerText;

	public Text timeBonusText;

	public GameObject sun;

	public GameObject moon;

	public AudioClip pointsClip;

	public AudioClip penaltyClip;

	private int score = 0;

	private int abducted = 0;

	private AudioSource audioSource;

	private float timeLeft;

	private bool timeRunning = false;

	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else {
			Debug.LogWarning ("There should be only one Score component!");
		}

		timeLeft = Mathf.FloorToInt (timeLimit);

		UpdateHighestScore ();
	}

	IEnumerator Start () {
		audioSource = GetComponent<AudioSource> ();

		yield return new WaitUntil (() => DayCycle.Instance != null);
		Debug.Log ("Score getting a late start");
	}

	void Update () {
		if (timeRunning) {
			timeLeft -= Time.deltaTime;
			timerText.text = Mathf.FloorToInt (timeLeft).ToString ().PadLeft (2, '0');

			if (timeLeft < 0f) {
				audioSource.PlayOneShot (pointsClip);
				StartCoroutine (ResetGame ());
			}
		} else {
			timeLeft = timeLimit;
			timerText.text = Mathf.FloorToInt (timeLimit).ToString ();
		}
	}

	public void StartTimer () {
		timeRunning = true;
	}

	public int Abducted () {
		return abducted;
	}

	public void AddPoints () {
		Debug.Log ("AddPoints()");
		abducted++;

		// Only one point during the day because it's heatscore
		score += DayCycle.Instance.IsDay () ? dayPoints : nightPoints;

		UpdateHighestScore ();

		scoreText.text = score.ToString ();

		audioSource.PlayOneShot (pointsClip);

		if (OnScore != null) {
			OnScore.Invoke (this);
		}

		if (abducted >= AbducteePool.Instance.abductees.Length) {
			StartCoroutine (ResetGame ());
		} else {
			// Give time bonus
			timeLeft += 5f;
		}
	}

	public void Penalize () {
		Debug.Log ("Penalize()");

		// Lose a point for getting the wrong person
		score += (score > 0) ? penaltyPoints : 0;

		UpdateHighestScore ();

		scoreText.text = score.ToString ();

		audioSource.PlayOneShot (penaltyClip);
	}

	IEnumerator ResetGame () {
		timeRunning = false;
		int timeBonus = Mathf.FloorToInt (timeLeft);

		yield return new WaitForSeconds (0.5f);

		if (timeBonus > 0) {
			timeBonusText.text = "Time Bonus: " + timeBonus;
			score += timeBonus;
		} else {
			timeBonusText.text = "Time Bonus: 0";
		}
		timeBonusText.gameObject.SetActive (true);

		finalScoreText.text = "Final Score: " + score;
		finalScoreText.gameObject.SetActive (true);

		UpdateHighestScore ();

		yield return new WaitForSeconds (2.5f);

		finalScoreText.gameObject.SetActive (false);
		timeBonusText.gameObject.SetActive (false);

		Intro.Instance.RunIntro ();
		Inputs.Instance.ResetUFO ();
		DayCycle.Instance.ResetCycle ();
		sun.SetActive (true);
		moon.SetActive (false);
		abducted = 0;
		score = 0;
		scoreText.text = score.ToString ();
		AbducteePool.Instance.Reset ();
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
