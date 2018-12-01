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
	public Dropdown drpLanguage;
	// Use this for initialization
	void Start () {
		Language = GameObject.Find("Language");
		drpLanguage.ClearOptions ();
		drpLanguage.AddOptions (Language.GetComponent<XMLLanguageLoader> ().MainMenuLanguage.Languages);
		var selectedIndex = Language.GetComponent<XMLLanguageLoader> ().MainMenuLanguage.Languages.IndexOf(
			Language.GetComponent<XMLLanguageLoader> ().MainMenuLanguage.SelectedLanguage);
		drpLanguage.value = selectedIndex;
		drpLanguage.onValueChanged.AddListener (
			delegate { 
				drpLanguageChange(drpLanguage); 
		});
		btnExit.onClick.AddListener (btnExitClick);
		btnCredits.onClick.AddListener (btnCreditsClick);
		btnStartGame.onClick.AddListener (btnStartGameClick);
	}

	void btnStartGameClick(){
		SceneManager.LoadScene ("GameOver"); 
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

	void drpLanguageChange(Dropdown change)
	{
		Language.GetComponent<XMLLanguageLoader> ().SetLanguage (drpLanguage.options[drpLanguage.value].text);
	}

	// Update is called once per frame
	void Update () {
		var mainMenuLanguage = Language.GetComponent<XMLLanguageLoader>().MainMenuLanguage;
		btnStartGame.GetComponentInChildren<Text>().text = mainMenuLanguage.StartGame;
		btnCredits.GetComponentInChildren<Text>().text = mainMenuLanguage.Credits;
		btnExit.GetComponentInChildren<Text>().text = mainMenuLanguage.Exit;
	}
}
