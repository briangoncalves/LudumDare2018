﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidBehaviour : MonoBehaviour {

    Vector3 target;
    bool goToTarget;
    bool goBack;
    float maxSpeed = 6f;


    void Update () 
    {
        if(goBack)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, maxSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.localPosition, Vector3.zero) < 0.1)
            {
                Destroy(gameObject);
            }
        }
        else if(goToTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, maxSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.localPosition, target) < 0.1)
            {
                goToTarget = false;
            }
        }
	}

    public void GoToTarget(Vector3 targetPos)
    {
        target = targetPos;
        goToTarget = true;
    }

    public void GoBack()
    {
        goBack = true;
        goToTarget = false;
    }
}
