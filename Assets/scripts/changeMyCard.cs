using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class changeMyCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    Image image; // ���� ������ ���� ī��
    GameObject select; // ������ ��ȭī��
    int[] numbers; // 5�� ������ ���� �ε���

    RectTransform pos;
    Vector2 origin;
    int index;
    bool status;

    public Button button; // 5�� ��ư
    public RectTransform[] posList; // 5�� ������ ��ǥ

    void OnEnable()
    {
        button.onClick.AddListener(() => changeCard());
        status = true;
    }

    void Start()
    {
        image = GetComponent<Image>();
        pos = GetComponent<RectTransform>();
        origin = pos.sizeDelta;
        index = int.Parse(this.name) - 1;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (status)
            pos.sizeDelta = origin;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (status)
            pos.sizeDelta = new Vector2(origin.x * 1.05f, origin.y * 1.05f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        setButton(true);
        for (int i = 0; i < 5; i++)
        {
            if (i != index)
            {
                changeMyCard c = posList[i].GetComponent<changeMyCard>();
                c.setStatus(true);
                c.setButton(false);
                posList[i].sizeDelta = origin;
            }
        }

        if (button.gameObject.activeSelf) // ��ư�� Ŀ������
        {
            status = false;
        }
        else
        {
            status = true;
        }
    }

    void setStatus(bool status)
    {
        this.status = status;
    }

    void setButton(bool status)
    {
        button.gameObject.SetActive(status);
    }

    public void selectCard(int[] numbers, GameObject obj)
    {
        this.numbers = numbers;
        select = obj;

        //foreach (int i in numbers) Debug.Log(i);
    }

    void changeCard()
    {
        posList[index].sizeDelta = origin;
        image.sprite = select.GetComponent<Image>().sprite;
        Debug.Log(numbers[index]);
        Debug.Log(GameManager.instance.deck[numbers[index]].name);
        GameManager.instance.deck[numbers[index]].GetComponent<SpriteRenderer>().sprite = image.sprite;
        button.gameObject.SetActive(false);

        select.SetActive(false); // ������ ��ȭī�� ����
        transform.parent.parent.gameObject.SetActive(false); // show ������Ʈ ����
    }
    
    public void cancel()
    {
        select.SetActive(false);
        transform.parent.parent.gameObject.SetActive(false);
    }
    
}