using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using TMPro;

public class consumeCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    Image image; // 원래 UI에 있는 강화카드
    RectTransform pos; // UI 강화카드 위치
    Vector2 origin;

    public GameObject cash; // hover cash
    TextMeshProUGUI cashPrice; // 가격
    public GameObject show; // 선택했을 때 교환 화면을 보여주기 위한 오브젝트
    public Image select; // 선택한 강화 카드
    public Image[] chooseCardInDeck; // 덱에 있는 카드 5장 보여주기
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

        show.SetActive(true); // 뒷 배경
        show.gameObject.transform.parent.GetChild(3).gameObject.SetActive(true); // 버튼
        select.sprite = image.sprite;
        select.gameObject.SetActive(true); // 선택한 강화카드
        image.enabled = false; // 원래 UI에 있는 강화카드 끄기
        foreach (Image img in chooseCardInDeck)
        {
            img.GetComponent<changeMyCard>().selectCard(image, select.gameObject, cashPrice);
        }
    }

}
