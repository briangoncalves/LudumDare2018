using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    bool tutorialComplete;
    List<string> tutorialCompleted = new List<string>();

    private void Start()
    {
        GameManager.ShowText("Se clicar na terra a criança faz o trampo sujo pra vc");
    }

    public void CheckTutorial(string action)
    {
        if (tutorialComplete || tutorialCompleted.Contains(action))
            return;

        tutorialCompleted.Add(action);

        switch (action)
        {
            case "PlantPrepared":
                GameManager.ShowText("Depois para semear");
                break;

            case "PlantPlanted":
                GameManager.ShowText("Agora eu preciso de almas vc consegue na rodovia escolhendo os carros melhores clique em mim");
                break;

            case "NeedWater":
                GameManager.ShowText("Regue as plantas");
                break;

            case "PlantDead":
                GameManager.ShowText("Limpe o terreno se a planta morrer");
                break;

            case "Autumn":
                GameManager.ShowText("Se as crianças não tiverem milho para o inverno elas morrem");
                break;

        }
    }
}
