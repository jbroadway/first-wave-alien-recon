using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour {

	public Animator animator;

	private Abductable abductable;

	void Start () {
		abductable = GetComponent<Abductable> ();
		animator.speed = 2f;
	}

	// Use this for initialization
	void Update () {
		if (abductable.IsBeingAbducted ()) {
			animator.Play ("Run_Static");
		} else {
			animator.Play ("Idle");
		}
	}
}
