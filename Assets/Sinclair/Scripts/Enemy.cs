using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public Transform player;
	public ScoreManager scoreManager;

	NavMeshAgent agent;

	float health = 50f;
	public float speed = 2f;
	float damage = 20f;

	void Start() {
		agent = GetComponent<NavMeshAgent>();
		agent.speed = speed;
		if (!player) {
			player = FindObjectOfType<Player>().transform;
		}
		if (!scoreManager) {
			scoreManager = FindObjectOfType<ScoreManager>();
		}
	}

	void Die() {
		scoreManager.IncrementScore();
		Destroy(gameObject);
	}

	public void TakeDamage(float d) {
		health -= d;
		if (health <= 0) {
			Die();
		}
	}

	void OnCollisionStay(Collision collision) {
		if (collision.transform.tag == "Player") {
			collision.transform.GetComponent<Player>().TakeDamage(damage * Time.deltaTime);
		}
	}

	void Update() {
		agent.destination = player.position;
	}
}
