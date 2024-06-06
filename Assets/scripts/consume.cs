using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class consume : MonoBehaviour
{
    public Image[] images;
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
            Sprite sprite = consumeCardList.transform.GetChild(numbers[i]).GetComponent<Image>().sprite;
            images[i].enabled = true;
            images[i].sprite = sprite;
        }
    }

    public void back()
    {
        for (int i = 0; i < 3; i++)
        {
            images[i].sprite = cardback.sprite;
        }
    }
}
