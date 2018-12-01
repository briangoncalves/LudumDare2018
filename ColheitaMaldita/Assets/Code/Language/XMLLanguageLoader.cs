using UnityEngine;
using System.Collections;
using System.Collections.Generic; //Needed for Lists
using System.Xml; //Needed for XML functionality
using System.Xml.Serialization; //Needed for XML Functionality
using System.IO;
using System.Xml.Linq; //Needed for XDocument
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class LanguageSelector {
	public string Language { get; set; }
	public string Folder { get; set; }
}

public class MainMenuLanguage {
	public string StartGame { get; set; }
	public string Credits { get; set; }
	public string Exit { get; set; }
	public string Back { get; set; }
	public List<string> Languages { get; set; }
	public string SelectedLanguage { get; set; }
}

public class XMLLanguageLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		MainMenuLanguage.Languages = new List<string>();
		LoadLanguagesXML();
		SetLanguage("English");
		SceneManager.LoadScene("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SetLanguage(string language)
	{
		LoadKidsMessages(language);
		LoadMainMenu(language);
	}
	
	XDocument xmlDocKidsMessages;
	public List<string> KidsMessages = new List<string>();
	XDocument xmlDocMainMenu;
	public MainMenuLanguage MainMenuLanguage = new MainMenuLanguage();
	XDocument xmlDocLanguage;
	public List<LanguageSelector> Languages = new List<LanguageSelector>();
	void LoadLanguagesXML()
	{
		Languages.Clear ();
		MainMenuLanguage.Languages.Clear ();
		//Assigning Xdocument xmlDoc. Loads the xml file from the file path listed. 
		xmlDocLanguage = XDocument.Load("Assets/Code/Language/Languages.xml");
		//This basically breaks down the XML Document into XML Elements. Used later. 
		var items = xmlDocLanguage.Descendants("Languages");
		Debug.Log(items.Count().ToString());
		foreach(var item in items)
		{
			var l = new LanguageSelector{ 
			              	Language = item.Element("Language").Value,
			              	Folder = item.Element("Folder").Value
			};
			Languages.Add(l);
			MainMenuLanguage.Languages.Add(l.Language);
		}
	}
	
	void LoadKidsMessages(string language)
	{
		var l = Languages.FirstOrDefault(x => x.Language == language);
		if (l == null) l = Languages.First();
		xmlDocKidsMessages = XDocument.Load("Assets/Code/Language/" + l.Folder + "/KidsMessage.xml");
		var items = xmlDocKidsMessages.Descendants("Messages");
		foreach(var item in items)
		{
			KidsMessages.Add(item.Element("Message").Value);
		}
	}
	
	void LoadMainMenu(string language)
	{
		var l = Languages.FirstOrDefault(x => x.Language == language);
		if (l == null) l = Languages.First();
		xmlDocMainMenu = XDocument.Load("Assets/Code/Language/" + l.Folder + "/MainMenu.xml");
		var item = xmlDocMainMenu.Descendants("Menu").First();
		MainMenuLanguage.StartGame = item.Element("StartGame").Value;
		MainMenuLanguage.Credits = item.Element("Credits").Value;
		MainMenuLanguage.Exit = item.Element("Exit").Value;
		MainMenuLanguage.Back = item.Element ("Back").Value;
		MainMenuLanguage.SelectedLanguage = l.Language;
	}
}
