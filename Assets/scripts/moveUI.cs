using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveUI : MonoBehaviour
{
    public RectTransform ui;
    public IEnumerator moveUp(int y)
    {
        while (ui.anchoredPosition.y <= y)
        {
            ui.anchoredPosition += Vector2.up * 8;
            yield return null;
        }
    }

    public IEnumerator moveDown(int y, bool flag = false)
    {
        while (ui.anchoredPosition.y >= y)
        {
            ui.anchoredPosition += Vector2.down * 8;
            yield return null;
        }
        transform.parent.gameObject.SetActive(flag);
    }

    public IEnumerator explainDown()
    {
        while (ui.anchoredPosition.y >= 355)
        {
            ui.anchoredPosition += Vector2.down * 8;
            yield return null;
        }
    }
}
