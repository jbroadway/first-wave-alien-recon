using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour {

	public GameObject earth;

	public float movementSpeed = 0.05f;
	
	// Update is called once per frame
	void Update () {
		float addX = 0f;
		float addZ = 0f;

		if (Input.GetKey (KeyCode.LeftArrow)) {
			addX = movementSpeed;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			addX = -movementSpeed;
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			addZ = movementSpeed;
		} else if (Input.GetKey (KeyCode.UpArrow)) {
			addZ = -movementSpeed;
		}

		earth.transform.position = new Vector3 (
			earth.transform.position.x + addX,
			earth.transform.position.y,
			earth.transform.position.z + addZ
		);
	}
}
