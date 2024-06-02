using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class completeManager : MonoBehaviour
{
    public GameObject complete;
    public TextMeshProUGUI goal;
    public TextMeshProUGUI cashText;
    public Button cashButton;

    public void open(string score, int cash)
    {
        complete.SetActive(true);
        goal.text = score;
        cashText.text = "$ " + cash.ToString() + " È¹µæ";

        cashButton.onClick.AddListener(() => getCash(cash));
    }

    public void getCash(int cash)
    {
        TextManager.instance.cash.text = (int.Parse(TextManager.instance.cash.text) + cash).ToString();
    }
}
