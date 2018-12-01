using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public float Timer;
	// Update is called once per frame
	void Update () {
		Timer -= Time.deltaTime;
		if (Timer <= 0) {
			Timer = 0;
			SceneManager.LoadScene ("MainMenu");
		}
	}
}
