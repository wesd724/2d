using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class changeMyCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    Image image; // 현재 선택한 덱의 카드
    GameObject select; // 선택한 강화카드
    int[] numbers; // 5장 덱들의 실제 인덱스

    RectTransform pos;
    Vector2 origin;
    int index;
    bool status;

    public Button button; // 5장 버튼
    public RectTransform[] posList; // 5장 덱들의 좌표

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

        if (button.gameObject.activeSelf) // 버튼이 커졌으면
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

        select.SetActive(false); // 선택한 강화카드 없앰
        transform.parent.parent.gameObject.SetActive(false); // show 오브젝트 종료
    }
    
    public void cancel()
    {
        select.SetActive(false);
        transform.parent.parent.gameObject.SetActive(false);
    }
    
}