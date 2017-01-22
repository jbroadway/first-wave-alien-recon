using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abductable : MonoBehaviour {

	public MeshRenderer mapMarker;

	private float initialPosY;

	void Start () {
		initialPosY = transform.position.y;
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.name == "Tractor Beam") {
			//Debug.Log ("Beam me up, Scotty!");

			StopAllCoroutines ();
			StartCoroutine (BeamMeUp ());
		}

		if (other.gameObject.name == "SpaceShip Hull") {
			int abducted = Score.Instance.Abducted ();

			Debug.Log (string.Format (
				"Abducted {0} this:{1} pool:{2}",
				abducted,
				gameObject.name,
				AbducteePool.Instance.abductees[abducted].gameObject.name
			));

			if (this == AbducteePool.Instance.abductees[abducted]) {
				Score.Instance.AddPoints ();

				// Disable and reset after captured
				gameObject.SetActive (false);
				transform.position = new Vector3 (
					transform.position.x,
					initialPosY,
					transform.position.z
				);

			} else {
				Score.Instance.Penalize ();

				// Reset after penalizing
				transform.position = new Vector3 (
					transform.position.x,
					initialPosY,
					transform.position.z
				);
			}
		}
	}

	IEnumerator BeamMeUp () {
		yield return new WaitForSeconds (0.2f);

		while (! Inputs.Instance.BeamReleased ()) {
			transform.position = new Vector3 (
				transform.position.x,
				transform.position.y + Inputs.Instance.beamSpeed,
				transform.position.z
			);

			yield return null;
		}

		// Drop to ground
		while (transform.position.y > initialPosY) {
			transform.position = new Vector3 (
				transform.position.x,
				transform.position.y - (Inputs.Instance.beamSpeed * 2f),
				transform.position.z
			);

			yield return null;
		}

		transform.position = new Vector3 (
			transform.position.x,
			initialPosY,
			transform.position.z
		);
	}
}
