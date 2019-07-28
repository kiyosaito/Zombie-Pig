using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	float health = 50f;

	void Start() {

	}

	void Die() {
		Destroy(gameObject);
	}

	public void TakeDamage(float d) {
		health -= d;
		if (health <= 0) {
			Die();
		}
	}

	void Update() {

	}
}
