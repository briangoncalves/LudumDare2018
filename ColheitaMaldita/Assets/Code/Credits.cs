using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

	public Button btnBack;

	// Use this for initialization
	void Start () {
		Language = GameObject.Find("Language");
		btnBack.onClick.AddListener (btnBackClick);
		var mainMenuLanguage = Language.GetComponent<XMLLanguageLoader>().MainMenuLanguage;
		btnBack.GetComponentInChildren<Text>().text = mainMenuLanguage.Back;
	}

	void btnBackClick()
	{
		SceneManager.LoadScene ("MainMenu");
	}

	public GameObject Language;
	// Update is called once per frame
	void Update () {
		
	}
}
