﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float speed = 10f; //The speed in m/s
	public float fireRate = 0.3f; // Seconds/shot (unused)
	public float health = 10;
	public int score = 100; //Points earned for destroying this enemy.

	public bool ___________;

	public Bounds bounds; //The bounds of this and its children.
	public Vector3 boundsCenterOffset; // Dist of bounds.center from position.


	void Awake() {
		InvokeRepeating ("CheckOffscreen", 0f, 2f);
	}

	// Update is called once per frame
	void Update () {
		Move ();	
	}

	public virtual void Move() {
		Vector3 tempPos = pos;
		tempPos.y -= speed * Time.deltaTime;
		pos = tempPos;
	}

	//This is a property: A method that acts like a field.
	public Vector3 pos {
		get{
			return (this.transform.position);
		}
		set {
			this.transform.position = value;
		}
	}

	void CheckOffscreen() {
		//If bounds are still their default value...
		if (bounds.size == Vector3.zero) {
		//Then set them.
			bounds = Utils.CombineBoundsOfChildren(this.gameObject);
			//Also find the diff between bounds.center & transform.position.
			boundsCenterOffset = bounds.center - transform.position;
		}

		//Everytime, update the bounds to the current position.
		bounds.center = transform.position + boundsCenterOffset;
		//Check to see whether the bounds are completely offscreen.
		Vector3 off = Utils.ScreenBoundsCheck (bounds, BoundsTest.offScreen);
		if (off != Vector3.zero) {
		//If this enemy has gone off the bottom edge of the screen.
			if (off.y < 0) {
			//Then destroy it.
				Destroy (this.gameObject);
			}
		}
	}

}
