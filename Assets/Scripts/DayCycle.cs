using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour {

	public float startOffset = 0.1f;

	public float timeIncrement = 0.1f;

	public float offsetIncrement = 0.05f;

	private float curOffset;

	private MeshRenderer ren;

	// Use this for initialization
	void Start () {
		ren = GetComponent<MeshRenderer> ();
		curOffset = startOffset;

		StartCoroutine (IncrementTime ());
	}

	IEnumerator IncrementTime () {
		WaitForSeconds wfs = new WaitForSeconds (timeIncrement);

		while (true) {
			curOffset += offsetIncrement;
			if (curOffset > 1f) {
				curOffset = 0f;
			}

			//Debug.Log (IsDay () ? "Day" : "Night");

			ren.material.SetTextureOffset ("_MainTex", new Vector2 (curOffset, 0f));

			yield return wfs;
		}
	}

	public bool IsDay () {
		if (curOffset >= 0f && curOffset <= 0.2f || curOffset >= 0.75f) {
			return true;
		}
		return false;
	}

	public bool IsNight () {
		return ! IsDay ();
	}
}
