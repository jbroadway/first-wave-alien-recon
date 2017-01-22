using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DayCycle : MonoBehaviour {

	public static DayCycle Instance;

	[Serializable]
	public class DayCycleEvent : UnityEvent<DayCycle> { }

	public DayCycleEvent OnChange = new DayCycleEvent ();

	public float startOffset = 0.1f;

	public float timeIncrement = 0.1f;

	public float offsetIncrement = 0.05f;

	private float curOffset;

	private MeshRenderer ren;

	private bool day = true;

	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else {
			Debug.LogWarning ("There should be only one DayCycle component!");
		}
	}

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

			ren.material.SetTextureOffset ("_MainTex", new Vector2 (curOffset, 0f));

			bool isDay = IsDay ();
			if (isDay != day) {
				day = isDay;

				if (OnChange != null) {
					OnChange.Invoke (this);
				}
			}

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

	public void ResetCycle () {
		curOffset = startOffset;
		day = true;

		ren.material.SetTextureOffset ("_MainTex", new Vector2 (curOffset, 0f));
	}
}
