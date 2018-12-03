using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {

	public static GameOverManager instance;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		NumberOfYearsSurvived = GameManager.instance.yearsCount;
		GameObject.Find("Survived").GetComponent<Text>().text = "You survived " + NumberOfYearsSurvived + " years"; 
	}

	public int NumberOfYearsSurvived = 0;

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
