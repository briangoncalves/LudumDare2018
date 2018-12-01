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

    public Image seasonsFeedback;

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

    public static void ReturnChild()
    {
        instance.childAvailable++;
        instance.childAvailable = instance.childAvailable > instance.childQuantitie ? instance.childQuantitie : instance.childAvailable;
        instance.txtChildren.text = instance.childAvailable + "/" + instance.childQuantitie;
    }


    public void ChangeSeason()
    {
        switch(season)
        {
            case seasons.autumn:
                season = seasons.winter;
                seasonsFeedback.sprite = worldVariables.winter;
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
                break;
            case seasons.summer:
                season = seasons.autumn;
                seasonsFeedback.sprite = worldVariables.autumn;
                break;

            case seasons.winter:
                season = seasons.spring;
                seasonsFeedback.sprite = worldVariables.spring;
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
}

public enum seasons
{
    spring, summer, autumn, winter
}