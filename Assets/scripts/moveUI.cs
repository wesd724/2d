using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveUI : MonoBehaviour
{
    public RectTransform ui;
    public IEnumerator moveUp(int y, float endTime = 0.25f)
    {
        float startTime = 0f;
        Vector2 start = ui.anchoredPosition;
        Vector2 target = new Vector2(ui.anchoredPosition.x, y);
        while (startTime <= endTime)
        {
            ui.anchoredPosition = Vector3.Lerp(start, target, startTime / endTime);
            startTime += Time.deltaTime;
            yield return null;
        }
        //while (ui.anchoredPosition.y <= y)
        //{
        //    ui.anchoredPosition += Vector2.up * 3500 * Time.deltaTime;
        //    yield return null;
        //}
        ui.anchoredPosition = target;
    }

    public IEnumerator moveDown(int y, bool flag = false, float endTime = 0.25f)
    {
        float startTime = 0f;
        Vector2 start = ui.anchoredPosition;
        Vector2 target = new Vector2(ui.anchoredPosition.x, y);
        while (startTime <= endTime)
        {
            ui.anchoredPosition = Vector3.Lerp(start, target, startTime / endTime);
            startTime += Time.deltaTime;
            yield return null;
        }
        //while (ui.anchoredPosition.y >= y)
        //{
        //    ui.anchoredPosition += Vector2.down * 3500 * Time.deltaTime;
        //    yield return null;
        //}
        transform.parent.gameObject.SetActive(flag);
        ui.anchoredPosition = target;
    }

    public IEnumerator explainDown(int y = 350, float endTime = 0.25f)
    {
        float startTime = 0f;
        Vector2 start = ui.anchoredPosition;
        Vector2 target = new Vector2(ui.anchoredPosition.x, y);
        while (startTime <= endTime)
        {
            ui.anchoredPosition = Vector3.Lerp(start, target, startTime / endTime);
            startTime += Time.deltaTime;
            yield return null;
        }
        //while (ui.anchoredPosition.y >= y)
        //{
        //    ui.anchoredPosition += Vector2.down * 3500 * Time.deltaTime;
        //    yield return null;
        //}
        ui.anchoredPosition = target;
    }
}
