using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class serviceCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    Image image; // ���� UI�� �ִ� ����ī��
    RectTransform pos; // UI ����ī�� ��ġ
    Vector2 origin;

    public GameObject cash; // hover cash

    void Start()
    {
        image = GetComponent<Image>();
        pos = GetComponent<RectTransform>();
        origin = pos.sizeDelta;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cash.SetActive(false);
        pos.sizeDelta = origin;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cash.SetActive(true);
        pos.sizeDelta = new Vector2(origin.x * 1.05f, origin.y * 1.05f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pos.sizeDelta = origin;
        cash.SetActive(false);
    }
}
