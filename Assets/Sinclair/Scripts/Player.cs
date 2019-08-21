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
	public Transform shotOrigin2;
    public Transform turret;

	// Internal references
	Rigidbody rb;

	// Variables
	float shootTimer = 0f;
	float shootRate = .1f;
	float speed = 4f;
	float movementThreshold = 0.2f;
    float aimingThreshold = 0.2f;
    Vector3 turretDirection = Vector3.forward;
	bool origin;

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
			transform.forward = Vector3.Lerp(turret.forward, rb.velocity, Time.deltaTime * 10f);
		}
        Vector2 aim = new Vector2(aimingStick.Horizontal, aimingStick.Vertical);

        if (aim.x * aim.x + aim.y * aim.y > aimingThreshold * aimingThreshold)
        {
            Shoot();
	        turret.forward = Vector3.Lerp(turret.forward, new Vector3(aim.x, 0f, aim.y), Time.deltaTime * 20f);
        }
	}

	void Shoot() {
		if (shootTimer <= 0f) {
			shootTimer += shootRate;
			origin = !origin;
			Vector3 o = origin ? shotOrigin.position : shotOrigin2.position;
			Instantiate(bulletPrefab, o, Quaternion.identity).transform.forward = Quaternion.Euler(0f, origin ? 2f : -2f, 0f) * turret.forward;
		}
	}

	void Update() {
		Move();
		
		shootTimer = shootTimer < 0f ? 0f : shootTimer - Time.deltaTime;
	}
}
