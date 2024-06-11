using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class changeMyCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    Image image; // ���� ������ ���� ī��
    int[] numbers; // 5�� ������ ���� �ε���
    RectTransform pos; // 5�� ������ ��ġ
    Vector2 origin; // ������ ���� ��ġ

    Image original; // ���� ��ȭī��
    GameObject select; // ������ ��ȭī��(����� ����)
    
    RectTransform selectPos; // ������ ��ȭī�� ��ġ
    Vector2 selectOrigin; // ������ ��ȭī�� ���� ��ġ

    public GameObject show;

    int index;
    bool status;

    public Button button; // 5�� ��ư
    public RectTransform[] posList; // 5�� ������ ��ǥ

    string cashPriceText; // ������ ��������

    void OnEnable()
    {
        button.onClick.RemoveAllListeners();
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
        AudioManager.instance.click();
        setButton(true);
        for (int i = 0; i < 5; i++)
        {
            if (i != index)
            {
                init(i);
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

    void init(int i)
    {
        changeMyCard c = posList[i].GetComponent<changeMyCard>();
        c.setStatus(true);
        c.setButton(false);
        posList[i].sizeDelta = origin;
    }

    void Enable(bool flag)
    {
        for (int i = 0; i < 5; i++)
        {
            posList[i].GetComponent<changeMyCard>().enabled = flag;
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

    public void selectCard(Image original, GameObject obj, TextMeshProUGUI cashText)
    {
        this.original = original;
        Enable(true);
        select = obj;
        selectPos = select.GetComponent<RectTransform>();
        selectOrigin = selectPos.anchoredPosition;

        cashPriceText = cashText.text;
        //foreach (int i in numbers) Debug.Log(i);
    }

    public void setNumbers(int[] numbers)
    {
        this.numbers = numbers;
    }

    void changeCard()
    {
        if(buy())
        {
            AudioManager.instance.buy();
            posList[index].sizeDelta = origin;
            StartCoroutine(changeAnimation(selectPos.anchoredPosition, new Vector2(-32f + (index * 106f), -329f), 0.2f));
            button.gameObject.SetActive(false);
        }
    }

    bool buy()
    {
        int cash = int.Parse(TextManager.instance.cash.text);
        int price = int.Parse(cashPriceText[1..]) ;
        if (cash >= price)
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

        selectPos.anchoredPosition = target; // �̼��� ������ ����
        image.sprite = select.GetComponent<Image>().sprite;
        GameManager.instance.deck[numbers[index]].GetComponent<SpriteRenderer>().sprite = select.GetComponent<Image>().sprite;

        select.SetActive(false); // ������ ��ȭī�� ����

        //yield return StartCoroutine(transform.parent.GetComponent<changeDeckList>().collect(30f, -100f, 0.3f));
        yield return new WaitForSecondsRealtime(0.35f);
        selectPos.anchoredPosition = selectOrigin; // ���� ���� �ٽ� ����ϱ� ���ؼ� ���� ��ġ�� �ǵ������´�.
        show.SetActive(false); // show ������Ʈ ����
        show.gameObject.transform.parent.GetChild(3).gameObject.SetActive(false); // ��� ��ư
        Enable(false);
    }


    public void cancel()
    {
        for(int i = 0; i < 5; i++)
        {
            init(i);
        }
        select.SetActive(false);
        show.gameObject.transform.parent.GetChild(3).gameObject.SetActive(false);
        show.SetActive(false);
        Enable(false);

        original.enabled = true; // ���� UI�� �ִ� ��ȭī�� �ѱ�
    }

}