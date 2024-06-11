using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using TMPro;

public class consumeCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    Image image; // ���� UI�� �ִ� ��ȭī��
    RectTransform pos; // UI ��ȭī�� ��ġ
    Vector2 origin;

    public GameObject cash; // hover cash
    TextMeshProUGUI cashPrice; // ����
    public GameObject show; // �������� �� ��ȯ ȭ���� �����ֱ� ���� ������Ʈ
    public Image select; // ������ ��ȭ ī��
    public Image[] chooseCardInDeck; // ���� �ִ� ī�� 5�� �����ֱ�
    //int[] numbers;

    void OnEnable()
    {
        //numbers = Enumerable.Range(0, 20).OrderBy(x => Random.value).Take(5).ToArray();
    }

    void Start()
    {
        image = GetComponent<Image>();
        pos = GetComponent<RectTransform>();
        origin = pos.sizeDelta;
        cashPrice = cash.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    //void setChooseCard()
    //{
    //    for (int i = 0; i < 5; i++)
    //        chooseCardInDeck[i].sprite =
    //            GameManager.instance.deck[numbers[i]].GetComponent<SpriteRenderer>().sprite;
    //}

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
        pos.sizeDelta = origin;
        cash.SetActive(false);
        //setChooseCard();

        show.SetActive(true); // �� ���
        show.gameObject.transform.parent.GetChild(3).gameObject.SetActive(true); // ��ư
        select.sprite = image.sprite;
        select.gameObject.SetActive(true); // ������ ��ȭī��
        image.enabled = false; // ���� UI�� �ִ� ��ȭī�� ����
        foreach (Image img in chooseCardInDeck)
        {
            img.GetComponent<changeMyCard>().selectCard(image, select.gameObject, cashPrice);
        }
    }

}
