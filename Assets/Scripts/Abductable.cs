using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abductable : MonoBehaviour {

	private Vector3 initialPos;

	void Start () {
		initialPos = transform.position;
	}

	void OnCollisionEnter (Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
			if (contact.otherCollider.gameObject.name == "Tractor Beam") {
				Debug.Log ("Beam me up, Scotty!");
			}

			if (contact.otherCollider.gameObject.name == "UFO") {
				Score.Instance.AddPoint ();
			}
        }
	}
}
