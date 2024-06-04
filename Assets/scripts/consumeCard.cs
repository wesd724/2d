using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class consumeCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    Image image; // ���� UI�� �ִ� ��ȭī��
    RectTransform pos;
    Vector2 origin;

    public GameObject cash; // hover cash
    public GameObject show; // �������� �� ��ȯ ȭ���� �����ֱ� ���� ������Ʈ
    public Image select; // ������ ��ȭ ī��
    public Image[] chooseCardInDeck; // ���� �ִ� ī�� 5�� �����ֱ�
    int[] numbers;

    void OnEnable()
    {
        //Debug.Log($"Enable: {gameObject.name}");
        numbers = Enumerable.Range(0, 20).OrderBy(x => Random.value).Take(5).ToArray();
        //foreach(var number in numbers) Debug.Log(number);
    }

    void Start()
    {
        //Debug.Log($"Start: {gameObject.name}");
        image = GetComponent<Image>();
        pos = GetComponent<RectTransform>();
        origin = pos.anchoredPosition;
    }

    void setChooseCard()
    {
        for (int i = 0; i < 5; i++)
            chooseCardInDeck[i].sprite =
                GameManager.instance.deck[numbers[i]].GetComponent<SpriteRenderer>().sprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cash.SetActive(false);
        pos.anchoredPosition = origin;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cash.SetActive(true);
        pos.anchoredPosition += Vector2.up * 30.0f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        cash.SetActive(false);
        setChooseCard();

        foreach (var number in numbers) Debug.Log(number);
        show.SetActive(true); // �� ���
        select.sprite = image.sprite;
        select.gameObject.SetActive(true); // ������ ��ȭī��
        gameObject.SetActive(false); // ���� UI�� �ִ� ��ȭī��
        foreach(Image img in chooseCardInDeck)
        {
            img.GetComponent<changeMyCard>().selectCard(numbers, select.gameObject);
        }
    }

}
