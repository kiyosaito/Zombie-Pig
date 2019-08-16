using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public Transform player;

	NavMeshAgent agent;

	float health = 50f;
	float speed = 2f;

	void Start() {
		agent = GetComponent<NavMeshAgent>();
		agent.speed = speed;
		if (!player) {
			player = FindObjectOfType<Player>().transform;
		}
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
		agent.destination = player.position;
	}
}
