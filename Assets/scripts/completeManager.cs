using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class completeManager : MonoBehaviour
{
    public GameObject complete;
    public TextMeshProUGUI stage;

    public TextMeshProUGUI goal;

    public TextMeshProUGUI basicCash;
    public TextMeshProUGUI handCash;
    public TextMeshProUGUI cashText;

    public Button cashButton;

    public void open(string score)
    {
        stage.text = $"stage {GameManager.round + 1}";
        int cash = GameManager.round != 2 ? 6 : 8;
        int handCount = int.Parse(TextManager.instance.handCount.text);
        complete.SetActive(true);
        goal.text = score;

        //allDiscard.text = $"{(4 - int.Parse(TextManager.instance.discardCount.text)) + int.Parse(allDiscard.text)}";

        basicCash.text = $"$ {cash} È¹µæ";
        handCash.text = $"$ {handCount} È¹µæ";
        cashText.text = $"$ {cash + handCount} È¹µæ";

        cashButton.onClick.RemoveAllListeners();
        cashButton.onClick.AddListener(() => getCash(cash, handCount));
    }

    public void getCash(int cash, int handCount)
    {
        TextManager.instance.cash.text = (int.Parse(TextManager.instance.cash.text) + cash + handCount).ToString();
    }
}
