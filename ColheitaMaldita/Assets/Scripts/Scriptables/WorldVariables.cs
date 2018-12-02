using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorldVariables", menuName = "Farm/WorldVariables", order = 1)]
public class WorldVariables : ScriptableObject
{
    public float SeasonTime;
    public int soulProgression;
    public int startSoulAmount;
    public int startChildren;

    public Sprite autumn;
    public Sprite winter;
    public Sprite spring;
    public Sprite summer;

	public Sprite autumnSky;
	public Sprite winterSky;
	public Sprite springSky;
	public Sprite summerSky;
}
