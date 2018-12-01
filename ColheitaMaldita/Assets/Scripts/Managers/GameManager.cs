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
    }


    public Text txtTemp;
    public Text txtTemp2;
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



    public static bool UseChild()
    {
        if(instance.childAvailable == 0)
        {
            return false;
        }

        instance.childAvailable--;
        instance.txtTemp.text = "Children iddle:" + instance.childAvailable + " Total Children: " + instance.childQuantitie;
        return true;
    }

    public static void ReturnChild()
    {
        instance.childAvailable++;
        instance.childAvailable = instance.childAvailable > instance.childQuantitie ? instance.childQuantitie : instance.childAvailable;
        instance.txtTemp.text = "Children iddle:" + instance.childAvailable + " Total Children: " + instance.childQuantitie;
    }


    public void ChangeSeason()
    {
        switch(season)
        {
            case seasons.autumn:
                season = seasons.winter;
                if(instance.food >= instance.childQuantitie)
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
                        print("Game over");
                        Time.timeScale = 0;
                    }
                }

                instance.txtTemp.text = "Children iddle:" + instance.childAvailable + " Total Children: " + instance.childQuantitie;
                break;

            case seasons.spring:
                season = seasons.summer;
                break;
            case seasons.summer:
                season = seasons.autumn;
                break;

            case seasons.winter:
                season = seasons.spring;
                yearsCount++;
                break;

        }

        txtTemp2.text = "Current season: " + season.ToString() + " years: " + yearsCount + " food: "+ food;
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
        instance.txtTemp2.text = "Current season: " + instance.season.ToString() + " years: " + instance.yearsCount + " food: " + instance.food;
    }
}

public enum seasons
{
    spring, summer, autumn, winter
}