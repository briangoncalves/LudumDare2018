using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantBehaviour : MonoBehaviour {

    public Text txtFeedbackTemp;
    plantState state = plantState.empty;
    PlantVariables variables;
    bool canClick = true;
    bool needWater;
    float timer;

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
                    txtFeedbackTemp.text = "dead";
                }
                else
                {
                    txtFeedbackTemp.text = "need water";
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

        canClick = false;

        switch (state)
        {
            case plantState.empty:
                txtFeedbackTemp.text = "preparing";
                Invoke("Prepare", variables.preparingTime);
                break;

            case plantState.prepared:
                txtFeedbackTemp.text = "planting";
                Invoke("Plant", variables.plantingTime);
                break;

            case plantState.dead:
                txtFeedbackTemp.text = "cleaning";
                Invoke("Clear", variables.clearTime);
                break;


            case plantState.planted:
                txtFeedbackTemp.text = "Watering";
                Invoke("Water", variables.wateringTime);
                break;

            case plantState.ready:

                if(needWater)
                {
                    txtFeedbackTemp.text = "Watering";
                    Invoke("Water", variables.wateringTime);
                }
                else
                {
                    txtFeedbackTemp.text = "croping";
                    Invoke("Crop", variables.cropTime);

                }
         
                break;
        }
    }

    void Prepare()
    {
        state = plantState.prepared;
        txtFeedbackTemp.text = "prepared";
        canClick = true;
        GameManager.ReturnChild();
    }

    void Plant()
    {
        timer = variables.waterNeedInterval;
        needWater = false;
        state = plantState.planted;
        txtFeedbackTemp.text = "plant";
        GameManager.ReturnChild();
    }

    void Water()
    {
        timer = variables.waterNeedInterval;
        needWater = false;
        txtFeedbackTemp.text = "plant";
        GameManager.ReturnChild();
    }

    void Clear()
    {
        state = plantState.empty;
        txtFeedbackTemp.text = "empty";
        canClick = true;
        GameManager.ReturnChild();
    }

    void Crop()
    {
        GameManager.AddFood();
        state = plantState.empty;
        txtFeedbackTemp.text = "empty";
        canClick = true;
        GameManager.ReturnChild();

    }


    public void Grow()
    {
        if(state == plantState.planted)
        {
            txtFeedbackTemp.text = "ready";
            state = plantState.ready;
            canClick = true;
        }
    }
}

public enum plantState
{
    empty, prepared, planted, ready, dead
}