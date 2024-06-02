using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioSources;
    
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
}
