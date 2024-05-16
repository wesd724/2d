using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    RawImage close;
    Color prevColor;
    moveUI ui;
    void Start()
    {
        close = GetComponent<RawImage>();
        ui = transform.parent.GetComponent<moveUI>();
        prevColor = close.color;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        close.color = prevColor;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        close.color = new Color(close.color.r, close.color.g, close.color.b, close.color.a - 100 / 255f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(ui.moveDown());
    }
}
