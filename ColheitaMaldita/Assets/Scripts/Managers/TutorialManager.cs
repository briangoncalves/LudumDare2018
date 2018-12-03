using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    bool tutorialComplete;
    List<string> tutorialCompleted = new List<string>();
	public static TutorialManager instance;
    int count;
	void Awake()
	{
		instance = this;
	}

	public GameObject Language;
    private void Start()
    {
        tutorialComplete = PlayerPrefs.GetInt("tutorialComplete", 0) == 1;
        if (tutorialComplete)
        {
            FindObjectOfType<GodBehaviour>().StartTalking();
        }
        else
        {
            instance.Language = GameObject.Find("Language");
            GameManager.ShowText(instance.Language.GetComponent<XMLLanguageLoader>().TutorialLanguage.StartMessage);
        }


    }

    public void CheckTutorial(string action)
    {
        if (tutorialComplete || tutorialCompleted.Contains(action))
            return;

        tutorialCompleted.Add(action);

        count++;

        switch (action)
        {
            case "PlantPrepared":
			GameManager.ShowText(instance.Language.GetComponent<XMLLanguageLoader> ().TutorialLanguage.PlantPrepared);

                break;

            case "PlantPlanted":
			GameManager.ShowText(instance.Language.GetComponent<XMLLanguageLoader> ().TutorialLanguage.PlantPlanted);
                break;

            case "NeedWater":
			GameManager.ShowText(instance.Language.GetComponent<XMLLanguageLoader> ().TutorialLanguage.NeedWater);
                break;

            case "PlantDead":
			GameManager.ShowText(instance.Language.GetComponent<XMLLanguageLoader> ().TutorialLanguage.PlantDead);
                break;

            case "Autumn":
			GameManager.ShowText(instance.Language.GetComponent<XMLLanguageLoader> ().TutorialLanguage.Autumn);
                break;

        }

        if(count == 5)
        {
            tutorialComplete = true;
            FindObjectOfType<GodBehaviour>().StartTalking();
            PlayerPrefs.SetInt("tutorialComplete", 1);
        }
    }
}
