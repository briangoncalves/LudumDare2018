using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodBehaviour : MonoBehaviour {

    int soulCount;
    int soulNeeded;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        soulNeeded = GameManager.instance.worldVariables.startSoulAmount;
    }

	public WorldVariables worldVariables;

    public void Clicked()
    {
		GameManager.instance.MainGame.SetActive (false);
		GameManager.instance.MiniGame.SetActive (true);
		MiniGameManager.instance.StartVariables ();
		GameManager.instance.MainCamera.GetComponent<AudioSource> ().Stop ();
		GameManager.instance.MainCamera.GetComponent<AudioSource> ().clip = worldVariables.miniGameMusic;
		GameManager.instance.MainCamera.GetComponent<AudioSource> ().Play ();
    }

    public void AddSoul(int amount)
    {
        soulCount += amount;

        if(soulCount >= soulNeeded)
        {
            animator.SetTrigger("play");
            Invoke("Grow", 2);
        }
    }

    void Grow()
    {
        soulCount = soulCount - soulNeeded;
        soulNeeded += GameManager.instance.worldVariables.soulProgression;
        GameManager.GrowCrop();
    }
}
