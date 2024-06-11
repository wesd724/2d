using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using TMPro;

public class serviceCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    Image image; // ���� UI�� �ִ� ����ī��
    RectTransform pos; // UI ����ī�� ��ġ
    Vector2 origin;

    public GameObject cash; // hover cash
    TextMeshProUGUI cashPrice; // ����
    public GameObject show; // �������� �� ��ȯ ȭ���� �����ֱ� ���� ������Ʈ
    public Image select; // ������ ��ȭ ī��
    public Image[] chooseCardInDeck; // ���� �ִ� ī�� 5�� �����ֱ�

    public moveUI window; // ���� ȭ��

    RectTransform selectPos; // ������ ����ī�� ��ġ
    Vector2 selectOrigin; // ������ ����ī�� ���� ��ġ

    void Start()
    {
        image = GetComponent<Image>();
        pos = GetComponent<RectTransform>();
        origin = pos.sizeDelta;
        selectPos = select.GetComponent<RectTransform>();
        selectOrigin = selectPos.anchoredPosition;
        cashPrice = cash.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
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
        AudioManager.instance.click();
        StartCoroutine(window.moveDown(-915, true));
        pos.sizeDelta = origin;
        cash.SetActive(false);

        show.SetActive(true); // �� ���
        select.sprite = image.sprite;
        select.gameObject.SetActive(true); // ������ ��ȭī��
        StartCoroutine(moveAnimation(selectPos.anchoredPosition, new Vector2(selectPos.anchoredPosition.x, -500f), 0.2f));
        image.enabled = false; // ���� UI�� �ִ� ��ȭī�� ����
        foreach (Image img in chooseCardInDeck)
        {
            img.GetComponent<selectServiceCard>().selectCard(image, select.gameObject, selectOrigin, cashPrice);
        }
    }

    IEnumerator moveAnimation(Vector2 start, Vector2 target, float endTime)
    {
        float startTime = 0f;
        while (startTime <= endTime)
        {
            selectPos.anchoredPosition = Vector3.Lerp(start, target, startTime / endTime);
            startTime += Time.deltaTime;
            yield return null;
        }
        selectPos.anchoredPosition = target;
    }
}
