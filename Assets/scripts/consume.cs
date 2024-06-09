using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class consume : MonoBehaviour
{
    public Image[] images;
    public TextMeshProUGUI[] texts;
    public GameObject consumeCardList; // 강화할 카드 목록을 가진 부모 오브젝트
    // 이 변수에서 랜덤으로 3개를 뽑아 img1, 2, 3의 sprite 적용한다.
    public Image cardback;

    void OnEnable()
    {
        
    }

    public void setting()
    {
        int[] numbers =
            Enumerable.Range(0, consumeCardList.transform.childCount).OrderBy(x => Random.value).Take(3).ToArray();

        for (int i = 0; i < 3; i++)
        {
            Transform child = consumeCardList.transform.GetChild(numbers[i]);
            Sprite sprite = child.GetComponent<Image>().sprite;
            images[i].enabled = true;
            images[i].sprite = sprite;

            int price = check(sprite.name);
            texts[i].text = $"${price}";
            images[i].GetComponent<cardInfo>().judge(sprite.name[^2..]);
        }
    }

    public void back()
    {
        for (int i = 0; i < 3; i++)
        {
            images[i].enabled = true;
            images[i].sprite = cardback.sprite;
        }
    }

    public int check(string name)
    {
        if (name[^1] == 's' || name == "empty")
        {
            return Random.Range(2, 4);
        }
        else if (name[^1] == 'g')
        {
            return Random.Range(2, 5);
        }
        return Random.Range(1, 3);
    }
}
