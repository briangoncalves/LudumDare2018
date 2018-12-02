using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour {

    bool starting = true;
    Image filledTransition;
    // Use this for initialization
    void Start () {
        filledTransition = GetComponent<Image>();
	}
	
	void Update () {
        if (starting)
        {
            filledTransition.fillAmount -= Time.deltaTime * 2;
            filledTransition.fillClockwise = true;
            if (filledTransition.fillAmount <= 0)
            {
                starting = false;
            }
        }
    }
}
