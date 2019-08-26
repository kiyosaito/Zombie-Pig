// Written by Sinclair

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {

	// External references
	public GameObject gameUI;
	public GameObject deathPanel;
	public Joystick movementStick;
    public Joystick aimingStick;
	public GameObject bulletPrefab;
	public Transform shotOrigin;
	public Transform shotOrigin2;
	public Slider healthSlider;

	// Internal references
	Rigidbody rb;

	// Variables
	float maxHealth = 100f;
	float health = 100f;
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
        Vector2 aim = new Vector2(aimingStick.Horizontal, aimingStick.Vertical);
		rb.velocity = Vector3.zero;
		Vector3 dir = transform.forward;
		if (input != Vector2.zero) {
			dir = new Vector3(input.x, 0f, input.y);
			rb.velocity = dir.normalized * speed;
		}
		if (aim.magnitude > .2f) {
			dir = new Vector3(aim.x, 0f, aim.y);
			Shoot();
		}
		transform.forward = Vector3.Slerp(transform.forward, dir, Time.deltaTime*30f);
	}

	void Shoot() {
		if (shootTimer <= 0f) {
			shootTimer += shootRate;
			origin = !origin;
			Vector3 o = origin ? shotOrigin.position : shotOrigin2.position;
			Instantiate(bulletPrefab, o, Quaternion.identity).transform.forward = Quaternion.Euler(0f, origin ? 2f : -2f, 0f) * transform.forward;
		}
	}

	public void TakeDamage(float damage) {
		health -= damage;
		if (health <= 0) {
			Die();
		}
	}

	public void ChangeScene() {
		Time.timeScale = 1f;
		SceneManager.LoadScene(0);
	}

	void Die() {
		Time.timeScale = 0.01f;
		foreach (MeshRenderer rend in GetComponentsInChildren<MeshRenderer>()) {
			rend.enabled = false;
		}
		gameUI.SetActive(false);
		deathPanel.SetActive(true);
	}

	void Update() {
		Move();
		shootTimer = shootTimer < 0f ? 0f : shootTimer - Time.deltaTime;
		healthSlider.value = health / maxHealth;
	}
}
