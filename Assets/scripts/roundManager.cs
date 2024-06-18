using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class roundManager : MonoBehaviour
{
    public UiManager uiManager;
    public TextMeshProUGUI level; // �������� ���� wave �� ���� �ǹ�
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
            if (i == r)  // �ش� ���常 ���� Ŭ������ �ϵ��� ����
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
        TextManager.instance.level.text = $"����{GameManager.wave + 1}";
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
