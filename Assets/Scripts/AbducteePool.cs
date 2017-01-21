using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbducteePool : MonoBehaviour {

	public static AbducteePool Instance;

	public Abductable[] abductees;

	// Use this for initialization
	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else {
			Debug.LogWarning ("There should be only one AbducteePool component!");
		}

		abductees = Object.FindObjectsOfType<Abductable> ();
		//Debug.Log ("Abductees: " + abductees.Length);

		ShufflePositions ();
	}
	
	void ShufflePositions () {
		// Randomize positions
		Vector3 tmpV;
		for (int i = 0; i < abductees.Length; i++) {
			var r = Random.Range (0, i);
			tmpV = abductees[i].transform.position;
			abductees[i].transform.position = abductees[r].transform.position;
			abductees[r].transform.position = tmpV;
		}

		// Randomize order to find them
		Abductable tmpA;
		for (int i = 0; i < abductees.Length; i++) {
			var r = Random.Range (0, i);
			tmpA = abductees[i];
			abductees[i] = abductees[r];
			abductees[r] = tmpA;
		}
	}

	public void Reset () {
		ShufflePositions ();

		foreach (Abductable ab in abductees) {
			ab.gameObject.SetActive (true);
		}
	}
}
