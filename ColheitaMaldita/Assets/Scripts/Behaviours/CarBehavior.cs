using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehavior : MonoBehaviour {


	// Use this for initialization
	void Start () {
		var sprite = this.GetComponentInParent<SpriteRenderer>();
		var colorId = Random.Range(0, sprites.Length); 
        sprite.sprite = sprites[colorId];
	}

    public Sprite[] sprites;
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
				GameObject.Find ("god").GetComponent<GodBehaviour> ().AddSoul (AdultsInCar, KidsInCar);
			}
		}

		if (Input.GetMouseButton (0) && canMove) {
			canMove = false;
			isBreaking = true;
			foreach (var audio in this.GetComponents<AudioSource>()) {
				if (audio.isPlaying)
					audio.Stop ();
			}
			this.transform.Find ("CarCrash").GetComponent<AudioSource> ().Play ();
		}
	}

	void OnBecameInvisible() {
		MiniGameManager.instance.NumberOfCars--;
		MiniGameManager.instance.CreateNewCar();
		Destroy(gameObject);
		if (MiniGameManager.instance.NumberOfCars == 0) {
			GameManager.instance.MainGame.SetActive (true);
			GameManager.instance.MiniGame.SetActive (false);
			GameManager.instance.MainCamera.GetComponent<AudioSource> ().Stop ();
			GameManager.instance.MainCamera.GetComponent<AudioSource> ().clip = worldVariables.mainGameMusic;
			GameManager.instance.MainCamera.GetComponent<AudioSource> ().Play ();
			GameObject.Find ("god").GetComponent<GodBehaviour> ().AddSoul (0, 1);
		}
	}
}
