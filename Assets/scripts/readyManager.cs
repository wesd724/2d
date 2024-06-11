using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class readyManager : MonoBehaviour
{
    public TextMeshProUGUI rerollPrice;
    public upgradeList upgradeList;
    public serviceList serviceList;
    public changeDeckList changeDeckList;

    public void reroll()
    {
        int cash = int.Parse(TextManager.instance.cash.text);
        int reroll = int.Parse(rerollPrice.text);
        if(cash >= reroll)
        {
            TextManager.instance.cash.text = (cash - reroll).ToString();
            rerollPrice.text = (reroll + 1).ToString();
            StartCoroutine(re());
        }
    }

    IEnumerator re()
    {
        StartCoroutine(changeDeckList.collect(27f, -100f, 0.2f));
        StartCoroutine(upgradeList.collect(50.42f, -160f, 0.2f));
        yield return StartCoroutine(serviceList.collect(53f, -159f, 0.2f));
        yield return new WaitForSecondsRealtime(0.05f);

        yield return StartCoroutine(showCard());
    }

    public IEnumerator show()
    {
        yield return StartCoroutine(showCard());
    }

    IEnumerator showCard()
    {
        yield return StartCoroutine(changeDeckList.expand(-100f, 27f, 0.2f));
        yield return StartCoroutine(upgradeList.expand(-160f, 50.42f, 0.2f));
        yield return StartCoroutine(serviceList.expand(-159f, 53f, 0.2f));
    }
    
    public void showBack()
    {
        rerollPrice.text = "2";
        changeDeckList.back();
        upgradeList.back();
        serviceList.back();
    }

    void Start()
    {
        
    }
}
