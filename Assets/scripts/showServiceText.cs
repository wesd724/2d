using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class showServiceText : MonoBehaviour
{
    GameObject textUI;
    TextMeshProUGUI tmp;

    void Start()
    {
        textUI = transform.GetChild(1).gameObject;
        tmp = textUI.GetComponent<TextMeshProUGUI>();
    }

    public bool setText(etcCardEffect effect)
    {
        if (effect.Chip != 0)
        {
            tmp.text = $"+{effect.Chip} 칩";
            return true;
        }

        if (effect.Multiple != 0)
        {
            tmp.text = $"+{effect.Multiple} 배수";
            return true;
        }

        tmp.text = "";
        return false;
    }

    public bool setText2(etcCardEffect effect)
    {
        if (effect.Multiple != 0)
        {
            tmp.text = $"x{effect.Multiple} 배수";
            return true;
        }

        tmp.text = "";
        return false;
    }

    public void close()
    {
        textUI.SetActive(false);
    }

    public void show()
    {
        textUI.SetActive(true);
    }
}
