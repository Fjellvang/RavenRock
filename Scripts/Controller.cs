using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
	public float friction = 0.5f;
	public float acceleration = 10f;
	float velocity = 0;
	public float timer = 1f;
	float origtime;
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		origtime = timer;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0) {
			timer -= Time.deltaTime;

		}
		float axis = 0;
		axis = Input.GetAxis ("Horizontal");
		Debug.Log (axis);
		if (axis != 0) {
			velocity += acceleration * Time.deltaTime * axis;
		}
		if (velocity != 0) {
			velocity -= velocity * friction * Time.deltaTime;
			this.transform.position += new Vector3 (velocity, 0, 0);
			anim.SetBool ("isMoving", true);
		}
		if (axis == 0) {
			anim.SetBool ("isMoving", false);
		}
		if (axis < 0) {
			transform.localScale = new Vector3 (-1, 1, 1);
		} else if(axis > 0) {
			transform.localScale = new Vector3 (1, 1, 1);

		}
		if(Input.GetKeyDown(KeyCode.JoystickButton1)){
			Debug.Log("PRESS X");
			//anim.SetBool("shootButton", true);
			anim.Play ("Attack");
			timer = origtime;
		} else{
			if (timer <= 0) {
				anim.SetBool ("shootButton", false);
			}
		}
	}
}
