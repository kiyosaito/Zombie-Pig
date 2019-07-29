// Written by Sinclair

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {

	// External references
	public Joystick joystick;
	public GameObject bulletPrefab;
	public Transform shotOrigin;

	// Internal references
	Rigidbody rb;

	// Variables
	float shootTimer = 0f;
	float shootRate = .1f;
	float speed = 2f;
	float movementThreshold = 0.25f;

	void Start() {
		rb = GetComponent<Rigidbody>();
	}
    
	void Move() {
		// Gets input from joystick
		Vector2 input = new Vector2(joystick.Horizontal, joystick.Vertical);
		// If input is less than movement threshold, set input to zero
		if (input.x * input.x + input.y * input.y < movementThreshold * movementThreshold) {
			input = input.normalized*0.001f;
		} else {
			input = input.normalized;
		}
		// Sets velocity to input direction
		rb.velocity = new Vector3(input.x, 0f, input.y) * speed;
		// Faces the player towards movement direction
		if (input != Vector2.zero) {
			transform.forward = rb.velocity;
		}
	}

	public void Shoot() {
		if (shootTimer <= 0f) {
			shootTimer += shootRate;

			Instantiate(bulletPrefab, shotOrigin.position, Quaternion.identity).transform.forward = transform.forward;
		}
	}

	void Update() {
		Move();
		shootTimer = shootTimer < 0f ? 0f : shootTimer - Time.deltaTime;
	}
}
