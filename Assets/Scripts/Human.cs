using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour {

	public Animator animator;

	// Use this for initialization
	void Update () {
		animator.Play ("Idle");
	}
}
