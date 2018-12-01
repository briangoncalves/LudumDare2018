using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour {

	public GameObject Language;
	public Button btnStartGame;
	public Button btnCredits;
	public Button btnExit;
	public Button btnEnglish;
	public Button btnPortuguese;
	// Use this for initialization
	void Start () {
		Language = GameObject.Find("Language");
		btnPortuguese.onClick.AddListener(btnPortugueseClick);
		btnEnglish.onClick.AddListener(btnEnglishClick);
		btnExit.onClick.AddListener (btnExitClick);
		btnCredits.onClick.AddListener (btnCreditsClick);
	}

	void btnExitClick()
	{
		#if UNITY_EDITOR
		// Application.Quit() does not work in the editor so
		// UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}

	void btnCreditsClick()
	{
		SceneManager.LoadScene("Credits");
	}

	void btnPortugueseClick()
	{
		Language.GetComponent<XMLLanguageLoader> ().SetLanguage ("Portuguese");
	}

	void btnEnglishClick()
	{
		Language.GetComponent<XMLLanguageLoader> ().SetLanguage ("English");
	}

	// Update is called once per frame
	void Update () {
		var mainMenuLanguage = Language.GetComponent<XMLLanguageLoader>().MainMenuLanguage;
		btnStartGame.GetComponentInChildren<Text>().text = mainMenuLanguage.StartGame;
		btnCredits.GetComponentInChildren<Text>().text = mainMenuLanguage.Credits;
		btnExit.GetComponentInChildren<Text>().text = mainMenuLanguage.Exit;
	}
}
