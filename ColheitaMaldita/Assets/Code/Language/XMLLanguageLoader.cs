﻿using UnityEngine;
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
	public List<string> Languages { get; set; }
	public string SelectedLanguage { get; set; }
}

public class TutorialLanguage {
	public string StartMessage {
		get;
		set;
	}
	public string PlantPrepared {
		get;
		set;
	}
	public string PlantPlanted {
		get;
		set;
	}
	public string NeedWater {
		get;
		set;
	}
	public string PlantDead {
		get;
		set;
	}
	public string Autumn {
		get;
		set;
	}
}

public class CreditsLanguage {
	public string Back { get; set; }
}

public class XMLLanguageLoader : MonoBehaviour {

    public static string selectedLanguage;

	// Use this for initialization
	void Start () 
    {
		DontDestroyOnLoad(gameObject);
		MainMenuLanguage.Languages = new List<string>();
		LoadLanguagesXML();
		SetLanguage("English");
		SceneManager.LoadScene("MainMenu");
        selectedLanguage = "English";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SetLanguage(string language)
	{
		LoadKidsMessages(language);
		LoadMainMenu(language);
		LoadTutorial(language);
        selectedLanguage = language;
    }
	
	XDocument xmlDocKidsMessages;
	public List<string> KidsMessages = new List<string>();
	XDocument xmlDocMainMenu;
	public MainMenuLanguage MainMenuLanguage = new MainMenuLanguage();
	public CreditsLanguage CreditsLanguage = new CreditsLanguage();
	public TutorialLanguage TutorialLanguage = new TutorialLanguage ();
	XDocument xmlDocLanguage;
	XDocument xmlDocTutorial;
	public List<LanguageSelector> Languages = new List<LanguageSelector>();
	void LoadLanguagesXML()
	{
		Languages.Clear ();
		MainMenuLanguage.Languages.Clear ();
		//Assigning Xdocument xmlDoc. Loads the xml file from the file path listed. 
		xmlDocLanguage = XDocument.Load("Assets/Code/Language/Languages.xml");
		//This basically breaks down the XML Document into XML Elements. Used later. 
		var items = xmlDocLanguage.Descendants("Languages");
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
		CreditsLanguage.Back = item.Element ("Back").Value;
		MainMenuLanguage.SelectedLanguage = l.Language;
	}

	void LoadTutorial(string language)
	{
		var l = Languages.FirstOrDefault(x => x.Language == language);
		if (l == null) l = Languages.First();
		xmlDocTutorial = XDocument.Load("Assets/Code/Language/" + l.Folder + "/TutorialMessage.xml");
		var item = xmlDocTutorial.Descendants ("Messages").First ();
		TutorialLanguage.Autumn = item.Element ("Autumn").Value;
		TutorialLanguage.NeedWater = item.Element ("NeedWater").Value;
		TutorialLanguage.PlantDead = item.Element ("PlantDead").Value;
		TutorialLanguage.PlantPlanted = item.Element ("PlantPlanted").Value;
		TutorialLanguage.PlantPrepared = item.Element ("PlantPrepared").Value;
		TutorialLanguage.StartMessage = item.Element ("StartMessage").Value;
	}
}
