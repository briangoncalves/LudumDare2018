using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour {

	public static CreditsManager instance;

	private void Awake()
	{ 
		instance = this;
	}


	public Button btnBack;

	// Use this for initialization
	void Start () {
		instance.Language = GameObject.Find("Language");
		instance.btnBack.onClick.AddListener (btnBackClick);
		var credits = instance.Language.GetComponent<XMLLanguageLoader>().CreditsLanguage;
		instance.btnBack.GetComponentInChildren<Text>().text = credits.Back;
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
