using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	Vector3 difference;

	void Start() {
		difference = transform.position - target.position;
	}

	void Update() {
		transform.position = target.position + difference;
	}
}
