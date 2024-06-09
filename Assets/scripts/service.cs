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
    public GameObject serviceCardList; // 강화할 카드 목록을 가진 부모 오브젝트
                                       // 이 변수에서 랜덤으로 3개를 뽑아 img1, 2, 3의 sprite 적용한다.
    public Image cardback;

    void OnEnable()
    {
        
    }

    public void setting()
    {
        // 현재가지고 있는 카드 제외 코드
        // 이 상점턴에서 나온 것들 제외
        // hover 도 만들어야한다!
        // hover를 세팅할때에는 모든 카드에 대해 오브젝트를 만들지 말고
        // sprite 이름으로 맵을 검색하여 텍스트를 가져온다
        // sprite 이름으로 이 게임에 모든 상호작용을 만드는 것이 편한거 같다.
        int[] numbers =
            Enumerable.Range(0, serviceCardList.transform.childCount).OrderBy(x => Random.value).Take(2).ToArray();

        for (int i = 0; i < 2; i++)
        {
            Transform child = serviceCardList.transform.GetChild(numbers[i]);
            Sprite sprite = child.GetComponent<Image>().sprite;
            images[i].enabled = true;
            images[i].sprite = sprite;

            int price = check(sprite.name);
            texts[i].text = $"${price}";
            images[i].GetComponent<cardInfo>().judge(sprite.name);
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

    public int check(string name)
    {
        if(name.Contains("ㅈㄱ") || name == "ㅂㄹㄱ")
        {
            return Random.Range(5, 7);
        }
        else if (name == "ㄸㄱㅇ" || name == "ㅆㅇ" || name == "ㅈㅂㅁㅅㅌ")
        {
            return Random.Range(8, 11);
        }
        return Random.Range(4, 7);
    }
}
