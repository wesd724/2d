using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class info : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Image back;
    public Image infoUI;
    Color color;
    

    void Start()
    {
        back = GetComponent<Image>();
        color = back.color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        back.color = color;
        infoUI.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        back.color = new Color(150 / 255f, 150 / 255f, 150 / 255f);
        infoUI.gameObject.SetActive(true);
    }
}
