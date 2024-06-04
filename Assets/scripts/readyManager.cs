using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readyManager : MonoBehaviour
{
    public GameObject[] consumes;

    void OnEnable()
    {
        foreach(GameObject g in consumes)
            g.gameObject.SetActive(true);
    }
    void Start()
    {
        
    }
}
