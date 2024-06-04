using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeDeckList : MonoBehaviour
{
    public Button[] buttons;

    void Start()
    {

    }

    public bool clickCard(int index)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == index)
            {
                buttons[i].gameObject.SetActive(!buttons[i].gameObject.activeSelf);
            }   
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }

        if(buttons[index].gameObject.activeSelf) // 버튼이 커졌으면
        {
            return false;
        }
        return true;
    }
}
