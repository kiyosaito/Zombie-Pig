using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text text;

	public int score = 0;

	public void IncrementScore() {
		score++;
		text.text = score.ToString();
	}

	void Start() {
		text.text = score.ToString();
	}

}
