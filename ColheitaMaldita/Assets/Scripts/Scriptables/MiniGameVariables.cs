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
		var random = new System.Random ();
		return random.Next (MinNumberOfCars, MaxNumberOfCars);
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
			var random = new System.Random ();
			if (random.Next (0, 100) <= p.AdultPercentage)
				seats.Add (CarSeatType.Adult);
			else if (random.Next (0, 100) <= p.KidPercentage)
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