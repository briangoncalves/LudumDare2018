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
}
