using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public static TextManager instance = null;

    public TextMeshProUGUI level;
    public TextMeshProUGUI goal;
    public TextMeshProUGUI score;
    public TextMeshProUGUI handName;
    public TextMeshProUGUI chip;
    public TextMeshProUGUI multiple;
    public TextMeshProUGUI handCount;
    public TextMeshProUGUI discardCount;
    public TextMeshProUGUI cash;

    string[] jokbo = { "»ïÆÈ±¤¶¯", "±¤¶¯", "¶¯", "¾Ë¸®", "µ¶»ç", "±¸»æ", "Àå»æ", "Àå»ç", "¼¼·ú", "²ý" };
    public TextMeshProUGUI[] jokbostack;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }
    void Start()
    {
        goal.text = "300";

        score.text = "0";
        handName.text = "";
        chip.text = "0";
        multiple.text = "0";
        handCount.text = "3";
        discardCount.text = "4";

        cash.text = "0";
        level.text = $"·¹º§{GameManager.wave}";
    }

    public void upStack(string jokboName)
    {
        int index = Array.IndexOf(jokbo, jokboName);
        jokbostack[index].text = (int.Parse(jokbostack[index].text) + 1).ToString();
    }

    public bool stackCheck()
    {
        foreach(TextMeshProUGUI count in jokbostack)
        {
            if (int.Parse(count.text) < 2)
                return false;
        }
        return true;
    }

    public void setJokboCount(string jokboName, int count)
    {
        int index = Array.IndexOf(jokbo, jokboName);
        jokbostack[index].text = (count).ToString();
    }

    public void setAllJokbo(int count)
    {
        for(int i = 0; i < jokbo.Length; i++)
        {
            jokbostack[i].text = (count).ToString();
        }
    }

    public void test()
    {
        goal.text = "10";
        cash.text = "500";
        handCount.text = "30";
        discardCount.text = "40";
        GameManager.discardStack = 3;
        foreach(var jk in jokbostack)
        {
            jk.text = "5";
        }
    }
}
