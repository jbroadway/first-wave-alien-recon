using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abductable : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.name == "Tractor Beam") {
			//Debug.Log ("Beam me up, Scotty!");

			StopAllCoroutines ();
			StartCoroutine (BeamMeUp ());
		}

		if (other.gameObject.name == "SpaceShip Hull") {
			// Double points at night
			if (DayCycle.Instance.IsNight ()) {
				Score.Instance.AddPoint ();
				Score.Instance.AddPoint ();

			// Only one point during the day, because it's heatscore
			} else {
				Score.Instance.AddPoint ();
			}

			// Destroy after captured
			Destroy (gameObject);
		}
	}

	IEnumerator BeamMeUp () {
		yield return new WaitForSeconds (0.1f);

		while (! Inputs.Instance.BeamReleased ()) {
			transform.position = new Vector3 (
				transform.position.x,
				transform.position.y + Inputs.Instance.beamSpeed,
				transform.position.z
			);

			yield return null;
		}

		// Drop to ground
		while (transform.position.y > 0f) {
			transform.position = new Vector3 (
				transform.position.x,
				transform.position.y - (Inputs.Instance.beamSpeed * 2f),
				transform.position.z
			);

			yield return null;
		}

		transform.position = new Vector3 (
			transform.position.x,
			0f,
			transform.position.z
		);
	}
}
