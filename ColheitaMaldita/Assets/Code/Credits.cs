using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

	public Button btnBack;

	// Use this for initialization
	void Start () {
		btnBack.onClick.AddListener (btnBackClick);
	}

	void btnBackClick()
	{
		SceneManager.LoadScene ("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
