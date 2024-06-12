using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class service : MonoBehaviour
{
    public Image[] images;
    public TextMeshProUGUI[] texts;
    public GameObject serviceCardList; // 서비스 카드 목록을 가진 부모 오브젝트
                                       // 이 변수에서 랜덤으로 3개를 뽑아 img1, 2, 3의 sprite 적용한다.
    public GameObject alreadyPickList;
    public Image cardback;

    void OnEnable()
    {
        List<Transform> childs = new List<Transform>();
        foreach (Transform child in alreadyPickList.transform)
        {
            childs.Add(child);
        }
        foreach(Transform child in childs)
        {
            child.SetParent(null);
            child.SetParent(serviceCardList.transform);  // 이전에 제거된 서비스 카드 목록들 복구
        }
        childs.Clear();

        availableServiceCard();
    }

    public void setting()
    {
        //전설급이 나올 확률은 매우 적음 => 애초부터 전설 3장임
        //기본카드가 70 % 금색을 제외한 강화카드는 20 % 금색카드는 10 % 정도
        List<Transform> availableCards = new List<Transform>();
        foreach(Transform child in serviceCardList.transform)
        {
            availableCards.Add(child);  // 현재 사용가능한 서비스카드 저장
        }

        int take = availableCards.Count >= 2 ? 2 : availableCards.Count == 1 ? 1 : 0;

        for(int i = 0; i < take; i++)
        {
            int index = Random.Range(0, availableCards.Count);
            Transform child = availableCards[index];
            Sprite sprite = child.GetComponent<Image>().sprite;
            images[i].enabled = true;
            images[i].sprite = sprite;

            availableCards.RemoveAt(index); // 선택된 카드를 리스트에서 제거하고 부모를 변경
            child.SetParent(alreadyPickList.transform);
        }

        // 사용가능한 서비스카드
        //int take = serviceCardList.transform.childCount >= 2 ? 2 : serviceCardList.transform.childCount == 1 ? 1 : 0;
        ////List<int> numbers =
        //    //Enumerable.Range(0, serviceCardList.transform.childCount).OrderBy(x => Random.value).Take(take).ToList();
        //for (int i = 0; i < take; i++)
        //{
        //    int index = Enumerable.Range(0, serviceCardList.transform.childCount).OrderBy(x => Random.value).First();
        //    Transform child = serviceCardList.transform.GetChild(index);
        //    Sprite sprite = child.GetComponent<Image>().sprite;
        //    images[i].enabled = true;
        //    images[i].sprite = sprite;
        //    child.SetParent(null); // 이미 해당 턴에서 나온 서비스 카드 제외
        //    child.SetParent(alreadyPickList.transform); // 해당 턴에서 나 온 서비스카드 저장

        //}

        for (int i = 0; i < 2; i++)
        {
            images[i].GetComponent<cardInfo>().judge(images[i].sprite.name);
            int price = checkPrice(images[i].sprite.name);
            texts[i].text = $"${price}";
            if (price == -1)
                images[i].enabled = false;
        }
    }

    public void back()
    {
        for (int i = 0; i < 2; i++)
        {
            images[i].enabled = true;
            images[i].sprite = cardback.sprite;
        }
    }

    void availableServiceCard()
    {
        List<Transform> temp = new List<Transform>();
        List<Image> useService = GameManager.instance.serviceDeck.FindAll(img => img.sprite.name != "-1");
        foreach (Transform child in serviceCardList.transform) // 이미 사용중인 서비스 카드
        {
            Sprite sprite = child.GetComponent<Image>().sprite;
            foreach (Image img in useService)
            {
                Sprite useSprite = img.sprite;
                if (sprite.name == useSprite.name)
                {
                    temp.Add(child); 
                    break;
                }
            }
        }
        temp.ForEach(child => child.SetParent(null)); // 이미 해당 턴에서 나온 서비스 카드 제외
    }

    public int checkPrice(string name)
    {
        if (name.Contains("ㅈㄱ") || name == "ㅂㄹㄱ")
        {
            return Random.Range(5, 7);
        }
        else if (name == "ㄸㄱㅇ" || name == "ㅆㅇ" || name == "ㅈㅂㅁㅅㅌ")
        {
            return Random.Range(8, 11);
        }
        else if (name == "cardback")
        {
            return -1;
        }

        return Random.Range(4, 7);
    }
}
