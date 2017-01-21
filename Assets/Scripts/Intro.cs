using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {

	public float speed = 5f;

	public float pauseFor = 2f;

	public GameObject[] enableAfter;

	// Use this for initialization
	IEnumerator Start () {
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

		foreach (GameObject obj in enableAfter) {
			obj.SetActive (true);
		}

		gameObject.SetActive (false);
	}
}
