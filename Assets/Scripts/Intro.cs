using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {

	public static Intro Instance;

	public float speed = 5f;

	public float pauseFor = 2f;

	public GameObject[] enableAfter;

	// Use this for initialization
	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else {
			Debug.LogWarning ("There should be only one Intro component!");
		}
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (DoRunIntro ());
	}

	public void RunIntro () {
		StopAllCoroutines ();
		StartCoroutine (DoRunIntro ());
	}

	IEnumerator DoRunIntro () {
		foreach (GameObject obj in enableAfter) {
			obj.SetActive (false);
		}
		
		RectTransform rect = GetComponent<RectTransform> ();

		while (rect.offsetMin.x < 0f) {
			rect.offsetMin = new Vector2 (
				rect.offsetMin.x + speed,
				rect.offsetMin.y
			);

			yield return null;
		}

		yield return new WaitForSeconds (pauseFor);

		while (rect.offsetMin.x < 2000f) {
			rect.offsetMin = new Vector2 (
				rect.offsetMin.x + speed,
				rect.anchorMin.y
			);

			yield return null;
		}

		//gameObject.SetActive (false);
		rect.offsetMin = new Vector2 (
			-3000f,
			rect.anchorMin.y
		);

		foreach (GameObject obj in enableAfter) {
			obj.SetActive (true);
		}
	}
}
