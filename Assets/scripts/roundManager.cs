using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class roundManager : MonoBehaviour
{
    public UiManager uiManager;
    public Button select1, select2, select3;
    public TextMeshProUGUI goal1, goal2, goal3;

    void Start()
    {
        select1.onClick.AddListener(() => selectGoal(goal1.text));
        select2.onClick.AddListener(() => selectGoal(goal2.text));
        select3.onClick.AddListener(() => selectGoal(goal3.text));
    }

    // Update is called once per frame

    void selectGoal(string score)
    {
        TextManager.instance.goal.text = score;
        uiManager.select();
        StartCoroutine(GameManager.instance.startGame());
    }
}
