using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class selectServiceCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    Image image; // 현재 선택한 서비스 카드
    //int[] numbers; // 5장 덱들의 실제 인덱스
    RectTransform pos; // 5장 덱들의 위치
    Vector2 origin; // 덱들의 원래 위치

    Image original; // 원래 강화카드
    GameObject select; // 선택한 강화카드(덮어씌운 구조)

    RectTransform selectPos; // 선택한 강화카드 위치
    Vector2 selectOrigin; // 선택한 강화카드 원래 위치

    public GameObject show; // 뒷 배경
    public moveUI window; // 상점 화면

    int index;
    bool status;

    public Button button; // 5장 버튼
    public RectTransform[] posList; // 5장 덱들의 좌표

    string cashPriceText; // 가격을 가져오기

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
                init(i);
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

    void init(int i)
    {
        selectServiceCard c = posList[i].GetComponent<selectServiceCard>();
        c.setStatus(true);
        c.setButton(false);
        posList[i].sizeDelta = origin;
    }

    void Enable(bool flag)
    {
        for (int i = 0; i < 5; i++)
        {
            posList[i].GetComponent<selectServiceCard>().enabled = flag;
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

    public void selectCard(Image original, GameObject obj, Vector2 selectOrigin, TextMeshProUGUI cashText)
    {
        this.original = original;
        Enable(true);
        select = obj;
        selectPos = select.GetComponent<RectTransform>();
        this.selectOrigin = selectOrigin; // 원래 위치(이동한 위치가 아니라 원래 UI 위에 위치)

        cashPriceText = cashText.text;

        //foreach (int i in numbers) Debug.Log(i);
    }

    //public void setNumbers(int[] numbers)
    //{
    //    this.numbers = numbers;
    //}

    void changeCard()
    {
        if(buy())
        {
            posList[index].sizeDelta = origin;
            StartCoroutine(changeAnimation(selectPos.anchoredPosition, new Vector2(-150f + (index * 86f), -296f), 0.2f));
            button.gameObject.SetActive(false);
        }
    }

    bool buy()
    {
        int cash = int.Parse(TextManager.instance.cash.text);
        int price = int.Parse(cashPriceText[1..]);
        if(cash >= price)
        {
            TextManager.instance.cash.text = (cash - price).ToString();
            return true;
        }
        return false;
    }

    IEnumerator changeAnimation(Vector2 start, Vector2 target, float endTime)
    {
        float startTime = 0f;
        while (startTime <= endTime)
        {
            selectPos.anchoredPosition = Vector3.Lerp(start, target, startTime / endTime);
            startTime += Time.deltaTime;
            yield return null;
        }

        selectPos.anchoredPosition = target; // 미세한 오차를 제거
        image.sprite = select.GetComponent<Image>().sprite;
        //GameManager.instance.deck[numbers[index]].GetComponent<SpriteRenderer>().sprite = image.sprite;
        GameManager.instance.serviceDeck[index] = image; // 사용할 서비스 카드로 등록

        select.SetActive(false); // 선택한 강화카드 없앰

        //yield return StartCoroutine(transform.parent.GetComponent<changeDeckList>().collect(30f, -100f, 0.3f));
        yield return new WaitForSecondsRealtime(0.35f);
        selectPos.anchoredPosition = selectOrigin; // 다음 번에 다시 사용하기 위해서 원래 위치로 되돌려놓는다.

        show.SetActive(false); // show 오브젝트 종료

        StartCoroutine(window.moveUp(190));
        Enable(false);
    }


    public void cancel()
    {
        for (int i = 0; i < 5; i++)
        {
            init(i);
        }
        select.SetActive(false);

        show.SetActive(false); // show 오브젝트 종료
        selectPos.anchoredPosition = selectOrigin;
        StartCoroutine(window.moveUp(190)); // show 오브젝트 종료
        Enable(false);
        original.enabled = true; // 원래 UI에 있는 강화카드 켜기
    }

}