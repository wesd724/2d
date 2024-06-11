using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using TMPro;

public class serviceCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    Image image; // 원래 UI에 있는 서비스카드
    RectTransform pos; // UI 서비스카드 위치
    Vector2 origin;

    public GameObject cash; // hover cash
    TextMeshProUGUI cashPrice; // 가격
    public GameObject show; // 선택했을 때 교환 화면을 보여주기 위한 오브젝트
    public Image select; // 선택한 강화 카드
    public Image[] chooseCardInDeck; // 덱에 있는 카드 5장 보여주기

    public moveUI window; // 상점 화면

    RectTransform selectPos; // 선택한 서비스카드 위치
    Vector2 selectOrigin; // 선택한 서비스카드 원래 위치

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

        show.SetActive(true); // 뒷 배경
        select.sprite = image.sprite;
        select.gameObject.SetActive(true); // 선택한 강화카드
        StartCoroutine(moveAnimation(selectPos.anchoredPosition, new Vector2(selectPos.anchoredPosition.x, -500f), 0.2f));
        image.enabled = false; // 원래 UI에 있는 강화카드 끄기
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
