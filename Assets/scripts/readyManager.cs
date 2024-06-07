using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readyManager : MonoBehaviour
{
    public upgradeList upgradeList;
    public serviceList serviceList;
    public changeDeckList changeDeckList;

    public void reroll()
    {
        StartCoroutine(re());
    }

    IEnumerator re()
    {
        StartCoroutine(changeDeckList.collect(27f, -100f, 0.3f));
        StartCoroutine(upgradeList.collect(50.42f, -160f, 0.3f));
        yield return StartCoroutine(serviceList.collect(53f, -159f, 0.3f));
        yield return new WaitForSecondsRealtime(0.1f);

        yield return StartCoroutine(changeDeckList.expand(-100f, 27f, 0.3f));
        yield return StartCoroutine(upgradeList.expand(-160f, 50.42f, 0.3f));
        yield return StartCoroutine(serviceList.expand(-159f, 53f, 0.3f));
    }

    public IEnumerator show()
    {
        yield return StartCoroutine(changeDeckList.expand(-100f, 27f, 0.3f));
        yield return StartCoroutine(upgradeList.expand(-160f, 50.42f, 0.3f));
        yield return StartCoroutine(serviceList.expand(-159f, 53f, 0.3f));
    }
    
    public void showBack()
    {
        changeDeckList.back();
        upgradeList.back();
        serviceList.back();
    }

    void Start()
    {
        
    }
}
