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
        stage.text = $"stage {GameManager.round + 1}"; // 마지막 라운드 처리 버그
        int cash = GameManager.round != 2 ? 6 : 8; // 마지막 라운드 처리 버그
        // GameManager의 checkRound() 먼저 실행해서 일어난 버그이다
        int handCount = int.Parse(TextManager.instance.handCount.text);
        complete.SetActive(true);
        goal.text = score;

        //allDiscard.text = $"{(4 - int.Parse(TextManager.instance.discardCount.text)) + int.Parse(allDiscard.text)}";

        basicCash.text = $"$ {cash} 획득";
        handCash.text = $"$ {handCount} 획득";
        cashText.text = $"$ {cash + handCount} 획득";

        cashButton.onClick.RemoveAllListeners();
        cashButton.onClick.AddListener(() => getCash(cash, handCount));
    }

    public void getCash(int cash, int handCount)
    {
        TextManager.instance.cash.text = (int.Parse(TextManager.instance.cash.text) + cash + handCount).ToString();
    }
}
