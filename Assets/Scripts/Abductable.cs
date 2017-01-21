using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abductable : MonoBehaviour {

	private Vector3 initialPos;

	void Start () {
		initialPos = transform.position;
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.name == "Tractor Beam") {
			Debug.Log ("Beam me up, Scotty!");

			// TODO: Beam it up
		}

		if (other.gameObject.name == "UFO") {
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
}
