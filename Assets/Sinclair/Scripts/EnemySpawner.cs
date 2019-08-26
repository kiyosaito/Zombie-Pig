using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public Transform enemyParent;
	public Transform spawnpoints;
	public Transform player;

	float timer = 0f;
	float spawnRate = 3f;
	float spawnDist = 10f;

	List<Transform> availablePoints;

	void Start() {
		availablePoints = new List<Transform>();
	}

	void Update() {
		timer += Time.deltaTime;
		if (timer >= spawnRate) {
			timer -= spawnRate;
			spawnRate = (-Mathf.Cos(Time.time*.2f)*.5f+.5f)*3f+1.5f;
			availablePoints.Clear();
			foreach (Transform spawnpoint in spawnpoints) {
				if (Vector3.Distance(spawnpoint.position, player.position) >= spawnDist) {
					availablePoints.Add(spawnpoint);
				}
			}
			Instantiate(enemyPrefab, availablePoints[Random.Range(0, availablePoints.Count)].position, Quaternion.identity, enemyParent).GetComponent<Enemy>().speed = Random.Range(2f, 4f);
		}
	}

}
