using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour {

	public GameObject earth;

	public GameObject clouds;

	public float movementSpeed = 0.05f;

	public GameObject tractorBeam;

	public float closedBeam = 0f;

	public float openBeam = 40f;

	public float beamSpeed = 0.5f;

	private float initialBeamY;

	private float initialBeamScale;

	void Start () {
		initialBeamY = tractorBeam.transform.position.y;
		initialBeamScale = tractorBeam.transform.localScale.z;
	}
	
	// Update is called once per frame
	void Update () {
		if (! TractorBeam ()) {
			MoveShip ();
		}
	}

	void MoveShip () {
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

		clouds.transform.position = new Vector3 (
			clouds.transform.position.x + addX,
			clouds.transform.position.y,
			clouds.transform.position.z + addZ
		);
	}

	bool TractorBeam () {
		bool open = false;

		if (Input.GetKey (KeyCode.Space)) {
			// Lower beam
			if (tractorBeam.transform.localScale.z < openBeam) {

				tractorBeam.transform.localScale = new Vector3 (
					tractorBeam.transform.localScale.x,
					tractorBeam.transform.localScale.y,
					tractorBeam.transform.localScale.z + beamSpeed
				);

				tractorBeam.transform.localPosition = new Vector3 (
					tractorBeam.transform.localPosition.x,
					tractorBeam.transform.localPosition.y - beamSpeed,
					tractorBeam.transform.localPosition.z
				);
			}

			open = true;

		} else {
			// Retract beam
			if (tractorBeam.transform.localScale.z > closedBeam) {
				tractorBeam.transform.localScale = new Vector3 (
					tractorBeam.transform.localScale.x,
					tractorBeam.transform.localScale.y,
					tractorBeam.transform.localScale.z - beamSpeed
				);

				tractorBeam.transform.localPosition = new Vector3 (
					tractorBeam.transform.localPosition.x,
					tractorBeam.transform.localPosition.y + beamSpeed,
					tractorBeam.transform.localPosition.z
				);

				open = true;
			}
		}

		return open;
	}
}
