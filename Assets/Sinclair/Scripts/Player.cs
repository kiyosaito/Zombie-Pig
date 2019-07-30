// Written by Sinclair

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {

	// External references
	public Joystick movementStick;
    public Joystick aimingStick;
	public GameObject bulletPrefab;
	public Transform shotOrigin;
    public Transform turret;

	// Internal references
	Rigidbody rb;

	// Variables
	float shootTimer = 0f;
	float shootRate = .1f;
	float speed = 2f;
	float movementThreshold = 0.2f;
    float aimingThreshold = 0.2f;
    Vector3 turretDirection;

	void Start() {
		rb = GetComponent<Rigidbody>();
	}
    
	void Move() {
		// Gets input from joystick
		Vector2 input = new Vector2(movementStick.Horizontal, movementStick.Vertical);
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
        Vector2 aim = new Vector2(aimingStick.Horizontal, aimingStick.Vertical);
        if (aim != Vector2.zero) {
            turretDirection = new Vector3(aim.x, 0f, aim.y);
        }
        turret.forward = turretDirection;
        if (aim.x * aim.x + aim.y * aim.y > aimingThreshold * aimingThreshold)
        {
            Shoot();
        }
	}

	void Shoot() {
		if (shootTimer <= 0f) {
			shootTimer += shootRate;

			Instantiate(bulletPrefab, shotOrigin.position, Quaternion.identity).transform.forward = turret.forward;
		}
	}

	void Update() {
		Move();
		shootTimer = shootTimer < 0f ? 0f : shootTimer - Time.deltaTime;
	}
}
