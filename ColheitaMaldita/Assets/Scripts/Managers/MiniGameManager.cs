using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour {

	public void StartVariables()
	{
		instance.NumberOfCars = instance.Variables.GetCars ();
		instance.CarNumber = 0;
		CreateNewCar ();
	}

	// Use this for initialization
	void Start () {
	//StartVariables ();
	}

	public static MiniGameManager instance;

	void SetCarAudio(GameObject go)
	{
		var audios = go.GetComponents<AudioSource> ();
		var carIndex = Random.Range(0, audios.Length);
		audios [carIndex].Play();
	}

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
        var carname = "Family_" + cb.AdultsInCar + "a_" + cb.KidsInCar + "c";
        print(carname);
        for (int i = 0; i < go.transform.childCount; i++)
        {
            if(go.transform.GetChild(i).name == carname)
            {
                go.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                break;
            }
        }
    }

	public void CreateNewCar()
	{
		if (instance.NumberOfCars > 0) {
			GameObject go = Instantiate(Car) as GameObject;
			go.transform.parent = GameManager.instance.MiniGame.transform;
			var v = new Vector3 (11, 0.3f, 0);
			var q = new Quaternion (0, 0, 0, 0);
			go.transform.SetPositionAndRotation (v, q);
			SetCarAudio (go);
			SetCarSeats (go);
			instance.CarNumber += 1;

		}
	}
}
