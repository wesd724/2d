using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class roundManager : MonoBehaviour
{
    public UiManager uiManager;
    public TextMeshProUGUI level; // 전역에서 쓰는 wave 와 같은 의미
    public Button[] select;
    public TextMeshProUGUI[] goal;
    public test test;

    string[,] waves =  {
                        {"500", "700", "1000"},
                        {"1200", "1600", "2500"},
                        {"3000", "6000", "12000"}
                    };

    void OnEnable()
    {
        //GameManager.wave = 2;
        //GameManager.round = 2;
        int w = GameManager.wave;
        int r = GameManager.round;
        level.text = $"{w + 1}";
        for (int i = 0; i < 3; i++)
        {
            int temp = i;
            goal[i].text = waves[w, i];
            select[i].interactable = false;
            if (i == r)  // 해당 라운드만 선택 클릭가능 하도록 설계
            {
                select[i].interactable = true;
                select[i].onClick.RemoveAllListeners();
                select[i].onClick.AddListener(() => selectGoal(goal[temp].text));
            }
        }
    }

    void selectGoal(string score)
    {
        GameManager.instance.deck = GameManager.instance.deck.OrderBy(card => int.Parse(card.name)).ToList();
        //test.change(); //
        TextManager.instance.goal.text = score;
        TextManager.instance.level.text = $"레벨{GameManager.wave + 1}";
        uiManager.select();
    }

    public void newWave()
    {
        GameManager.wave += 1;
        for (int i = 0; i < 3; i++)
        {
            select[i].interactable = true;
        }
    }
}
