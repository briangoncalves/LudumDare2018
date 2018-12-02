using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusicBehaviour : MonoBehaviour {


    public AudioClip en;
    public AudioClip pt;
    public AudioSource audiosrc;

    void Start () {
        PlayRandom();
    }

    void PlayRandom()
    {
        Invoke("PlayMusic", Random.Range(60, 180));
    }

    void PlayMusic()
    {
        if(XMLLanguageLoader.selectedLanguage == "English")
        {
            audiosrc.PlayOneShot(en);
        }
        else
        {

            audiosrc.PlayOneShot(pt);
        }

        PlayRandom();
    }
}
