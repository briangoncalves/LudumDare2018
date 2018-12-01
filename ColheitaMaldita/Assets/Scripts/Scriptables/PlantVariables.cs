using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantVariables", menuName = "Farm/PlantVariables", order = 2)]
public class PlantVariables : ScriptableObject
{
    public float preparingTime;
    public float plantingTime;
    public float wateringTime;
    public float cropTime;
    public float deathTime;
    public float waterNeedInterval;
    public float clearTime;

    public Sprite empty;
    public Sprite prepared;
    public Sprite planted;
    public Sprite needWater;
    public Sprite ready;
    public Sprite dead;
    public Sprite preparing;
    public Sprite planting;
    public Sprite cleaning;
    public Sprite watering;
    public Sprite croping;
}
