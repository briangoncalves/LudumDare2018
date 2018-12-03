using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "MiniGameVariables", menuName = "MiniGame/CarVariables", order = 3)]
public class MiniGameVariables : ScriptableObject
{
	public int MinNumberOfCars;
	public int MaxNumberOfCars;

	public CarSeat[] CarSeats;

	public int GetCars()
	{		
		var numberofCars = UnityEngine.Random.Range (MinNumberOfCars, MaxNumberOfCars+1);
		return numberofCars;
	}
}

[Serializable]
public class CarSeat
{
	public CarSeatPercentage[] CarSeatPercentage;

	public CarSeatType[] GetSeatRandom()
	{
		List<CarSeatType> seats = new List<CarSeatType> ();
		foreach (var p in CarSeatPercentage) {
			if (UnityEngine.Random.Range(0, 101) <= p.AdultPercentage)
				seats.Add (CarSeatType.Adult);
			else if (UnityEngine.Random.Range (0, 101) <= p.KidPercentage)
				seats.Add (CarSeatType.Kid);
			else
				seats.Add (CarSeatType.None);
		}
		return seats.ToArray ();
	}
}

public enum CarSeatType
{
	Kid,
	Adult,
	None
}

[Serializable]
public class CarSeatPercentage
{
	public int KidPercentage;
	public int AdultPercentage;
}