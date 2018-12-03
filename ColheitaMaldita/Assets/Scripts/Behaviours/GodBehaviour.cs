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
        GameManager.ShowText("Se clicar na terra a criança faz o trampo sujo pra vc");
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
        GameManager.AddKid(-1);
    }

    public void AddSoul(int amountAdults,int amountKids)
    {
        soulCount += amountAdults;
        GameManager.AddKid(amountKids);
        GameManager.ShowAdults(amountAdults);
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
