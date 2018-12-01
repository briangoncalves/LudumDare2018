using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public static MainMenuManager instance;

	void Awake()
	{
		instance = this;
	}

	public GameObject Language;
	public Button btnStartGame;
	public Button btnCredits;
	public Button btnExit;
	public Dropdown drpLanguage;
	// Use this for initialization
	void Start () {
		instance.Language = GameObject.Find("Language");
		instance.drpLanguage.ClearOptions ();
		instance.drpLanguage.AddOptions (instance.Language.GetComponent<XMLLanguageLoader> ().MainMenuLanguage.Languages);
		var selectedIndex = instance.Language.GetComponent<XMLLanguageLoader> ().MainMenuLanguage.Languages.IndexOf(
			instance.Language.GetComponent<XMLLanguageLoader> ().MainMenuLanguage.SelectedLanguage);
		instance.drpLanguage.value = selectedIndex;
		instance.drpLanguage.onValueChanged.AddListener (
			delegate { 
				instance.drpLanguageChange(instance.drpLanguage); 
		});
		instance.btnExit.onClick.AddListener (btnExitClick);
		instance.btnCredits.onClick.AddListener (btnCreditsClick);
		instance.btnStartGame.onClick.AddListener (btnStartGameClick);
	}

	void btnStartGameClick(){
		SceneManager.LoadScene ("SampleScene"); 
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
		instance.Language.GetComponent<XMLLanguageLoader> ().SetLanguage (instance.drpLanguage.options[drpLanguage.value].text);
	}

	// Update is called once per frame
	void Update () {
		var mainMenuLanguage = instance.Language.GetComponent<XMLLanguageLoader>().MainMenuLanguage;
		instance.btnStartGame.GetComponentInChildren<Text>().text = mainMenuLanguage.StartGame;
		instance.btnCredits.GetComponentInChildren<Text>().text = mainMenuLanguage.Credits;
		instance.btnExit.GetComponentInChildren<Text>().text = mainMenuLanguage.Exit;
	}
}
