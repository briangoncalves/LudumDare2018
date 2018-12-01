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
	// Update is called once per frame
	void Update () {		
		if(canMove)
			transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed, Camera.main.transform);

		if (Input.GetMouseButton (0)) {
			canMove = false;
		}
	}

	void OnBecameInvisible() {
		MiniGameManager minigame = GameObject.FindObjectOfType<MiniGameManager>();
		minigame.NumberOfCars--;
		minigame.CreateNewCar();
		Destroy(gameObject);
	}
}
