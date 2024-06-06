using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class service : MonoBehaviour
{
    public Image[] images;
    public GameObject serviceCardList; // 강화할 카드 목록을 가진 부모 오브젝트
                                       // 이 변수에서 랜덤으로 3개를 뽑아 img1, 2, 3의 sprite 적용한다.
    public Image cardback;

    void OnEnable()
    {
        
    }

    public void setting()
    {
        int[] numbers =
            Enumerable.Range(0, serviceCardList.transform.childCount).OrderBy(x => Random.value).Take(2).ToArray();

        for (int i = 0; i < 2; i++)
        {
            Sprite sprite = serviceCardList.transform.GetChild(numbers[i]).GetComponent<Image>().sprite;
            images[i].sprite = sprite;
        }
    }

    public void back()
    {
        for (int i = 0; i < 2; i++)
        {
            images[i].sprite = cardback.sprite;
        }
    }
}
