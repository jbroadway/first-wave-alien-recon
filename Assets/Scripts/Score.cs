using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public static Score Instance;

	public Text scoreText;

	public Text highestScoreText;

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

		DayCycle.Instance.OnChange += HandleDayCycleChange;
	}

	public void AddPoints () {
		abducted++;

		// Only one point during the day because it's heatscore
		score += DayCycle.Instance.IsDay () ? 1 : 2;

		UpdateHighestScore ();

		scoreText.text = score.ToString ();

		if (abducted >= AbducteePool.Instance.abductees.Length) {
			StartCoroutine (ResetGame ());
		}
	}

	IEnumerator ResetGame () {
		yield return new WaitForSeconds (0.5f);
		AbducteePool.Instance.Reset ();
		Intro.Instance.RunIntro ();
		Inputs.Instance.ResetUFO ();
		DayCycle.Instance.ResetCycle ();
		sun.SetActive (true);
		moon.SetActive (false);
		abducted = 0;
		score = 0;
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
