using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	Rigidbody rb;

	float speed = 10f;
	float damage = 10f;

	float timer = 0f;
	float lifeSpan = 5f;

	void Start() {
		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward * speed;
	}

	void Die() {
		Destroy(gameObject);
	}

	void Update() {
		timer += Time.deltaTime;
		if (timer >= lifeSpan) {
			Die();
		}
	}

	//void OnCollisionEnter(Collision collision) {
	//	Enemy enemy = collision.gameObject.GetComponent<Enemy>();
	//	if (!enemy) {
	//		transform.forward = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
	//		rb.velocity = transform.forward * speed;
	//	}
	//}


	void OnTriggerEnter(Collider other) {
		Enemy enemy = other.GetComponent<Enemy>();
		if (enemy) {
			enemy.TakeDamage(damage);
			Destroy(gameObject);
		}

		if (!(other.tag == "Player")) {
			Destroy(gameObject);
		}

        // Boucing off walls (currently not working)
        /*else {
            RaycastHit hit;
            if (Physics.Raycast(transform.position-transform.forward*.1f, transform.forward, out hit, 1f)) {
                Debug.Log("A");
                transform.forward = Vector3.Reflect(transform.forward, hit.normal);
                rb.velocity = transform.forward * speed;
            }
        }*/
	}
}
