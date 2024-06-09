using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public static TextManager instance = null;

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
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }
    void Start()
    {
        goal.text = "10";
        score.text = "0";
        handName.text = "";
        chip.text = "0";
        multiple.text = "0";
        handCount.text = "3";
        discardCount.text = "4";
        cash.text = "50";
    }

    public void upStack(string jokboName)
    {
        int index = Array.IndexOf(jokbo, jokboName);
        jokbostack[index].text = (int.Parse(jokbostack[index].text) + 1).ToString();
    }
}
