using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

	public static GameOverManager instance;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}

	public float Timer;
	// Update is called once per frame
	void Update () {
		Timer -= Time.deltaTime;
		if (Timer <= 0) {
			Timer = 0;
			GameObject.Find("Language").GetComponent<AudioSource> ().Play ();
			SceneManager.LoadScene ("MainMenu");
		}
	}
}
