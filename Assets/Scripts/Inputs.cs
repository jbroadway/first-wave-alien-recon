using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour {

	public static Inputs Instance;

	public GameObject cam;

	public GameObject skyDome;

	public GameObject UFO;

	public float movementSpeed = 0.05f;

	public GameObject tractorBeam;

	public float closedBeam = 0f;

	public float openBeam = 40f;

	public float beamSpeed = 0.5f;

	private bool beamReleased = true;

	private Vector3 initialPosition;

	private Vector3 initialCamPos;

	private Vector3 initialSkyPos;

	private AudioSource audioSource;

	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else {
			Debug.LogWarning ("There should be only one Inputs component!");
		}
	}

	void Start () {
		audioSource = GetComponent<AudioSource> ();

		initialPosition = UFO.transform.position;
		initialCamPos = cam.transform.position;
		initialSkyPos = skyDome.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (! TractorBeam ()) {
			MoveShip ();
		}
	}

	public void ResetUFO () {
		UFO.transform.position = initialPosition;
		cam.transform.position = initialCamPos;
		skyDome.transform.position = initialSkyPos;
		
		tractorBeam.transform.localScale = new Vector3 (
			tractorBeam.transform.localScale.x,
			tractorBeam.transform.localScale.y,
			closedBeam
		);

		tractorBeam.transform.localPosition = new Vector3 (
			tractorBeam.transform.localPosition.x,
			closedBeam,
			tractorBeam.transform.localPosition.z
		);
	}

	void MoveShip () {
		float addX = 0f;
		float addZ = 0f;

		if (Input.GetKey (KeyCode.LeftArrow)) {
			addX = -movementSpeed;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			addX = movementSpeed;
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			addZ = -movementSpeed;
		} else if (Input.GetKey (KeyCode.UpArrow)) {
			addZ = movementSpeed;
		}

		cam.transform.position = new Vector3 (
			cam.transform.position.x + addX,
			cam.transform.position.y,
			cam.transform.position.z + addZ
		);

		skyDome.transform.position = new Vector3 (
			skyDome.transform.position.x + addX,
			skyDome.transform.position.y,
			skyDome.transform.position.z + addZ
		);

		UFO.transform.position = new Vector3 (
			UFO.transform.position.x + addX,
			UFO.transform.position.y,
			UFO.transform.position.z + addZ
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
			beamReleased = false;
			audioSource.mute = false;

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

			beamReleased = true;
			audioSource.mute = true;
		}

		return open;
	}

	public bool BeamReleased () {
		return beamReleased;
	}
}
