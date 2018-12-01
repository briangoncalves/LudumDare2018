using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		instance.NumberOfCars = instance.Variables.GetCars ();
		instance.CarNumber = 1;
		SetCarSeats (GameObject.Find("Car"));
	}

	public static MiniGameManager instance;

	void Awake()
	{
		instance = this;
	}

	public int NumberOfCars;
	public int CarNumber;
	public Text txtNumberOfCars;
	public Text txtPeopleInCar;
	public GameObject Car;
	public MiniGameVariables Variables;

	// Update is called once per frame
	void Update () {
		instance.txtNumberOfCars.text = instance.NumberOfCars + " cars remaining";
	}

	void SetCarSeats(GameObject go)
	{
		var cb = go.GetComponent<CarBehavior>();
		cb.AdultsInCar = 0;
		cb.KidsInCar = 0;
		CarSeatType[] carSeats = instance.Variables.CarSeats [instance.CarNumber].GetSeatRandom();
		foreach (CarSeatType seat in carSeats) {
			if (seat == CarSeatType.Adult)
				cb.AdultsInCar += 1;
			else if (seat == CarSeatType.Kid)
				cb.KidsInCar += 1;
		}
		instance.txtPeopleInCar.text = cb.AdultsInCar + " Adults in car and " + cb.KidsInCar + " Kids in car";
	}

	public void CreateNewCar()
	{
		if (instance.NumberOfCars > 0) {
			GameObject go = Instantiate(Car) as GameObject;
			var v = new Vector3 (11, -0.7f, 0);
			var q = new Quaternion (0, 0, 0, 0);
			go.transform.SetPositionAndRotation (v, q);
			instance.CarNumber = 2;
			SetCarSeats (go);
		}
	}
}
