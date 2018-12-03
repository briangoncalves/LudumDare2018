using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantBehaviour : MonoBehaviour 
{

    public SpriteRenderer img;
    plantState state = plantState.empty;
    PlantVariables variables;
    bool canClick = true;
    bool needWater;
    float timer;
    KidBehaviour kid;

	void Start () 
    {
        variables = GameManager.instance.plantVariables;

    }

	void Update () 
    {
        if(state == plantState.planted)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                if(needWater)
                {
                    state = plantState.dead;
                    img.sprite = variables.dead;
                }
                else
                {
                    img.sprite = variables.needWater;
                    needWater = true;
                    timer = variables.deathTime;
                }
                canClick = true;
            }

        }
	}



    public void Clicked()
    {
        if (!canClick)
            return;

        if (!GameManager.UseChild())
            return;

        kid = GameManager.SpawnKid(transform.position);

        canClick = false;

        switch (state)
        {
            case plantState.empty:
                img.sprite = variables.empty;
                Invoke("Prepare", variables.preparingTime);
                break;

            case plantState.prepared:
                img.sprite = variables.planting;
                Invoke("Plant", variables.plantingTime);
                break;

            case plantState.dead:
                img.sprite = variables.cleaning;
                Invoke("Clear", variables.clearTime);
                break;


            case plantState.planted:
                img.sprite = variables.watering;
                Invoke("Water", variables.wateringTime);
                break;

            case plantState.ready:

                if(needWater)
                {
                    img.sprite = variables.watering;
                    Invoke("Water", variables.wateringTime);
                }
                else
                {
                    img.sprite = variables.croping;
                    Invoke("Crop", variables.cropTime);

                }
         
                break;
        }
    }

    void Prepare()
    {
        state = plantState.prepared;
        img.sprite = variables.prepared;
        canClick = true;
        GameManager.ReturnChild(kid);
    }

    void Plant()
    {
        timer = variables.waterNeedInterval;
        needWater = false;
        state = plantState.planted;
        img.sprite = variables.planted;
        GameManager.ReturnChild(kid);

    }

    void Water()
    {
		if (state == plantState.ready) {
			Crop ();
		} else {
			timer = variables.waterNeedInterval;
			needWater = false;
			img.sprite = variables.planted;
			GameManager.ReturnChild (kid);
		}
    }

    void Clear()
    {
        state = plantState.empty;
        img.sprite = variables.empty;
        canClick = true;
        GameManager.ReturnChild(kid);
    }

    void Crop()
    {
        GameManager.AddFood();
        state = plantState.empty;
        img.sprite = variables.empty;
        canClick = true;
        GameManager.ReturnChild(kid);

    }


    public void Grow()
    {
        if(state == plantState.planted)
        {
            img.sprite = variables.ready;
            state = plantState.ready;
            canClick = true;
        }
    }
}

public enum plantState
{
    empty, prepared, planted, ready, dead
}