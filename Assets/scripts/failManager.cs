using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class failManager : MonoBehaviour
{
    public TextMeshProUGUI currentPoint;
    public TextMeshProUGUI goal;

    void OnEnable()
    {
        currentPoint.text = TextManager.instance.score.text;
        goal.text = TextManager.instance.goal.text;
    }
}
