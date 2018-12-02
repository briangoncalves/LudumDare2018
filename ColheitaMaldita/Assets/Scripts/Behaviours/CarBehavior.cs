using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehavior : MonoBehaviour {


	// Use this for initialization
	void Start () {
		var sprite = this.GetComponentInParent<SpriteRenderer>();
		var rnd = new System.Random();
		var colorId = rnd.Next(0, colors.Length -1); 
		sprite.color = colors[colorId];
	}

	Color[] colors = new Color[] { Color.blue, Color.green, Color.grey, Color.red };

	public float MoveSpeed = 5f;
	public bool canMove = true;
	public MiniGameVariables Variables;
	public int AdultsInCar = 1;
	public int KidsInCar = 0;
	public bool isBreaking = false;
	public WorldVariables worldVariables;

	// Update is called once per frame
	void Update () {		
		if(canMove)
			transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed, Camera.main.transform);

		if (isBreaking) {
			MoveSpeed -= 0.05f;
			transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed, Camera.main.transform);
			if (MoveSpeed <= 0f) {
				isBreaking = false;
				MiniGameManager.instance.NumberOfCars = 0;
				GameManager.instance.MainGame.SetActive (true);
				GameManager.instance.MiniGame.SetActive (false);
				GameManager.instance.MainCamera.GetComponent<AudioSource> ().Stop ();
				GameManager.instance.MainCamera.GetComponent<AudioSource> ().clip = worldVariables.mainGameMusic;
				GameManager.instance.MainCamera.GetComponent<AudioSource> ().Play ();
				GameManager.instance.childAvailable += KidsInCar;
				GameManager.instance.childQuantitie += KidsInCar;
				GameObject.Find ("god").GetComponent<GodBehaviour> ().AddSoul (AdultsInCar, KidsInCar);
			}
		}

		if (Input.GetMouseButton (0) && canMove) {
			canMove = false;
			isBreaking = true;
			foreach (var audio in this.GetComponents<AudioSource>()) {
				if (audio.isPlaying)
					audio.Pause ();
			}
			this.transform.Find ("CarCrash").GetComponent<AudioSource> ().Play ();
		}
	}

	void OnBecameInvisible() {
		MiniGameManager.instance.NumberOfCars--;
		MiniGameManager.instance.CreateNewCar();
		Destroy(gameObject);
	}
}
