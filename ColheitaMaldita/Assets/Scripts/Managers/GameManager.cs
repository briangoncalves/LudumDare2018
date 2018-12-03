using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance;

    private void Awake()
    { 
        instance = this;
    }


    private void Start()
    {
        plants = FindObjectsOfType<PlantBehaviour>();
        childQuantitie = worldVariables.startChildren;
        childAvailable = worldVariables.startChildren;
        InvokeRepeating("ChangeSeason", worldVariables.SeasonTime, worldVariables.SeasonTime);
        season = seasons.spring;
		seasonsFeedback.sprite = worldVariables.spring;
		seasonsSkyFeedback.sprite = worldVariables.springSky;
		seasonsIconFeedback.sprite = worldVariables.springIcon;
		txtChildren.text = childAvailable + "/" + childQuantitie;
    }


    public Text txtChildren;
    public Text txtFood;
    [HideInInspector]
    public int childQuantitie;
    [HideInInspector]
    public int childAvailable;

    public int yearsCount;

    public seasons season = seasons.spring;

    public int food;

    [HideInInspector]
    public PlantBehaviour[] plants;

    public PlantVariables plantVariables;

    public WorldVariables worldVariables;

    public SpriteRenderer seasonsFeedback;

	public SpriteRenderer seasonsSkyFeedback;

	public SpriteRenderer seasonsIconFeedback;

    public KidSpawnerBehaviour kidSpawner;

	public GameObject MainGame;
	public GameObject MiniGame;
	public GameObject MainCamera;

    public GameObject[] adults;

    public Text Speech;

    public static bool UseChild()
    {
        if(instance.childAvailable == 0)
        {
            return false;
        }

        instance.childAvailable--;
        instance.txtChildren.text = instance.childAvailable + "/" + instance.childQuantitie;
        return true;
    }

    public static void ReturnChild(KidBehaviour kid)
    {
        instance.childAvailable++;
        instance.childAvailable = instance.childAvailable > instance.childQuantitie ? instance.childQuantitie : instance.childAvailable;
        instance.txtChildren.text = instance.childAvailable + "/" + instance.childQuantitie;
        kid.GoBack();
        kid = null;
    }


    public void ChangeSeason()
    {
        switch(season)
        {
		case seasons.autumn:
			season = seasons.winter;
			seasonsFeedback.sprite = worldVariables.winter;
			seasonsSkyFeedback.sprite = worldVariables.winterSky;
			seasonsIconFeedback.sprite = worldVariables.winterIcon;
                if (instance.food >= instance.childQuantitie)
                {
                    instance.food = instance.food - instance.childQuantitie;

                }
                else
                {
                    instance.childQuantitie = instance.food;
                    instance.childAvailable = instance.childQuantitie;
                    instance.food = 0;
                    if(childQuantitie <= 0)
                    {
						UnityEngine.SceneManagement.SceneManager.LoadScene ("GameOver");
                    }
                }

                instance.txtChildren.text = instance.childAvailable + "/" + instance.childQuantitie;
                break;

            case seasons.spring:
                season = seasons.summer;
                seasonsFeedback.sprite = worldVariables.summer;
			seasonsSkyFeedback.sprite = worldVariables.summerSky;
			seasonsIconFeedback.sprite = worldVariables.summerIcon;
                break;
            case seasons.summer:
                season = seasons.autumn;
                seasonsFeedback.sprite = worldVariables.autumn;
			seasonsSkyFeedback.sprite = worldVariables.autumnSky;
			seasonsIconFeedback.sprite = worldVariables.autumnIcon;
                break;

            case seasons.winter:
                season = seasons.spring;
                seasonsFeedback.sprite = worldVariables.spring;
			seasonsSkyFeedback.sprite = worldVariables.springSky;
			seasonsIconFeedback.sprite = worldVariables.springIcon;
                yearsCount++;
                break;

        }

        txtFood.text =  food.ToString();
    }

    public static void GrowCrop()
    {
        foreach (var item in instance.plants)
        {
            item.Grow();
        }
    }

    public static void AddFood()
    {
        instance.food++;
        instance.txtFood.text = instance.food.ToString();
    }

    public static KidBehaviour SpawnKid(Vector3 pos)
    {
        return instance.kidSpawner.SendKidTo(pos);
    }

    public static void AddKid(int quantitie)
    {
        if(quantitie > 0 || instance.childQuantitie > 1)
            instance.AddChild(quantitie);
    }

    void AddChild(int quantitie)
    {
        childAvailable += quantitie;
        childQuantitie += quantitie;
        txtChildren.text = childAvailable + "/" + childQuantitie;
    }

    public static void ShowAdults(int amount)
    {
        foreach (var item in instance.adults)
        {
          
            item.SetActive(amount > 0);
            amount--;
        }
    }

    public static void ShowText(string text)
    {
        instance.Speech.text = text;
        instance.Speech.transform.parent.gameObject.SetActive(true);
        instance.Invoke("HideBubble", 3);
    }

    void HideBubble()
    {
        instance.Speech.transform.parent.gameObject.SetActive(false);
    }
}

public enum seasons
{
    spring, summer, autumn, winter
}