using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null; // 아직은 사용 안함.
    public AudioSource[] audioSources;
    //public AudioSource bgm;
    //public AudioClip[] clips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }

    public void handPlay()
    {
        audioSources[0].Play();
    }
    public void score()
    {
        audioSources[1].Play();

    }
    public void discard()
    {
        audioSources[2].Play();
    }
    public void chip()
    {
        audioSources[3].Play();
    }

    public void draw()
    {
        audioSources[4].Play();
    }

    public void select()
    {
        audioSources[5].Play();
    }

    public void restart()
    {
        audioSources[6].Play();
    }

    public void legend()
    {
        audioSources[7].Play();
    }

    public void buy()
    {
        audioSources[8].Play();
    }

    public void click()
    {
        audioSources[9].Play();
    }

    public void spread()
    {
        audioSources[10].Play();
    }

    public void BGM()
    {
        audioSources[11].Play();
    }
}
