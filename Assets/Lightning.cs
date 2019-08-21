using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour {

	public Light lightning;
	public Light spotLight;

	void Update() {
		lightning.intensity = Mathf.Clamp(Mathf.Pow(Mathf.PerlinNoise(Time.time*2f, 0f), 30f) * 500f, 0f, 5f);
		spotLight.intensity = Mathf.Clamp(Mathf.Pow(Mathf.PerlinNoise(Time.time * 3.14f, 1f), 1f)*10f, 0f, 2f);
	}
}
